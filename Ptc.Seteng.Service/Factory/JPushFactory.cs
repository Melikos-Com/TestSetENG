
using Newtonsoft.Json;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class JPushFactory : IPushFactory
    {
        private readonly ISystemLog _logger;
        private readonly IPushRepository _notifyRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _tvenderRepo;
        private readonly IBaseRepository<DataBase.TCallLogRecord, TcallogRecord> _pushRecordRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;

        public JPushFactory(ISystemLog Logger,
                            IPushRepository NotifyRepo,
                            IBaseRepository<DataBase.TVENDER, Tvender> TvenderRepo,
                            IBaseRepository<DataBase.TCallLogRecord, TcallogRecord> PushRecordRepo,
                            IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                            IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo)
        {
            this._logger = Logger;
            this._notifyRepo = NotifyRepo;
            this._tvenderRepo = TvenderRepo;
            this._technicianRepo = TechnicianRepo;
            this._pushRecordRepo = PushRecordRepo;
            this._technicianGroupRepo = TechnicianGroupRepo;
        }
        /// <summary>
        /// 單一推播
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean Exucte(JPushRequest data)
        {
            _logger.Info($"---------準備執行單一推播,公司:{data.CompCd},廠商:{data.VendorCd}----------");

            #region 找到技師資訊

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Comp_Cd == data.CompCd &&
                         x.Vender_Cd == data.VendorCd);


            con.And(x => data.Accounts.Contains(x.Account));

            var technicians = _technicianRepo.GetList(con);

            if (technicians == null || technicians.Count == 0)
            {
                _logger.Info($"[INFO]=>單一推播時,找不到技師群資訊");
                return false;
            }

            #endregion

            #region 推播

            bool bo = _notifyRepo.Push(data.Content, data.Extras, RegIds(technicians));
            if (bo)
            {
                //推播給誰,需要紀錄
                technicians.ForEach(x =>
                {
                    if (x.RegistrationID == string.Empty)
                        this.Record(data.CompCd, data.Sn, x.Account, $"案件編號：{data.Sn}，因為沒有推播ID，無法推播。");
                    else
                        this.Record(data.CompCd, data.Sn, x.Account, data.Content);
                });
            }
            #endregion

            return true;

        }
        /// <summary>
        /// 案件立案-自動推播
        /// </summary>
        /// <param name="data"></param>
        /// <param name="account"></param>
        /// <param name="RegId"></param>
        /// <returns></returns>
        public Boolean Exucte(JPushRequest data, string account, string RegId)
        {
            _logger.Info($"---------準備執行自動推播,公司:{data.CompCd},廠商:{data.VendorCd}----------");
            _logger.Info($"送出推播，技師帳號：{account}");
            if (RegId == string.Empty)
            {
                _logger.Info($"技師帳號：{account}，沒有RegID。");
                this.Record(data.CompCd, data.Sn, account, $"案件編號：{data.Sn}，因為沒有推播ID，無法推播。");
            }
            else
            {
                bool bo = _notifyRepo.Push(data.Content, data.Extras, RegId);
                if (bo)
                {
                    this.Record(data.CompCd, data.Sn, account, data.Content);
                }
            }
            return true;
        }
        /// <summary>
        /// 多案件、多技師推撥
        /// </summary>
        /// <returns>從Web進行通知</returns>
        public Boolean Exucte(JPushRequest data, List<string> Sn, Dictionary<string, string> Account)
        {
            _logger.Info($"---------準備執行多案件、多技師推播,公司:{data.CompCd},廠商:{data.VendorCd}----------");
            Account.ForEach(account =>
                {
                    _logger.Info($"送出推播，技師帳號：{account.Key}");

                    if (account.Value == string.Empty)
                    {
                        _logger.Info($"技師帳號：{account.Key}，沒有RegID。");
                        Sn.ForEach(sn => 
                        {                           
                            this.Record(data.CompCd, sn, account.Key, $"案件編號：{sn}，因為沒有推播ID，無法推播。");
                        });                 
                    }
                    else
                    {
                        bool bo = _notifyRepo.Push(data.Content, data.Extras, account.Value);

                        if (bo)
                        {
                            Sn.ForEach(sn =>
                            {
                                this.Record(data.CompCd, sn, account.Key, data.Content);
                            });
                        }
                    }
                }
            );
            return true;
        }
        /// <summary>
        /// 多案件、單一技師推播(指派)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public Boolean Exucte(JPushRequest data, List<string> Sn, List<string> Account)
        {
            _logger.Info($"---------準備執行多案件、多技師推播,公司:{data.CompCd},廠商:{data.VendorCd}----------");

            _logger.Info($"送出推播，技師帳號：{Account[0].ToString()}");

            if (string.IsNullOrEmpty(Account[1]) || Account[1] == string.Empty)
            {                          
                _logger.Info($"技師帳號：{Account[0].ToString()}，沒有RegID。");
                Sn.ForEach(sn =>
                {
                    this.Record(data.CompCd, sn, Account[0].ToString(), $"案件編號：{sn}，因為沒有推播ID，無法推播。");
                });
            }
            else
            {
                bool bo = _notifyRepo.Push(data.Content, data.Extras, Account[1].ToString());

                if (bo)
                {
                    Sn.ForEach(sn =>
                    {
                        this.Record(data.CompCd, sn, Account[0].ToString(), data.Content);
                    });
                }
            }

            return true;
        }



        /// <summary>
        /// 取得技師的regID
        /// </summary>
        /// <param name="technicians"></param>
        /// <returns></returns>
        private String[] RegIds(IEnumerable<TvenderTechnician> technicians)
        {

            if (technicians == null || technicians.Count() == 0)
            {
                _logger.Info($"[INFO]=>解析regID時,沒有傳入技師資訊");
                return default(String[]);
            }

            HashSet<string> ids = new HashSet<string>();

            technicians?.ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.RegistrationID))
                {
                    _logger.Info($"[INFO]=>解析regID時,技師並無註冊RegID,技師帳號:{x.Account}");
                    return;
                }

                ids.Add(x.RegistrationID);
            });

            return ids.ToArray();
        }

        /// <summary>
        /// 紀錄推播訊息
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Content"></param>
        private void Record(string CompCd, string Sn, string Account, string Content)
        {
            var con = new Conditions<DataBase.TCallLogRecord>();

            con.And(x => x.SN == Sn &&
                         x.Account == Account);

            con.Allow(x => x.RecordRemark);
            con.Allow(x => x.RecordDatetime);

            TcallogRecord record = new TcallogRecord()
            {
                Comp_Cd = CompCd,
                SN = Sn,
                Account = Account,
                RecordDatetime = DateTime.Now,
                RecordRemark = Content,
            };

            _pushRecordRepo.Insert(con, record);
        }

    }
}
