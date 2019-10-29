

using Ptc.Logger.Service;
using Ptc.Seteng.Factory;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ptc.Seteng.Factory
{
    public class PartFactory : IPartFactory
    {


        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TVUPART, Tvupart> _tpartRepo;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _callogRepo;


        public PartFactory(ISystemLog Logger,
                           IBaseRepository<DataBase.TVUPART, Tvupart> TpartRepo,
                           IBaseRepository<DataBase.TCALLOG, Tcallog> CallogRepo)
        {
            _logger = Logger;
            _tpartRepo = TpartRepo;
            _callogRepo = CallogRepo;
        }

        /// <summary>
        /// 新增案件材料
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public Boolean Add(Tvupart part)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                part.Seq = (byte)(GetMaxSeq(part.Sn) + 1);

                #region 新增案件材料

                var con = new Conditions<DataBase.TVUPART>();

                con.And(x => x.Sn == part.Sn &&
                             x.Seq == part.Seq);

                _tpartRepo.Add(con, part);

                #endregion

          
                decimal price = GetTotalPrice(part.Sn);

            
                RefillPrice(part.CompCd, part.Sn, price);


                scope.Complete();

            }
            return true;
        }

        /// <summary>
        /// 更新案件材料
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public Boolean Update(Tvupart part)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                #region 更新案件材料

                var con = new Conditions<DataBase.TVUPART>();


                con.And(x => x.Sn == part.Sn &&
                             x.Seq == part.Seq);

                con.Allow(x => x.Part_No);
                con.Allow(x => x.Qty);
                //con.Allow(x => x.Use_Date);
                con.Allow(x => x.Price);


                _tpartRepo.Update(con , part);

                #endregion

                decimal price = GetTotalPrice(part.Sn);

                RefillPrice(part.CompCd, part.Sn, price);


                scope.Complete();

            }
            return true;
        }

        /// <summary>
        /// 刪除案件材料
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Seq"></param>
        /// <returns></returns>
        public Boolean Remove(string CompCd, string Sn, string Seq)
        {
            using (TransactionScope scope = new TransactionScope())
            {

                #region 刪除案件材料

                var con = new Conditions<DataBase.TVUPART>();

                byte value = Byte.Parse(Seq);

                con.And(x => x.Sn == Sn && 
                             x.Seq == value);

                 _tpartRepo.Remove(con);

                #endregion

                decimal price =  GetTotalPrice(Sn);

                RefillPrice(CompCd, Sn, price);


                scope.Complete();

            }
            return true;
        }

        /// <summary>
        /// 取得最大序號
        /// </summary>
        /// <returns></returns>
        private int GetMaxSeq(string Sn)
        {
            if (string.IsNullOrEmpty(Sn))
                throw new ArgumentNullException($"取得最大序號時,並沒有傳入參數");

            var con = new Conditions<DataBase.TVUPART>();

            con.And(x => x.Sn == Sn);

            var list = _tpartRepo.GetList(con);

            if (!list.Any())
                return 0;

            return list.Max(x => x.Seq);

        }

        /// <summary>
        /// 取得案件下材料的總金額
        /// </summary>
        /// <param name="Sn"></param>
        /// <returns></returns>
        private decimal GetTotalPrice(String Sn)
        {

            var con = new Conditions<DataBase.TVUPART>();

            con.And(x => x.Sn == Sn);

            var list = _tpartRepo.GetList(con);

            if (list == null || list.Count == 0)
                throw new NullReferenceException($"[ERROR]找不到案件材料資訊");

            return list.Sum(x => {

                return x.Price * x.Qty;

            });

        }

        /// <summary>
        /// 回填金額
        /// </summary>
        /// <param name="CompCd"></param>
        /// <param name="Sn"></param>
        /// <param name="Price"></param>
        /// <returns></returns>
        private Boolean RefillPrice(String CompCd, String Sn, decimal Price)
        {
            var con = new Conditions<DataBase.TCALLOG>();

            con.And(x => x.Sn == Sn && 
                         x.Comp_Cd == CompCd);


            var data = _callogRepo.Get(con);

            //data.VndPartCost = Price;
            //data.VndTotalCost = Price;
            //data.AllCost = Price + data.ArtificialCostfact;           
            
            //con.Allow(x => x.Vnd_Part_Cost,
            //          x => x.Vnd_Total_Cost,
            //          x => x.All_Cost);

            
            return _callogRepo.Update(con , data);

           
        }


    }
}
