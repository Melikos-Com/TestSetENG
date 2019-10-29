using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace Ptc.Seteng.Helpers
{
    public class ExcelResult : ActionResult
    {
        public enum ExportFormat : int { CSV = 1, Excel = 2 };

        /// <summary>
        /// Excel
        /// </summary>
        public XLWorkbook Workbook { get; set; }
        /// <summary>
        /// 檔名
        /// </summary>
        public string FileName { get; set; }

        private ExportFormat _ExportFormatItem = ExportFormat.Excel;
        private DataTable _ExportData = null;

        /// <summary>
        /// 匯出檔案類型(1:CSV, 2:Excel)
        /// </summary>
        public ExportFormat ExportFormatItem
        {
            get { return _ExportFormatItem; }
            set { _ExportFormatItem = value; }
        }

        /// <summary>
        /// 匯出資料
        /// </summary>
        public DataTable ExportData
        {
            get { return _ExportData; }
            set { _ExportData = value; }
        }

        public ExcelResult()
        {
        }

        public ExcelResult(XLWorkbook workbook, string exportFileName)
        {
            this.Workbook = workbook;
            this.FileName = exportFileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Workbook == null)
                throw new InvalidDataException("Workbook");

            if (this.ExportFormatItem == ExportFormat.CSV && this.ExportData == null)
                throw new InvalidDataException("CSV匯入無資料");


            if (string.IsNullOrWhiteSpace(this.FileName))
            {
                if (this.ExportFormatItem == ExportFormat.Excel)
                {
                    this.FileName = string.Concat(
                        "ExportData_",
                        DateTime.Now.ToString("yyyyMMddHHmmss"),
                        ".xlsx");
                }
                else
                {
                    this.FileName = string.Concat(
                        "ExportData_",
                        DateTime.Now.ToString("yyyyMMddHHmmss"),
                        ".csv");
                }
            }

            try
            {
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.ClearHeaders();

                //編碼
                context.HttpContext.Response.ContentEncoding = Encoding.UTF8;

                //匯出檔名
                var browser = context.HttpContext.Request.Browser.Browser;
                var exportFileName = browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase)
                    ? this.FileName
                    : HttpUtility.UrlEncode(this.FileName, Encoding.UTF8);

                context.HttpContext.Response.AddHeader(
                    "content-disposition",
                    string.Format("attachment;filename=\"{0}\"", exportFileName));

                //設定網頁ContentType
                if (this.ExportFormatItem == ExportFormat.Excel)
                {
                    context.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    using (var ms = new MemoryStream())
                    {
                        Workbook.SaveAs(ms);
                        ms.WriteTo(context.HttpContext.Response.OutputStream);
                        ms.Close();
                    }
                }
                else
                {
                    context.HttpContext.Response.ContentType = "text/csv";

                    using (StreamWriter sw = new StreamWriter(context.HttpContext.Response.OutputStream, Encoding.GetEncoding("BIG5")))
                    {
                        if (this.ExportData != null)
                        {
                            string[] columnNames = this.ExportData.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

                            var column = JsonConvert.SerializeObject(columnNames);

                            sw.Write(column.TrimStart('[').TrimEnd(']') + "\r\n");

                            foreach (DataRow row in this.ExportData.Rows)
                            {
                                var outStr = JsonConvert.SerializeObject(row.ItemArray);
                                sw.Write(outStr.TrimStart('[').TrimEnd(']') + "\r\n");
                            }
                        }

                        sw.WriteLine();
                        sw.Close();
                    }
                }



                context.HttpContext.Response.End();

                Workbook.Dispose();
            }
            catch
            {
                throw;
            }
        }
    }
}