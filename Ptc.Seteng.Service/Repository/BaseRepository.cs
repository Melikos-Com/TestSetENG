using AutoMapper;
using LinqKit;
using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public class BaseRepository<T1, T2> : IBaseRepository<T1, T2>
                 where T1 : class
                 where T2 : class
    {

        /// <summary>
        /// 取得清單
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public PagedList<T2> GetList(Conditions<T1> conditions)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                query = query.OrderBy(conditions.MajorOrders);

                conditions.MinorOrders.ForEach(x => query = query.ThenBy(x));

                conditions.TotalCount = query.Count();


                return new PagedList<T2>(Mapper.Map<IEnumerable<T2>>(query),
                                        conditions.PageIndex,
                                        conditions.PageSize,
                                        conditions.TotalCount);

            }

        }
        /// <summary>
        /// 取得清單，有多Top指令
        /// </summary>
        /// <returns>For案件查詢用，一次載入筆數過多，APP會有問題</returns>
        public PagedList<T2> TakeCountGetList(Conditions<T1> conditions)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {

                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                query = query.OrderBy(conditions.MajorOrders);

                conditions.MinorOrders.ForEach(x => query = query.ThenBy(x));

                conditions.TotalCount = query.Count();

                int Count = conditions.PageSize * (conditions.PageIndex + 1);

                query = query.Take(Count);

                return new PagedList<T2>(Mapper.Map<IEnumerable<T2>>(query),
                                        conditions.PageIndex,
                                        conditions.PageSize,
                                        conditions.TotalCount);

            }

        }
        /// <summary>
        /// 單一取得
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public T2 Get(Conditions<T1> conditions)
        {

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                return Mapper.Map<T2>(query.SingleOrDefault());

            }
        }

        /// <summary>
        /// (單一/整批)刪除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public Boolean Remove(Conditions<T1> conditions)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                if (!query.Any()) { throw new NullReferenceException($"[REMOVE] error. data is not found : {typeof(T1)} ."); }

                db.Set<T1>().RemoveRange(query);

                return db.SaveChanges() > 0;

            }
        }
        /// <summary>
        /// 單一新增
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean Add(Conditions<T1> conditions, T2 data)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                if (query.Any()) { throw new NullReferenceException($"[ADD] error. data is Exist : {typeof(T1)} ."); }

                var entity = Mapper.Map<T1>(data);

                db.Set<T1>().Add(entity);

                return db.SaveChanges() > 0;

            }
        }
        /// <summary>
        /// 單一新增(排除query.Any)
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean Insert(Conditions<T1> conditions, T2 data)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                //if (query.Any()) { throw new NullReferenceException($"[ADD] error. data is Exist : {typeof(T1)} ."); }

                var entity = Mapper.Map<T1>(data);

                db.Set<T1>().Add(entity);

                return db.SaveChanges() > 0;

            }
        }
        /// <summary>
        /// 單一更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Boolean Update(Conditions<T1> conditions, T2 data)
        {

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                var original = query.FirstOrDefault();

                if (original == null) { throw new NullReferenceException($"[UPDATE] error. data is not found : {typeof(T1)} ."); }

                var noval = Mapper.Map<T1>(data);

                UpdateProcess(original, noval, conditions.AllowProps);

                return db.SaveChanges() > 0;

            }

        }
        /// <summary>
        /// 是否包含一個及一個以上的項目
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public Boolean IsExist(Conditions<T1> conditions)
        {

            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                return query.Any();

            }

        }
        /// <summary>
        /// 取得數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public int Count(Conditions<T1> conditions)
        {
            using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                var query = db.Set<T1>().AsQueryable();

                conditions.Includes?.ForEach(x => query = query.Include(x));

                query = query.AsExpandable().Where(conditions.GetPredicate());

                return query.Count();

            }

        }

        #region extend

        /// <summary>
        /// 比對既有資料,並更新
        /// </summary>
        /// <param name="original"></param>
        /// <param name="noval"></param>
        /// <param name="denys"></param>
        private void UpdateProcess(T1 original, T1 noval, List<string> allows)
        {

            original.GetProperties()
                    .ForEach(x =>
                    {
                        if (allows.Contains(x.Name))
                        {

                            x.SetValue(original,
                                       noval.GetType().GetProperty(x.Name).GetValue(noval, null),
                                       null);
                        }
                    });

        }

        #endregion
    }
}
