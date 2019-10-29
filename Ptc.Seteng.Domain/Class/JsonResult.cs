using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 與app界接的回傳物件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T>
    {

        public JsonResult() { }

        public JsonResult(T Element,
                          string Message,
                          int TotalCount,
                          Boolean IsSuccess)
        {

            this.element = Element;
            this.totalCount = TotalCount;
            this.message = Message;
            this.isSuccess = IsSuccess;
        }

        /// <summary>
        /// 傳輸資料
        /// </summary>
        public T element { get; set; }

        /// <summary>
        /// 資料筆數
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// 回饋訊息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public Boolean isSuccess { get; set; }

        /// <summary>
        /// 額外資訊
        /// </summary>
        public Object additions { get; set; }
    }
}
