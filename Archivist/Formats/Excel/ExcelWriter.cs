using Archivist.BaseClasses;
using Archivist.Interfaces;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Archivist.Formats.Excel
{
    /// <summary>
    /// Writes an Excel file.
    /// </summary>
    /// <seealso cref="WriterBaseClass"/>
    public class ExcelWriter : WriterBaseClass
    {
        /// <summary>
        /// The default metadata.
        /// </summary>
        private static readonly string[] _DefaultMetadata = new string[]
        {
            "Author", "Title", "Created", "Modified", "LastModifiedBy", "Revision", "Version", "Category", "ContentStatus",
            "Description", "Identifier", "Keywords", "Language", "Subject", "ContentType", "LastPrinted", "Application",
            "ApplicationVersion", "Company", "Manager", "HyperlinkBase", "Template", "TotalTime", "Pages", "Words",
            "Characters", "CharactersWithSpaces", "Lines", "Paragraphs", "PresentationFormat", "LinksUpToDate",
            "HyperlinksChanged"
        };

        /// <summary>
        /// Writes the file to the stream
        /// </summary>
        /// <param name="file">The file object to write.</param>
        /// <param name="stream">The stream to write to.</param>
        /// <returns>True if it is written succesfully, false otherwise.</returns>
        public override Task<bool> WriteAsync(IGenericFile? file, Stream? stream)
        {
            if (stream?.CanWrite != true || file is null)
                return Task.FromResult(false);
            try
            {
                DataTypes.Tables? TablesFile = file.ToFileType<DataTypes.Tables>();
                if (TablesFile is not null)
                    return Task.FromResult(WriteTables(stream, TablesFile));
                DataTypes.Table? TableFile = file.ToFileType<DataTypes.Table>();
                if (TableFile is not null)
                    return Task.FromResult(WriteTable(stream, TableFile));
                return Task.FromResult(WriteFile(stream, file));
            }
            catch { return Task.FromResult(false); }
        }

        /// <summary>
        /// Gets the specified column name.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>The column name</returns>
        private static string Column(int column)
        {
            var ColumnLetter = "";
            while (column > 0)
            {
                var Mod = (column - 1) % 26;
                ColumnLetter = (char)(65 + Mod) + ColumnLetter;
                column = (column - Mod) / 26;
            }
            return ColumnLetter;
        }

        /// <inheritdoc/>
        /// <summary>
        /// Inserts the sheet in worksheet.
        /// </summary>
        /// <param name="workbookPart">The workbook part.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <returns>The worksheet</returns>
        private static WorksheetPart InsertSheetInWorksheet(WorkbookPart workbookPart, string? sheetName = "")
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart NewWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            NewWorksheetPart.Worksheet = new Worksheet(new SheetData());
            NewWorksheetPart.Worksheet.Save();

            Sheets? Sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            if (Sheets is null)
                return NewWorksheetPart;
            var RelationshipId = workbookPart.GetIdOfPart(NewWorksheetPart);

            // Get a unique ID for the new sheet.
            uint SheetId = 1;
            if (Sheets.Elements<Sheet>().Any())
            {
                SheetId = Sheets.Elements<Sheet>().Max(s => s.SheetId?.Value ?? 0) + 1;
            }

            var SheetName = sheetName;
            if (string.IsNullOrWhiteSpace(SheetName))
                SheetName = "Sheet" + SheetId;

            // Append the new worksheet and associate it with the workbook.
            var Sheet = new Sheet() { Id = RelationshipId, SheetId = SheetId, Name = SheetName };
            Sheets.Append(Sheet);
            workbookPart.Workbook.Save();

            return NewWorksheetPart;
        }

        /// <summary>
        /// Write a generic file to the specified stream asynchronously.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        /// <param name="file">The file to write.</param>
        /// <returns>True if written successfully, false otherwie.</returns>
        private static bool WriteFile(Stream stream, IGenericFile file)
        {
            using var Document = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);
            WriteMetadata(file, Document);
            WorkbookPart? WorkbookPart = Document.AddWorkbookPart();
            if (WorkbookPart is null)
                return false;
            WorkbookPart.Workbook = new Workbook();
            Sheets Sheets = WorkbookPart.Workbook.AppendChild(new Sheets());
            WorksheetPart WorksheetPart = InsertSheetInWorksheet(WorkbookPart, file.Title);
            Worksheet Worksheet = WorksheetPart.Worksheet;
            SheetData? SheetData = Worksheet.GetFirstChild<SheetData>();
            var Row = new Row { RowIndex = 1 };
            _ = Row.AppendChild(new Cell
            {
                CellValue = new CellValue(file.GetContent() ?? ""),
                DataType = new EnumValue<CellValues>(CellValues.String),
                CellReference = "A1"
            });
            _ = SheetData?.AppendChild(Row);
            return true;
        }

        /// <summary>
        /// Reads the metadata from the Excel document.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        /// <param name="document">The Excel document.</param>
        private static void WriteMetadata(IGenericFile returnValue, SpreadsheetDocument document)
        {
            document.PackageProperties.Creator = returnValue.Metadata.GetValueOrDefault("Author") ?? "";
            document.PackageProperties.Title = returnValue.Metadata.GetValueOrDefault("Title") ?? "";
            if (DateTime.TryParse(returnValue.Metadata.GetValueOrDefault("Created") ?? "", out DateTime Created))
                document.PackageProperties.Created = Created;
            if (DateTime.TryParse(returnValue.Metadata.GetValueOrDefault("Modified") ?? "", out DateTime Modified))
                document.PackageProperties.Modified = Modified;
            document.PackageProperties.LastModifiedBy = returnValue.Metadata.GetValueOrDefault("LastModifiedBy") ?? "";
            document.PackageProperties.Revision = returnValue.Metadata.GetValueOrDefault("Revision") ?? "";
            document.PackageProperties.Version = returnValue.Metadata.GetValueOrDefault("Version") ?? "";
            document.PackageProperties.Category = returnValue.Metadata.GetValueOrDefault("Category") ?? "";
            document.PackageProperties.ContentStatus = returnValue.Metadata.GetValueOrDefault("ContentStatus") ?? "";
            document.PackageProperties.Description = returnValue.Metadata.GetValueOrDefault("Description") ?? "";
            document.PackageProperties.Identifier = returnValue.Metadata.GetValueOrDefault("Identifier") ?? "";
            document.PackageProperties.Keywords = returnValue.Metadata.GetValueOrDefault("Keywords") ?? "";
            document.PackageProperties.Language = returnValue.Metadata.GetValueOrDefault("Language") ?? "";
            document.PackageProperties.Subject = returnValue.Metadata.GetValueOrDefault("Subject") ?? "";
            document.PackageProperties.ContentType = returnValue.Metadata.GetValueOrDefault("ContentType") ?? "";
            if (DateTime.TryParse(returnValue.Metadata.GetValueOrDefault("LastPrinted") ?? "", out DateTime LastPrinted))
                document.PackageProperties.LastPrinted = LastPrinted;

            ExtendedFilePropertiesPart ExtendedProperties = document.AddExtendedFilePropertiesPart();
            ExtendedProperties.Properties = new DocumentFormat.OpenXml.ExtendedProperties.Properties
            {
                Application = new Application { Text = returnValue.Metadata.GetValueOrDefault("Application") ?? "" },
                ApplicationVersion = new ApplicationVersion { Text = returnValue.Metadata.GetValueOrDefault("ApplicationVersion") ?? "" },
                Company = new Company { Text = returnValue.Metadata.GetValueOrDefault("Company") ?? "" },
                Manager = new Manager { Text = returnValue.Metadata.GetValueOrDefault("Manager") ?? "" },
                HyperlinkBase = new HyperlinkBase { Text = returnValue.Metadata.GetValueOrDefault("HyperlinkBase") ?? "" },
                Template = new Template { Text = returnValue.Metadata.GetValueOrDefault("Template") ?? "" },
                TotalTime = new TotalTime { Text = returnValue.Metadata.GetValueOrDefault("TotalTime") ?? "" },
                Pages = new DocumentFormat.OpenXml.ExtendedProperties.Pages { Text = returnValue.Metadata.GetValueOrDefault("Pages") ?? "" },
                Words = new Words { Text = returnValue.Metadata.GetValueOrDefault("Words") ?? "" },
                Characters = new Characters { Text = returnValue.Metadata.GetValueOrDefault("Characters") ?? "" },
                CharactersWithSpaces = new CharactersWithSpaces { Text = returnValue.Metadata.GetValueOrDefault("CharactersWithSpaces") ?? "" },
                Lines = new Lines { Text = returnValue.Metadata.GetValueOrDefault("Lines") ?? "" },
                Paragraphs = new Paragraphs { Text = returnValue.Metadata.GetValueOrDefault("Paragraphs") ?? "" },
                PresentationFormat = new PresentationFormat { Text = returnValue.Metadata.GetValueOrDefault("PresentationFormat") ?? "" },
                LinksUpToDate = new LinksUpToDate { Text = returnValue.Metadata.GetValueOrDefault("LinksUpToDate") ?? "" },
                HyperlinksChanged = new HyperlinksChanged { Text = returnValue.Metadata.GetValueOrDefault("HyperlinksChanged") ?? "" }
            };
            CustomFilePropertiesPart CustomProperties = document.AddCustomFilePropertiesPart();

            foreach (var MetadataKey in returnValue.Metadata.Keys)
            {
                if (MetadataKey is null)
                    continue;
                if (_DefaultMetadata.Contains(MetadataKey))
                    continue;
                var CustomProperty = new CustomDocumentProperty
                {
                    Name = MetadataKey
                };
                if (DateTime.TryParse(returnValue.Metadata[MetadataKey], out DateTime TempDateTime))
                    CustomProperty.VTFileTime = new VTFileTime { Text = TempDateTime.ToString("yyyy-MM-ddTHH:mm:ssZ") };
                else if (int.TryParse(returnValue.Metadata[MetadataKey], out var TempInt))
                    CustomProperty.VTInt32 = new VTInt32 { Text = TempInt.ToString() };
                else if (long.TryParse(returnValue.Metadata[MetadataKey], out var TempLong))
                    CustomProperty.VTInt64 = new VTInt64 { Text = TempLong.ToString() };
                else if (double.TryParse(returnValue.Metadata[MetadataKey], out var TempDouble))
                    CustomProperty.VTDouble = new VTDouble { Text = TempDouble.ToString() };
                else if (bool.TryParse(returnValue.Metadata[MetadataKey], out var TempBool))
                    CustomProperty.VTBool = new VTBool { Text = TempBool ? "true" : "false" };
                else
                    CustomProperty.VTLPWSTR = new VTLPWSTR { Text = returnValue.Metadata[MetadataKey] };

                CustomProperties.Properties.Append(CustomProperty);
            }
        }

        /// <summary>
        /// Writes the table file to the specified workbook part.
        /// </summary>
        /// <param name="workbookPart">Workbook part</param>
        /// <param name="tableFile">Table file</param>
        /// <returns>True if written successfully, false otherwise.</returns>
        private static bool WriteTable(WorkbookPart workbookPart, DataTypes.Table tableFile)
        {
            WorksheetPart WorksheetPart = InsertSheetInWorksheet(workbookPart, tableFile.Title ?? "");
            Worksheet Worksheet = WorksheetPart.Worksheet;
            SheetData? SheetData = Worksheet.GetFirstChild<SheetData>();
            if (SheetData is null)
                return false;

            var HeaderOffset = 1;
            if (tableFile.Columns.Count > 0)
            {
                HeaderOffset = 2;
                var Row = new Row { RowIndex = 1 };
                for (var X = 0; X < tableFile.Columns.Count; ++X)
                {
                    _ = Row.AppendChild(new Cell
                    {
                        CellValue = new CellValue(tableFile.Columns[X]),
                        DataType = new EnumValue<CellValues>(CellValues.String),
                        CellReference = Column(X + 1) + 1
                    });
                }
                _ = SheetData.AppendChild(Row);
            }
            for (var X = 0; X < tableFile.Count; ++X)
            {
                var Row = new Row { RowIndex = (uint)(X + 2) };
                _ = SheetData.AppendChild(Row);
                for (var Y = 0; Y < tableFile[X].Count; ++Y)
                {
                    _ = Row.AppendChild(new Cell
                    {
                        CellValue = new CellValue(tableFile[X][Y].Content ?? ""),
                        DataType = new EnumValue<CellValues>(CellValues.String),
                        CellReference = Column(Y + 1) + (X + HeaderOffset)
                    });
                }
            }
            return true;
        }

        /// <summary>
        /// Writes the tables file to the specified stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tableFile">The table file.</param>
        /// <returns>True if written successfully, false otherwise.</returns>
        private static bool WriteTable(Stream writer, DataTypes.Table tableFile)
        {
            using var Document = SpreadsheetDocument.Create(writer, SpreadsheetDocumentType.Workbook);
            WriteMetadata(tableFile, Document);

            WorkbookPart WorkbookPart = Document.AddWorkbookPart();
            WorkbookPart.Workbook = new Workbook();
            Sheets Sheets = WorkbookPart.Workbook.AppendChild(new Sheets());
            return WriteTable(WorkbookPart, tableFile);
        }

        /// <summary>
        /// Writes the tables file to the specified stream.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="tablesFile">The tables file.</param>
        /// <returns>True if written successfully, false otherwise.</returns>
        private static bool WriteTables(Stream writer, DataTypes.Tables tablesFile)
        {
            using var Document = SpreadsheetDocument.Create(writer, SpreadsheetDocumentType.Workbook);
            WriteMetadata(tablesFile, Document);

            WorkbookPart WorkbookPart = Document.AddWorkbookPart();
            WorkbookPart.Workbook = new Workbook();
            Sheets Sheets = WorkbookPart.Workbook.AppendChild(new Sheets());
            var ReturnValue = true;
            foreach (DataTypes.Table Table in tablesFile)
            {
                ReturnValue &= WriteTable(WorkbookPart, Table);
            }

            return ReturnValue;
        }
    }
}