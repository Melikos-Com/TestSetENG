using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class MailFactory : IMailFactory
    {
        private readonly ISystemLog _logger;
        private readonly IMailRepository _mailRepo;
        private readonly IBaseRepository<DataBase.TSYSDAT, Tsysdat> _paramRepo;


        public MailFactory(ISystemLog Logger,
                           IMailRepository MailRepo,
                           IBaseRepository<DataBase.TSYSDAT, Tsysdat> ParamRepo)
        {
            _logger = Logger;
            _mailRepo = MailRepo;
            _paramRepo = ParamRepo;
        }

        /// <summary>
        /// 執行寄信
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Excute(MailRequest data)
        {

            if (!data.MailTos.Any())
                throw new ArgumentNullException("發信行為失敗,並無給予寄信對象");

            #region  找到系統參數,並填入SMTP相關參數

            Tsysdat param = _paramRepo.Get(new Conditions<DataBase.TSYSDAT>());


            if (param == null)
                throw new NullReferenceException("發信行為失敗,找不到系統參數");

            data.SmptName = param.SmptName;
            data.SendMail = param.SendMail;
            data.MailPassword = param.MailPassword;


            #endregion

            return _mailRepo.Send(data);

        }
    }
}
