using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class MailRequest
    {
        public MailRequest() { }

        public MailRequest(string[] MailTos)
        {
            this.MailTos = MailTos;
        }
        public MailRequest(string MailFrom,
                           string[] MailTos)
        {
            this.MailFrom = MailFrom;
            this.MailTos = MailTos;
        }

      
        public MailRequest(string MailFrom,
                           string[] MailTos,
                           string[] Ccs)
        {

            this.MailFrom = MailFrom;
            this.MailTos = MailTos;
            this.Ccs = Ccs;
        }
        public MailRequest(string[] MailTos,
                           string Subject,
                           string Body)
        {
            this.MailTos = MailTos;
            this.Subject = Subject;
            this.Body = Body;
        }
        /// <summary>
        /// 發信人Email
        /// </summary>
        public string MailFrom { get; set; }
        /// <summary>
        /// 要寄信的對象
        /// </summary>
        public string[] MailTos { get; set; }
        /// <summary>
        /// CC對象
        /// </summary>
        public string[] Ccs { get; set; }
        /// <summary>
        /// 主旨
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 是否為Html格式
        /// </summary>
        public Boolean isBodyHtml { get; set; } = false;
        /// <summary>
        /// 附加的檔案
        /// </summary>
        public Dictionary<string, Stream> files { get; set; } = new Dictionary<string, Stream>();
        /// <summary>
        /// 寄信模式
        /// </summary>
        public string mailType { get; set; }
        /// <summary>
        /// SERVER OF SMTP
        /// </summary>
        public string SmptName { get; set; }
        /// <summary>
        /// ACCOUNT
        /// </summary>
        public string SendMail { get; set; }
        /// <summary>
        /// PASSWORD
        /// </summary>
        public string MailPassword { get; set; }

    }
}
