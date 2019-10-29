using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Provider
{
    public class TcallogProvider : ITcallogProvider
    {


        public TcallogProvider() { }

        /// <summary>
        /// 取得案件照片
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <returns></returns>
        public IEnumerable<Byte[]> GetWebImageList(string CompCd, string Sn)
        {

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.TCALIMG.Where(x => x.Comp_Cd == CompCd &&
                                                  x.Sn == Sn);


                if (!query.Any()) return null;

                //var result = query.Select(x => x.Call_Image)
                //                  .ToList();
                var result = db.TUpFile.Where(x => query.Select(y => y.File_Seq).Contains(x.File_Seq)).Select(z => z.Upload_File).ToList();

                return result;

            }

        }
        /// <summary>
        /// 紀錄推播訊息
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public Boolean RecordPush(string Sn, IPushRequest data)
        {

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var exist = db.TCallLogRecord.SingleOrDefault(x => x.SN == Sn);

                if (exist == null)
                {
                    db.TCallLogRecord.Add(new DataBase.TCallLogRecord()
                    {
                        SN = Sn,
                        RecordRemark = JsonConvert.SerializeObject(new List<IPushRequest>() { data }),
                        RecordDatetime = DateTime.Now,
                    });
                }
                else
                {

                    var records = JsonConvert.DeserializeObject<List<IPushRequest>>(exist.RecordRemark);

                    records.Add(data);

                    exist.RecordRemark = JsonConvert.SerializeObject(records);
                }

                return db.SaveChanges() > 0;
            }

        }
    }
}
