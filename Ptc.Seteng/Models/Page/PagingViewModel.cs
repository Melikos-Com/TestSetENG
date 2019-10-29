using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    [TypeLite.TsClass]
    public class PagingViewModel
    {


        private readonly int _pageSize = 30;

        /// <summary>
        /// 頁碼 (第幾頁)
        /// </summary>
        public int page { get; set; }


        /// <summary>
        /// 頁面長度(一頁幾筆)
        /// </summary>
        public int pageSize
        {
            get { return this._pageSize; }

        }

        /// <summary>
        /// 關鍵字
        /// </summary>
        public string keyword { get; set; }

        /// <summary>
        /// 排序欄位
        /// </summary>
        public string OrderBy { get; set; }


        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}