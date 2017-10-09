using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NHRMSAttendanceLog
{
    class ExcelSheetController
    {
        //private static object column;


        public static void ExcelGenerator(List<ExcelModel> list)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //xlApp.DisplayAlerts = false;
            int row;
            int col;

            List<String> headingList = new List<string>();
            headingList.Add("Employee Code");
            headingList.Add("Employee Name");
            headingList.Add("Date");
            headingList.Add("Check In");
            headingList.Add("Check Out");
            headingList.Add("Total Hours");
            headingList.Add("Status");
            
            if (xlApp == null)
            {
                Console.Write("Excel is not properly installed!!");
                return;
            }


            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            
            
            row = 1;
            col = 1;
            
            List<String> objectList=new List<string>();
            String fileName=null;

            foreach(String heading in headingList)
            {
                
                xlWorkSheet.Cells[row,col++] =heading ;
            }

            foreach(ExcelModel excelModel in list)
            {
                row++;
                objectList=excelModel.getList();
                for(int column = 0; column < objectList.Count(); column++)
                {
                    
                    if(column==5 && objectList[column]!=null && Double.Parse(objectList[column]) < 8.5)
                    {
                        var columnHeadingsRange = xlWorkSheet.Range[
                                                                    xlWorkSheet.Cells[row, column+1],
                                                                    xlWorkSheet.Cells[row, column+1]
                                                                    ];
                        columnHeadingsRange.Interior.Color = XlRgbColor.rgbRed;
                    }
                    
                    xlWorkSheet.Cells[row, column + 1]=objectList[column];
                }
               
                
            }
            fileName = "c:\\Attendence Sheets\\" + objectList[2];
            
            //"e:\\attendenceSheet.xls"
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            Console.Write("Excel file created , you can find the file "+fileName+"\n");
        }
    }
}
