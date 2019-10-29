using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class JPushRequest 
    {

        public JPushRequest() { }


        public JPushRequest(string compCd,
                           string venderCd)
        {
            this.CompCd = compCd;
            this.VendorCd = venderCd;
        }

        public JPushRequest(string compCd,
                           string venderCd,
                           params string[] accounts)
        {
            this.CompCd = compCd;
            this.VendorCd = venderCd;
            this.Accounts = accounts;

        }

        public JPushRequest(string compCd,
                           string venderCd,
                           params int[] groups)
        {
            this.CompCd = compCd;
            this.VendorCd = venderCd;
            this.Groups = groups;

        }

        /// <summary>
        /// 標題
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 內文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VendorCd { get; set; }
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 案件編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 群組
        /// </summary>
        public int[] Groups { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string[] Accounts { get; set; }
        /// <summary>
        /// 額外資訊
        /// </summary>
        public IDictionary<string, string> Extras { get; set; } = new Dictionary<string, string>();
    }
}
