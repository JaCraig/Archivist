using Archivist.BaseClasses;
using Archivist.Interfaces;
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
    public class ExcelReader : ReaderBaseClass
    {
        /// <summary>
        /// Gets the header information for Excel files.
        /// </summary>
        public override byte[] HeaderInfo { get; } = new byte[] { 0x50, 0x4B, 0x03, 0x04 };

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
        /// Determines if the reader can read the given stream as an Excel file.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>True if the reader can read the file, false otherwise.</returns>
        public override bool InternalCanRead(Stream stream)
        {
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
            var ReturnValue = new DataTypes.Tables();
            if (stream is null)
                return Task.FromResult<IGenericFile?>(ReturnValue);

            // Open the excel document
            WorkbookPart? WorkbookPart;
            try
            {
                var Document = SpreadsheetDocument.Open(stream, false);
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

                    Worksheet WorkSheet = ((WorksheetPart)WorkbookPart.GetPartById(SheetId)).Worksheet;
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

                    Row Row = Rows[0];
                    IEnumerator<Cell> CellEnumerator = GetExcelCellEnumerator(Row);
                    while (CellEnumerator.MoveNext())
                    {
                        Cell Cell = CellEnumerator.Current;
                        var Text = ReadExcelCell(Cell, WorkbookPart).Trim();
                        CurrentTable.Columns.Add(Text);
                    }

                    // Read the sheet data
                    if (Rows.Count <= 1)
                        continue;
                    for (var I = 1; I < Rows.Count; I++)
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
    }
}