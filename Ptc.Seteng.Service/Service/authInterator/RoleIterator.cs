using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public abstract class RoleIterator : IRoleIterator
    {
        protected RoleIterator _iterator;

        public abstract string Key { get; set; }
        

        public void SetIterator(RoleIterator Iterator)
        {
            this._iterator = Iterator;
        }

        /// <summary>
        /// 驗證規則
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract Boolean IsMatch(UserBase data);

        /// <summary>
        /// 取得資料範圍
        /// </summary>
        /// <param name="Data"></param>
        public abstract void GetDataAuth(ref UserBase Data);

        /// <summary>
        /// 取得頁面權限
        /// </summary>
        /// <param name="Data"></param>
        public abstract void GetPageAuth(ref UserBase Data);
      
    }
}
