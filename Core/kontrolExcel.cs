using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace simlitekkes.Core
{    
    public class kontrolExcel
    {
        public void WriteExcel(Page page, DataTable dt, String extension, string namaFile, string namaSheet)
        {
            IWorkbook workbook;
            if (extension == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (extension == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }

            ISheet sheet1 = workbook.CreateSheet(namaSheet);

            //make a header row
            IRow row1 = sheet1.CreateRow(0);

            for (int j = 0; j < dt.Columns.Count; j++)
            {

                ICell cell = row1.CreateCell(j);
                String columnName = dt.Columns[j].ToString();
                cell.SetCellValue(columnName);
            }

            //loops through data
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet1.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    ICell cell = row.CreateCell(j);
                    String columnName = dt.Columns[j].ToString();
                    cell.SetCellValue(dt.Rows[i][columnName].ToString());
                }
            }

            using (var exportData = new MemoryStream())
            {
                page.Response.Clear();
                workbook.Write(exportData);
                if (extension == "xlsx") //xlsx file format
                {
                    page.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    page.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "\"" + namaFile + "\""));
                    page.Response.BinaryWrite(exportData.ToArray());
                }
                else if (extension == "xls")  //xls file format
                {
                    page.Response.ContentType = "application/vnd.ms-excel";
                    page.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "\"" + namaFile + "\""));
                    page.Response.BinaryWrite(exportData.GetBuffer());
                }
                page.Response.End();
            }
        }
    }
}
