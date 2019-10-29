using System.Web;
using System.Web.Optimization;

namespace Ptc.Seteng
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/css/dataTable").Include(
                        "~/Content/select.dataTables.min.css",
                        "~/Content/dataTables.bootstrap.min.css",
                        "~/Content/responsive.bootstrap.min.css",
                        "~/Content/autoFill.dataTables.min.css",
                        "~/Content/buttons.dataTables.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/fileInput").Include(
                        "~/Content/fileinput.css",
                        "~/Content/fileinput.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/treeview").Include(
                        "~/Content/bootstrap-treeview.css"));

            bundles.Add(new StyleBundle("~/Content/css/select2").Include(
                        "~/Content/select2.css",
                        "~/Content/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/alert").Include(
                        "~/Content/sweetalert2.css",
                        "~/Content/sweetalert2.min.css"));

            bundles.Add(new StyleBundle("~/Content/css/datetime").Include(
                       "~/Content/daterangepicker.css",
                       "~/Content/bootstrap-datetimepicker.min.css",
                       "~/Content/bootstrap-datetimepicker.css",
                       "~/Content/bootstrap-datetimepicker-standalone.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));


            // 使用開發版本的 Modernizr 進行開發並學習。然後，當您
            // 準備好實際執行時，請使用 http://modernizr.com 上的建置工具，只選擇您需要的測試。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //BUNDLE OF JQUERY VALIDATION 
            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                        "~/Scripts/PTC/PTCGlobal.js"));

            //BUNDLE OF JQUERY VALIDATION 
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/ProjectJS/validation/jquery.validate.min.js",
                        "~/Scripts/ProjectJS/validation/additional-methods.min.js",
                        "~/Scripts/ProjectJS/validation/messages_zh_TW.js",
                        "~/Scripts/PTC/PTCValidation.js"));

            //BUNDLE OF BOOTSRTAP FILEINPUT 
            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                        "~/Scripts/ProjectJS/fileInput/fileinput.js",
                        "~/Scripts/ProjectJS/fileInput/fileinput.min.js",
                        "~/Scripts/ProjectJS/fileInput/fileinput_zhTW.js",
                        "~/Scripts/PTC/PTCFileInput.js"));


            //BUNDLE OF BOOTSTRAP DATETIME 
            bundles.Add(new ScriptBundle("~/bundles/datetime").Include(
                        "~/Scripts/ProjectJS/datetime/moment.js",
                        "~/Scripts/ProjectJS/datetime/bootstrap-datetimepicker.js",
                        "~/Scripts/ProjectJS/datetime/bootstrap-datepicker.min.js",
                        "~/Scripts/ProjectJS/datetime/bootstrap-datetimepicker.min.js",
                        "~/Scripts/ProjectJS/datetime/bootstrap-timepicker.min.js"));

            //BUNDLE OF JQUERY DATA TABLE 
            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                        "~/Scripts/ProjectJS/dataTables/jquery.dataTables.min.js",
                        "~/Scripts/ProjectJS/dataTables/dataTables.bootstrap.min.js",
                        "~/Scripts/ProjectJS/dataTables/extensions/dataTables.fixedHeader.min.js",
                        "~/Scripts/ProjectJS/dataTables/extensions/dataTables.responsive.min.js",
                        "~/Scripts/ProjectJS/dataTables/extensions/responsive.bootstrap.min.js",
                        "~/Scripts/ProjectJS/dataTables/extensions/dataTables.buttons.min.js",
                        "~/Scripts/ProjectJS/dataTables/extensions/dataTables.select.min.js",
                        "~/Scripts/ProjectJS/dataTables/dataTables-zhTW.js",
                        "~/Scripts/PTC/PTCFloatTableFooter.js",
                        "~/Scripts/PTC/PTCDatatables.js"));        

            //BUNDLE OF TABVIEW
            bundles.Add(new ScriptBundle("~/bundles/tabview").Include(
                        "~/Scripts/PTC/PTCTabview.js"));

            //BUNDLE OF TOTOP BUTTON
            bundles.Add(new ScriptBundle("~/bundles/totop").Include(
                        "~/Scripts/PTC/PTCTotop.js"));


            //BUNDLE OF BOOTSTRAP SELECT2
            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                        "~/Scripts/ProjectJS/select2/select2.js",
                        "~/Scripts/PTC/PTCSelect2.js",
                        "~/Scripts/custom/AssetsSelect2.js",
                        "~/Scripts/custom/AreaSelect2.js",
                        "~/Scripts/custom/EnumSelect2.js"));

            //BUNDLE OF BOOTSTRAP ALERT
            bundles.Add(new ScriptBundle("~/bundles/alert").Include(
                        "~/Scripts/ProjectJS/promise/es6-promise.auto.min.js",
                        "~/Scripts/ProjectJS/sweetAlert2/sweetalert2.min.js",
                        "~/Scripts/PTC/PTCAlert.js"));

            //BUNDLE OF BOOTSTRAP TREEVIEW
            bundles.Add(new ScriptBundle("~/bundles/treeview").Include(
                        "~/Scripts/ProjectJS/treeview/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/ProjectJS/treeview/bootstrap-treeview-special.js"));

            //BUNDLE OF BOOTSTRAP OTHER EXTEND
            bundles.Add(new ScriptBundle("~/bundles/extend").Include(
                        "~/Scripts/PTC/PTCExtend.js"));



            // 將 EnableOptimizations 設為 false 以進行偵錯。如需詳細資訊，
            // 請造訪 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
