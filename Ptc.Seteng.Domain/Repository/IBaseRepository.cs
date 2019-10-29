using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Repository
{
    public interface IBaseRepository<T1,T2>
                     where T1 : class
                     where T2 : class
    {
        /// <summary>
        /// 取得清單
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        PagedList<T2> GetList(Conditions<T1> conditions);
        /// <summary>
        /// 取得清單，有多Top指令
        /// </summary>
        /// <returns>For案件查詢用，一次載入筆數過多，APP會有問題</returns>
        PagedList<T2> TakeCountGetList(Conditions<T1> conditions);
        /// <summary>
        /// 單一取得
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        T2 Get(Conditions<T1> conditions);
        /// <summary>
        /// 單一刪除
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Boolean Remove(Conditions<T1> conditions);
        /// <summary>
        /// 單一新增
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Add(Conditions<T1> conditions, T2 data);
        /// <summary>
        /// 單一新增(排除query.Any)
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Insert(Conditions<T1> conditions, T2 data);
        /// <summary>
        /// 單一更新
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean Update(Conditions<T1> conditions, T2 data);
        /// <summary>
        /// 是否包含一個及一個以上的項目
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        Boolean IsExist(Conditions<T1> conditions);
        /// <summary>
        /// 取得數量
        /// </summary>
        /// <param name="conditions"></param>
        /// <returns></returns>
        int Count(Conditions<T1> conditions);
    }

}
