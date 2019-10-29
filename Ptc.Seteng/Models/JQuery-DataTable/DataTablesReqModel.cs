using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Ptc.Seteng.Models
{
    /// <summary>
    /// DataTables Ajax(客戶端)送出資料格式
    /// <para>更多資訊請見：<see cref="https://datatables.net/manual/server-side"/></para>
    /// </summary>
    public class DataTablesReqModel
    {
        /// <summary>
        /// 僅供DataTables內部使用紀錄Ajax動作順序
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 各欄位設定
        /// </summary>
        public DataTablesColumnStatus[] columns { get; set; }

        /// <summary>
        /// 排序條件
        /// </summary>
        public DataTablesOrder[] order { get; set; }

        /// <summary>
        /// 查詢結果筆數的起始項次
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 查詢結果的筆數
        /// </summary>
        public int length { get; set; }

        /// <summary>
        /// 篩選條件
        /// </summary>
        public DataTablesSearchTerm search { get; set; }

        /// <summary>
        /// DataTables自帶查詢條件格式
        /// </summary>
        public sealed class DataTablesSearchTerm
        {
            /// <summary>
            /// 一般查詢字串
            /// </summary>
            public string value { get; set; }

            /// <summary>
            /// 正規表示式查詢字串
            /// </summary>
            public bool regex { get; set; }
        }

        /// <summary>
        /// DataTables排序條件格式
        /// </summary>
        public sealed class DataTablesOrder
        {
            /// <summary>
            /// 欄位次
            /// </summary>
            public int column { get; set; }

            /// <summary>
            /// 排序方式(升序、降序)
            /// </summary>
            public OrderType dir { get; set; }
        }

        /// <summary>
        /// DataTables各欄位條件設定格式
        /// </summary>
        public sealed class DataTablesColumnStatus
        {
            /// <summary>
            /// 欄位資料來源
            /// </summary>
            public string data { get; set; }

            /// <summary>
            /// 欄位名稱
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 欄位可否被篩選
            /// </summary>
            public bool searchable { get; set; }

            /// <summary>
            /// 欄位可否被排序
            /// </summary>
            public bool orderable { get; set; }

            /// <summary>
            /// 欄位篩選條件
            /// </summary>
            public DataTablesSearchTerm search { get; set; }
        }
    }

    /// <summary cref="DataTablesReqModel">
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataTablesReqModel<T> : DataTablesReqModel where T : class
    {
        /// <summary>
        /// 自訂的查詢條件
        /// </summary>
        public T criteria { get; set; }
    }

 
}