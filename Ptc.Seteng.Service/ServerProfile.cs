using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using Autofac;


namespace Ptc.Seteng
{
    public class ServerProfile
    {

        private static ServerProfile _serverProfile;


        public ServerProfile()
        {
            this.CALLOG_PATH = ConfigurationManager.AppSettings["CALLOG_PATH"];
            this.LOG_PATH = ConfigurationManager.AppSettings["LOG_PATH"];
            this.STICKER_PATH = ConfigurationManager.AppSettings["STICKER_PATH"];
            this.JPUSH_APP_KEY = ConfigurationManager.AppSettings["JPUSH_APP_KEY"];
            this.LOCAL_API_SITE = ConfigurationManager.AppSettings["LOCAL_API_SITE"];
            this.TECHNICIAN_PATH = ConfigurationManager.AppSettings["TECHNICIAN_PATH"];
            this.LICENSE_PATH = ConfigurationManager.AppSettings["LICENSE_PATH"];
            this.REMOTE_API_SITE = ConfigurationManager.AppSettings["REMOTE_API_SITE"];
            this.JPUSH_APP_SECRET = ConfigurationManager.AppSettings["JPUSH_APP_SECRET"];
            this.LOCAL_SITE = ConfigurationManager.AppSettings["LOCAL_SITE"];
            this.REMOTE_SITE = ConfigurationManager.AppSettings["REMOTE_SITE"];
            this.SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"];
            this.SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"];
            this.MAIL_ACCOUNT = ConfigurationManager.AppSettings["MAIL_ACCOUNT"];
            this.MAIL_PWD = ConfigurationManager.AppSettings["MAIL_PWD"];
            this.VendorVesion = ConfigurationManager.AppSettings["VendorVesion"];
            this.VendorVesionNew = ConfigurationManager.AppSettings["VendorVesionNew"];
            this.Debugger = ConfigurationManager.AppSettings["Debuger"];
            this.WebFormUrl = ConfigurationManager.AppSettings["webForm"];
            this.Mail = ConfigurationManager.AppSettings["SendMail"];
            this.PushToOfficial = ConfigurationManager.AppSettings["PushToOfficial"];
        }


        public static ServerProfile GetInstance()
        {
            if (_serverProfile == null)
                _serverProfile = new ServerProfile();


            return _serverProfile;
        }

        /// <summary>
        /// 技師大頭照
        /// </summary>
        public string STICKER_PATH { get; set; }
        /// <summary>
        /// 技師技能證書
        /// </summary>
        public string LICENSE_PATH { get; set; }
        /// <summary>
        /// 技師證件照
        /// </summary>
        public string TECHNICIAN_PATH { get; set; }
        /// <summary>
        /// LOG
        /// </summary>
        public string LOG_PATH { get; set; }
        /// <summary>
        /// 無圖片位址
        /// </summary>
        public string NON_FILE_PATH { get; set; }
        /// <summary>
        /// 叫修案件
        /// </summary>
        public string CALLOG_PATH { get; set; }
        /// <summary>
        /// 取得WebApi Server位置
        /// </summary>
        public string REMOTE_API_SITE { get; set; }
        /// <summary>
        /// 取得WebApi Sorter位置
        /// </summary>
        public string LOCAL_API_SITE { get; set; }
        /// <summary>
        /// LOCAL SITE
        /// </summary>
        public string LOCAL_SITE { get; set; }
        /// <summary>
        /// REMOTE SITE
        /// </summary>
        public string REMOTE_SITE { get; set; }
        /// <summary>
        /// 登入上一層帳號
        /// </summary>
        public string REMOTE_USER_NAME { get; set; }
        /// <summary>
        /// 登入上一層密碼
        /// </summary>
        public string REMOTE_USER_PASSWORD { get; set; }
        /// <summary>
        /// 極光推播 App KEY
        /// </summary>
        public string JPUSH_APP_KEY { get; set; }
        /// <summary>
        /// 極光推播 App 金鑰
        /// </summary>
        public string JPUSH_APP_SECRET { get; set; }
        /// <summary>
        /// SERVER OF SMTP
        /// </summary>
        public string SMTP_SERVER { get; set; }
        /// <summary>
        /// PORT OF SMTP
        /// </summary>
        public string SMTP_PORT { get; set; }
        /// <summary>
        /// ACCOUNT
        /// </summary>
        public string MAIL_ACCOUNT { get; set; }
        /// <summary>
        /// PASSWORD
        /// </summary>
        public string MAIL_PWD { get; set; }
        /// <summary>
        /// 廠商APP版本 VendorVesionNew
        /// </summary>
        public string VendorVesion { get; set; }
        /// <summary>
        /// 廠商APP版本(New)
        /// </summary>
        public string VendorVesionNew { get; set; }
        /// <summary>
        /// 網頁導向
        /// </summary>
        public string Debugger { get; set; }
        /// <summary>
        /// seteng工程保修系統連結
        /// </summary>
        public string WebFormUrl { get; set; }
        /// <summary>
        /// 收件者
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 推撥開關(true：正式 / false：開發)
        /// </summary>
        public string PushToOfficial { get; set; }
    }
}






