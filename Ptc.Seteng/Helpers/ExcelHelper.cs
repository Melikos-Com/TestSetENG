using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace Ptc.Seteng.Helpers
{
    public class ExcelHelper : XLWorkbook
    {
        public string SheetName { get; set; }
        public DataTable ExportData { get; set; }

        public ExcelHelper()
        {
        }

        public ExcelHelper(string SheetName, DataTable data)
        {
            this.SheetName = SheetName;
            this.ExportData = data;
        }

        public XLWorkbook Generate()
        {
            if (ExportData == null)
            {
                throw new InvalidDataException("ExportData");
            }
            if (string.IsNullOrWhiteSpace(this.SheetName))
            {
                this.SheetName = "Sheet1";
            }

            var ws = new XLWorkbook();

            // Add all DataTables in the DataSet as a worksheets
            ws.Worksheets.Add(this.ExportData, this.SheetName);
            ws.Worksheets.FirstOrDefault().Tables.FirstOrDefault().ShowAutoFilter = false;
            //ws.ShowGridLines = true;
            //ws.Worksheets.FirstOrDefault().Columns().AdjustToContents();

            return ws;
        }

        /// <summary>
        /// 讀取Excel.xlsx轉DateTable
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public DataTable ExcelToDataTable(System.Web.HttpPostedFileBase file)
        {
            var workbook = new XLWorkbook(file.InputStream);
            var xlWorksheet = workbook.Worksheet(1);

            var datatable = new DataTable();
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            int col = range.ColumnCount();
            int row = range.RowCount();

            // add columns hedars
            datatable.Clear();

            for (int i = 1; i <= col; i++)
            {
                IXLCell column = xlWorksheet.Cell(1, i);
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = column.Value.ToString();
                datatable.Columns.Add(dataColumn);
            }

            // add rows data   
            int firstHeadRow = 0;
            foreach (var item in range.Rows())
            {
                if (firstHeadRow != 0)
                {
                    var array = new object[col];
                    for (int y = 1; y <= col; y++)
                    {
                        array[y - 1] = item.Cell(y).Value;
                    }
                    datatable.Rows.Add(array);
                }
                firstHeadRow++;
            }
            return datatable;
        }
    }
}