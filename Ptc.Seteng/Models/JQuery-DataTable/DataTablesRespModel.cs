using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    /// <summary>
    /// DataTables Ajax(伺服器端)回傳資料格式
    /// <para>更多資訊請見：<see cref="https://datatables.net/manual/server-side"/></para>
    /// </summary>
    public class DataTablesRespModel
    {
        public DataTablesRespModel()
        {

        }

        public DataTablesRespModel(int drawCount)
        {
            this.draw = drawCount;
        }
        public void TotalCount(int totalCount)
        {
            recordsTotal = recordsFiltered = totalCount;
        }

        /// <summary>
        /// 等同查詢要求(request)的draw數
        /// </summary>
        public int draw { get; set; }

        /// <summary>
        /// 查詢結果總筆數
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 查詢結果筆數(分頁後)
        /// </summary>
        public int recordsFiltered { get; set; }

        /// <summary>
        /// 查詢結果資料
        /// </summary>
        public string[][] data { get; set; }

        /// <summary>
        /// 非必要欲回傳的錯誤資訊
        /// </summary>
        public string error { get; set; }

        /// <summary>
        /// 擴充欄位
        /// </summary>
        public object extend { get; set; }
    }
}