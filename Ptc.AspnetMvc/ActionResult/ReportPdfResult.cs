using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Linq;

namespace Ptc.AspnetMvc.ActionResult
{
    /// <summary>
    /// 產生PDF，透過Reportviewer
    /// </summary>
    public class ReportPdfResult : System.Web.Mvc.ActionResult
    {
        public LocalReport LocalReport { get; set; }

        public ReportDataSourceCollection DataSource
        {
            get
            {
                return LocalReport.DataSources;
            }
        }

        public ReportPdfResult()
        {
            this.LocalReport = new LocalReport();
        }

        public ReportPdfResult(string ReportName)
        {
            this.LocalReport = new LocalReport();
            setupRdlcStram(ReportName);
        }

        public ReportPdfResult(string ReportName, List<ReportDataSource> dataSourceColl)
        {

            this.LocalReport = new LocalReport();
            setupRdlcStram(ReportName);

            //加入資料來源
            foreach (ReportDataSource item in dataSourceColl)
                this.LocalReport.DataSources.Add(item);
        }

        /// <summary>
        /// 於資源檔取得報表檔
        /// </summary>
        /// <param name="ReportName"></param>
        private void setupRdlcStram(string ReportName)
        {
            //取得報表的Stream
            System.Reflection.Assembly assReport = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Ptc.Dispatch.Reports")).FirstOrDefault();
            if (assReport == null)
                return;
            System.IO.Stream rdlcStream = assReport.GetManifestResourceStream("Ptc.Dispatch.Reports." + ReportName + ".rdlc");
            if (rdlcStream == null)
                return;

            //載入Rdlc
            this.LocalReport.LoadReportDefinition(rdlcStream);
        }

        /// <summary>
        /// 於資源檔取得子報表的報表檔
        /// </summary>
        /// <param name="ReportName">報表檔名稱</param>
        /// <param name="MainSubName">主報表區塊名稱</param>
        public void setupSubRdlcStram(string ReportName, string MainSubName)
        {
            //取得報表的Stream
            System.Reflection.Assembly assReport = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Ptc.Dispatch.Reports")).FirstOrDefault();
            if (assReport == null)
                return;
            System.IO.Stream rdlcStream = assReport.GetManifestResourceStream("Ptc.Dispatch.Reports." + ReportName + ".rdlc");
            if (rdlcStream == null)
                return;

            //載入Rdlc
            this.LocalReport.LoadSubreportDefinition(MainSubName, rdlcStream);
        }


        public override void ExecuteResult(ControllerContext context)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx        
            //D:\PTC\LISA\src\Ptc.DS2015\Ptc.Dispatch.Web\Helpers\ReportPdfResult.cs(71,20,71,30): warning CS0219: 已指派變數 'deviceInfo'，但從未使用其值
            //string deviceInfo = "<DeviceInfo>" +
            //    "  <OutputFormat>jpeg</OutputFormat>" +
            //    "  <PageWidth>8.5in</PageWidth>" +
            //    "  <PageHeight>11in</PageHeight>" +
            //    "  <MarginTop>0.5in</MarginTop>" +
            //    "  <MarginLeft>0.1in</MarginLeft>" +
            //    "  <MarginRight>0.1in</MarginRight>" +
            //    "  <MarginBottom>0.5in</MarginBottom>" +
            //    "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = this.LocalReport.Render(
                reportType,
                null,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/pdf";
            response.BinaryWrite(renderedBytes);
            response.End();
        }




    }
}