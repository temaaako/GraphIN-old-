using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace GraphIN.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Excel = Microsoft.Office.Interop.Excel;

    public class ExcelToArrayConverter
    {
        public static string[] ConvertColumnToArray(string filePath, int columnNumber)
        {
            // Create Excel application object
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = null;
            Excel.Worksheet excelWorksheet = null;

            try
            {
                // Open Excel workbook
                excelWorkbook = excelApp.Workbooks.Open(filePath);
                // Get the first worksheet
                excelWorksheet = excelWorkbook.Sheets[1];

                // Get the used range of the specified column
                Excel.Range usedRange = excelWorksheet.UsedRange;
                Excel.Range column = usedRange.Columns[columnNumber];

                // Check if the column exists in the Excel document
                if (column == null)
                {
                    return null;
                }

                // Get the values in the specified column
                object[,] values = column.Value;

                // Convert the values to a string array
                string[] array = new string[values.GetLength(0)];
                for (int i = 0; i < values.GetLength(0); i++)
                {
                    array[i] = Convert.ToString(values[i + 1, 1]);
                }

                return array;
            }
            finally
            {
                // Clean up Excel objects
                if (excelWorksheet != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
                }
                if (excelWorkbook != null)
                {
                    excelWorkbook.Close();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
            }
        }
    }
}
