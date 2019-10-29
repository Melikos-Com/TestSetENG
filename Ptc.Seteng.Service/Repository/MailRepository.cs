using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public class MailRepository : IMailRepository
    {
      

        public MailRepository() { }

        public Boolean Send(MailRequest data)
        {

            //建立MailMessage物件  
            MailMessage mms = new MailMessage();
            //指定一位寄信人MailAddress  
            mms.From = new MailAddress(data.SendMail);
            //信件主旨  
            mms.Subject = data.Subject;
            //信件內容  
            mms.Body = data.Body;
            //信件內容 是否採用Html格式  
            mms.IsBodyHtml = data.isBodyHtml;
            //放入寄信對象
            Array.ForEach(data.MailTos ?? new string[] { }, toUser =>
            {
                if (!string.IsNullOrEmpty(toUser.Trim()))
                    mms.To.Add(new MailAddress(toUser.Trim()));
            });
            //放入CC對象
            Array.ForEach(data.Ccs ?? new string[] { }, ccUser =>
            {

                if (!string.IsNullOrEmpty(ccUser.Trim()))
                    mms.CC.Add(new MailAddress(ccUser.Trim()));

            });
            //放入附件
            data.files?.ForEach(file => mms.Attachments.Add(new Attachment(file.Value, file.Key)));


            using (SmtpClient client = new SmtpClient(data.SmptName, 25))
            {

                //若有帳號密碼存在,需要通過驗證
                if (!string.IsNullOrEmpty(data.SendMail) &&
                    !string.IsNullOrEmpty(data.MailPassword))
                    client.Credentials = new NetworkCredential(data.SendMail, data.MailPassword);

                client.Send(mms);
            }

            //釋放stream
            mms.Attachments?.ForEach(file => file.Dispose());


            return true;


        }

    }
}
