using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DotnetSelenium.DDT
{
    public class ExcelReader
    {
        public static IEnumerable<object[]> ReadExcel(string filePath, string sheetName)
        {
            Console.WriteLine($"[ExcelReader] Trying to open {filePath}");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("[ExcelReader] File missing");
                yield break;
            }

            var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(sheetName);
            var usedRange = worksheet.RangeUsed();
            int firstRow = usedRange.FirstRow().RowNumber();
            int lastRow = usedRange.LastRow().RowNumber();
            int firstCol = usedRange.FirstColumn().ColumnNumber();
            int lastCol = usedRange.LastColumn().ColumnNumber();
            Console.WriteLine($"[ExcelReader] firstRow={firstRow}, lastRow={lastRow}, firstCol={firstCol}, lastCol={lastCol}");
            for (int row = firstRow + 1; row <= lastRow; row++)
            {
                var values = new object[lastCol - firstCol + 1];
                bool allEmpty = true;

                for (int col = firstCol; col <= lastCol; col++)
                {
                    var cellValue = worksheet.Cell(row, col).GetValue<string>();
                    values[col - firstCol] = string.IsNullOrWhiteSpace(cellValue) ? "" : cellValue.Trim();

                    if (!string.IsNullOrWhiteSpace(cellValue))
                        allEmpty = false;
                }

                if (allEmpty)
                {
                    Console.WriteLine($"[ExcelReader] Empty row at {row}, stopping.");
                    break;
                }

                Console.WriteLine($"[ExcelReader] Row {row}: {string.Join(", ", values)}");
                yield return values;
            }
            
        }
    }
}
