
using Ptc.Seteng.Provider;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class CallogFactory : ICallogFactory
    {

        private readonly ITcallogProvider _callogProvider;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _callogRepo;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _compRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _venderRepo;

        public CallogFactory(ITcallogProvider CallogProvider,
                             IBaseRepository<DataBase.TVENDER, Tvender> VenderRepo,
                             IBaseRepository<DataBase.TCALLOG, Tcallog> CallogRepo,
                             IBaseRepository<DataBase.TCMPDAT, Tcmpdat> CompRepo)
        {
            _compRepo = CompRepo;
            _callogRepo = CallogRepo;
            _venderRepo = VenderRepo;
            _callogProvider = CallogProvider;

        }


        #region 叫修流程

        /// <summary>
        /// 技師認養案件
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean TechnicianAccept(Tcallog data)
        {
            var con = new Conditions<DataBase.TCALLOG>();

            con.And(x => x.Sn == data.Sn);

            con.And(x => x.Comp_Cd == data.CompCd);

            con.Include(x => x.TAcceptedLog);

            con.Allow(x => x.TAcceptedLog);       //認養主檔

            con.Allow(x => x.TimePoint);

            return _callogRepo.Update(con, data);

        }
        #endregion

        public void AddDateRecords(List<TCallLogDateRecord> DateRecords)
        {

            List<DataBase.TCallLogDateRecord> AddCDRs = DateRecords.Select(x =>
                                                                        new DataBase.TCallLogDateRecord()
                                                                        {
                                                                            Comp_Cd = x.CompCd,
                                                                            SN = x.SN,
                                                                            RecordType = (int)x.RecordType,
                                                                            RecordDate = x.RecordDate,
                                                                            Create_User = x.Create_User,
                                                                            Create_Date = x.Create_Date
                                                                        }).ToList();

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.TCallLogDateRecord.AddRange(AddCDRs);
                db.SaveChanges();
            }
        }


    }
}
