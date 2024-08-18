using Archivist.BaseClasses;
using Archivist.Converters;
using Archivist.Interfaces;
using Archivist.Options;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Archivist.Formats.Excel
{
    /// <summary>
    /// Represents a reader for Excel files.
    /// </summary>
    /// <seealso cref="ReaderBaseClass"/>
    public class ExcelReader : ReaderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelReader"/> class.
        /// </summary>
        /// <param name="options">The Excel options.</param>
        /// <param name="converter">The converter.</param>
        public ExcelReader(ExcelOptions options, Convertinator? converter)
        {
            Options = options;
            _Converter = converter;
        }

        /// <summary>
        /// Gets the header information for Excel files.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x50, 0x4B, 0x03, 0x04 };

        /// <summary>
        /// Gets the Excel options.
        /// </summary>
        public ExcelOptions Options { get; }

        /// <summary>
        /// Gets the regular expression pattern for matching alphabetic strings.
        /// </summary>
        private static Regex AlphaRegex { get; } = new Regex("^[A-Z]+$", RegexOptions.Compiled);

        /// <summary>
        /// Gets the regular expression pattern for matching column names.
        /// </summary>
        private static Regex ColumnNameRegex { get; } = new Regex("[A-Za-z]+", RegexOptions.Compiled);

        /// <summary>
        /// Gets an empty cell.
        /// </summary>
        private static Cell EmptyCell { get; } = new Cell
        {
            DataType = null,
            CellValue = new CellValue("")
        };

        /// <summary>
        /// The converter.
        /// </summary>
        private readonly Convertinator? _Converter;

        /// <summary>
        /// Determines if the reader can read the given stream as an Excel file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>True if the reader can read the file, false otherwise.</returns>
        public override bool InternalCanRead(Stream? stream)
        {
            if (stream?.CanRead != true)
                return false;
            try
            {
                var Document = SpreadsheetDocument.Open(stream, false);
                if (Document.RootPart?.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml")
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads the Excel file asynchronously and returns the data as a generic file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is the generic file data.
        /// </returns>
        public override Task<IGenericFile?> ReadAsync(Stream? stream)
        {
            var ReturnValue = new DataTypes.Tables(_Converter);
            if (stream?.CanRead != true)
                return Task.FromResult<IGenericFile?>(ReturnValue);

            // Open the excel document
            WorkbookPart? WorkbookPart;
            try
            {
                var Document = SpreadsheetDocument.Open(stream, false);
                ReadMetadata(ReturnValue, Document);

                WorkbookPart = Document.WorkbookPart;
                if (WorkbookPart is null)
                    return Task.FromResult<IGenericFile?>(ReturnValue);

                foreach (Sheet Sheet in WorkbookPart.Workbook.Descendants<Sheet>())
                {
                    DataTypes.Table CurrentTable = ReturnValue.AddTable();
                    CurrentTable.Title = Sheet.Name?.ToString() ?? "";

                    var SheetId = Sheet.Id?.Value ?? "";

                    if (string.IsNullOrEmpty(SheetId))
                        continue;

                    var WorkSheetPart = (WorksheetPart?)WorkbookPart.GetPartById(SheetId);

                    Worksheet? WorkSheet = WorkSheetPart?.Worksheet;
                    if (WorkSheet is null)
                        continue;

                    Columns? Columns = WorkSheet.Descendants<Columns>().FirstOrDefault();

                    SheetData? SheetData = WorkSheet.Elements<SheetData>().FirstOrDefault();
                    if (SheetData is null)
                        continue;
                    var Rows = SheetData.Elements<Row>().ToList();

                    // Read the header
                    if (Rows.Count == 0)
                        continue;

                    var CurrentRow = 0;

                    Row Row = Rows[0];
                    IEnumerator<Cell>? CellEnumerator = null;
                    if (Options.FirstRowIsColumnHeaders)
                    {
                        CellEnumerator = GetExcelCellEnumerator(Row);
                        while (CellEnumerator.MoveNext())
                        {
                            Cell Cell = CellEnumerator.Current;
                            var Text = ReadExcelCell(Cell, WorkbookPart).Trim();
                            CurrentTable.Columns.Add(Text);
                        }
                        CurrentRow = 1;
                    }

                    // Read the sheet data
                    for (var I = CurrentRow; I < Rows.Count; I++)
                    {
                        DataTypes.TableRow DataRow = CurrentTable.AddRow();
                        Row = Rows[I];
                        CellEnumerator = GetExcelCellEnumerator(Row);
                        while (CellEnumerator.MoveNext())
                        {
                            Cell Cell = CellEnumerator.Current;
                            var Text = ReadExcelCell(Cell, WorkbookPart).Trim();
                            DataRow.Add(Text);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Task.FromResult<IGenericFile?>(ReturnValue);
            }

            return Task.FromResult<IGenericFile?>(ReturnValue);
        }

        /// <summary>
        /// Converts the column name to a number.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <returns>The column number.</returns>
        private static int ConvertColumnNameToNumber(string columnName)
        {
            if (!AlphaRegex.IsMatch(columnName))
                return -1;

            var ColLetters = columnName.ToCharArray();
            Array.Reverse(ColLetters);

            var ConvertedValue = 0;
            for (var I = 0; I < ColLetters.Length; I++)
            {
                var Letter = ColLetters[I];
                // ASCII 'A' = 65
                var Current = I == 0 ? Letter - 65 : Letter - 64;
                ConvertedValue += Current * (int)Math.Pow(26, I);
            }

            return ConvertedValue;
        }

        /// <summary>
        /// Gets the column name from the cell reference.
        /// </summary>
        /// <param name="cellReference">The cell reference.</param>
        /// <returns>The column name.</returns>
        private static string GetColumnName(string? cellReference) => string.IsNullOrEmpty(cellReference) ? "" : ColumnNameRegex.Match(cellReference).Value;

        /// <summary>
        /// Gets the enumerator for iterating over the cells in a row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns>The cell enumerator.</returns>
        private static IEnumerator<Cell> GetExcelCellEnumerator(Row row)
        {
            var CurrentCount = 0;
            foreach (Cell CurrentCell in row.Descendants<Cell>())
            {
                var ColumnName = GetColumnName(CurrentCell.CellReference);
                if (string.IsNullOrEmpty(ColumnName))
                    continue;

                var CurrentColumnIndex = ConvertColumnNameToNumber(ColumnName);

                if (CurrentColumnIndex < 0)
                    continue;

                for (; CurrentCount < CurrentColumnIndex; ++CurrentCount)
                {
                    yield return EmptyCell;
                }
                yield return CurrentCell;
                ++CurrentCount;
            }
        }

        /// <summary>
        /// Reads the custom properties from the Excel document.
        /// </summary>
        /// <param name="returnValue">The return value</param>
        /// <param name="document">The Excel document.</param>
        private static void ReadCustomProperties(DataTypes.Tables returnValue, SpreadsheetDocument document)
        {
            CustomFilePropertiesPart? CustomPropertiesPart = document.CustomFilePropertiesPart;
            if (CustomPropertiesPart?.Properties is null)
                return;

            foreach (CustomDocumentProperty CustomProperty in CustomPropertiesPart.Properties.Cast<CustomDocumentProperty>())
            {
                DocumentFormat.OpenXml.StringValue? PropName = CustomProperty.Name;
                if (string.IsNullOrEmpty(PropName))
                    continue;

                string? PropValue;
                if (CustomProperty.VTLPWSTR != null)
                    PropValue = CustomProperty.VTLPWSTR.Text;
                else if (CustomProperty.VTFileTime != null)
                    PropValue = CustomProperty.VTFileTime.Text;
                else if (CustomProperty.VTDouble != null)
                    PropValue = CustomProperty.VTDouble.Text;
                else if (CustomProperty.VTBool != null)
                    PropValue = CustomProperty.VTBool.Text;
                else if (CustomProperty.VTInt32 != null)
                    PropValue = CustomProperty.VTInt32.Text;
                else if (CustomProperty.VTInt64 != null)
                    PropValue = CustomProperty.VTInt64.Text;
                else if (CustomProperty.VTBString != null)
                    PropValue = CustomProperty.VTBString.Text;
                else
                    PropValue = CustomProperty.InnerText;

                returnValue.Metadata[PropName!] = PropValue;
            }
        }

        /// <summary>
        /// Reads the value of an Excel cell.
        /// </summary>
        /// <param name="cell">The cell.</param>
        /// <param name="workbookPart">The workbook part.</param>
        /// <returns>The cell value.</returns>
        private static string ReadExcelCell(Cell cell, WorkbookPart workbookPart)
        {
            if (cell is null)
                return "";

            CellValue? CellValue = cell.CellValue;
            var Text = CellValue?.Text ?? cell.InnerText ?? "";

            if (cell.DataType == null || cell.DataType != CellValues.SharedString || string.IsNullOrEmpty(cell.CellValue?.Text))
                return Text.Trim();

            Text = workbookPart.SharedStringTablePart
                               ?.SharedStringTable
                               .Elements<SharedStringItem>()
                               .ElementAt(Convert.ToInt32(cell.CellValue.Text))
                               .InnerText;

            return (Text ?? "").Trim();
        }

        /// <summary>
        /// Reads the extended file properties.
        /// </summary>
        /// <param name="returnValue">Return value</param>
        /// <param name="document">The Excel document.</param>
        private static void ReadExtendedFileProperties(DataTypes.Tables returnValue, SpreadsheetDocument document)
        {
            if (document.ExtendedFilePropertiesPart?.Properties is null)
                return;
            returnValue.Metadata["Application"] = document.ExtendedFilePropertiesPart.Properties.Application?.Text ?? "";
            returnValue.Metadata["ApplicationVersion"] = document.ExtendedFilePropertiesPart.Properties.ApplicationVersion?.Text ?? "";
            returnValue.Metadata["Company"] = document.ExtendedFilePropertiesPart.Properties.Company?.Text ?? "";
            returnValue.Metadata["Manager"] = document.ExtendedFilePropertiesPart.Properties.Manager?.Text ?? "";
            returnValue.Metadata["HyperlinkBase"] = document.ExtendedFilePropertiesPart.Properties.HyperlinkBase?.Text ?? "";
            returnValue.Metadata["Template"] = document.ExtendedFilePropertiesPart.Properties.Template?.Text ?? "";
            returnValue.Metadata["TotalTime"] = document.ExtendedFilePropertiesPart.Properties.TotalTime?.Text ?? "";
            returnValue.Metadata["Pages"] = document.ExtendedFilePropertiesPart.Properties.Pages?.Text ?? "";
            returnValue.Metadata["Words"] = document.ExtendedFilePropertiesPart.Properties.Words?.Text ?? "";
            returnValue.Metadata["Characters"] = document.ExtendedFilePropertiesPart.Properties.Characters?.Text ?? "";
            returnValue.Metadata["CharactersWithSpaces"] = document.ExtendedFilePropertiesPart.Properties.CharactersWithSpaces?.Text ?? "";
            returnValue.Metadata["Lines"] = document.ExtendedFilePropertiesPart.Properties.Lines?.Text ?? "";
            returnValue.Metadata["Paragraphs"] = document.ExtendedFilePropertiesPart.Properties.Paragraphs?.Text ?? "";
            returnValue.Metadata["PresentationFormat"] = document.ExtendedFilePropertiesPart.Properties.PresentationFormat?.Text ?? "";
            returnValue.Metadata["LinksUpToDate"] = document.ExtendedFilePropertiesPart.Properties.LinksUpToDate?.Text ?? "";
            returnValue.Metadata["HyperlinksChanged"] = document.ExtendedFilePropertiesPart.Properties.HyperlinksChanged?.Text ?? "";
        }

        /// <summary>
        /// Reads the metadata from the Excel document.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="document">The Excel document.</param>
        private static void ReadMetadata(DataTypes.Tables returnValue, SpreadsheetDocument document)
        {
            if (document is null || returnValue is null)
                return;

            ReadPackageProperties(returnValue, document);
            ReadExtendedFileProperties(returnValue, document);
            ReadCustomProperties(returnValue, document);
        }

        /// <summary>
        /// Reads the package properties.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="document">The document.</param>
        private static void ReadPackageProperties(DataTypes.Tables returnValue, SpreadsheetDocument document)
        {
            if (document.PackageProperties is null)
                return;
            returnValue.Metadata["Title"] = document.PackageProperties.Title ?? "";
            returnValue.Metadata["Author"] = document.PackageProperties.Creator ?? "";
            returnValue.Metadata["Created"] = document.PackageProperties.Created?.ToString() ?? "";
            returnValue.Metadata["Modified"] = document.PackageProperties.Modified?.ToString() ?? "";
            returnValue.Metadata["LastModifiedBy"] = document.PackageProperties.LastModifiedBy ?? "";
            returnValue.Metadata["Revision"] = document.PackageProperties.Revision ?? "";
            returnValue.Metadata["Version"] = document.PackageProperties.Version ?? "";
            returnValue.Metadata["Category"] = document.PackageProperties.Category ?? "";
            returnValue.Metadata["ContentStatus"] = document.PackageProperties.ContentStatus ?? "";
            returnValue.Metadata["Description"] = document.PackageProperties.Description ?? "";
            returnValue.Metadata["Identifier"] = document.PackageProperties.Identifier ?? "";
            returnValue.Metadata["Keywords"] = document.PackageProperties.Keywords ?? "";
            returnValue.Metadata["Language"] = document.PackageProperties.Language ?? "";
            returnValue.Metadata["Subject"] = document.PackageProperties.Subject ?? "";
            returnValue.Metadata["ContentType"] = document.PackageProperties.ContentType ?? "";
            returnValue.Metadata["LastPrinted"] = document.PackageProperties.LastPrinted?.ToString() ?? "";
        }
    }
}