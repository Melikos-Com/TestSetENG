
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 地址資訊
    /// </summary>
    public class Address
    {
      
        public Address()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fullAddress"></param>
        public Address(string fullAddress)
        {
            this.FullAddress = fullAddress;

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="postCode"></param>
        /// <param name="addressCity"></param>
        public Address(string postCode, string addressCity)
        {
            this.PostCode = postCode;
            this.AddressCity = addressCity;

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="postCode"></param>
        /// <param name="addressCity"></param>
        /// <param name="fullAddress"></param>
        public Address(string postCode, string addressCity, string fullAddress)
        {
            this.PostCode = postCode;
            this.AddressCity = addressCity;
            this.FullAddress = fullAddress;
        }


        public override string ToString()
        {
            return $"({PostCode}){AddressCity}{FullAddress}";
        }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string PostCode { get; set; }
        /// <summary>
        /// 城市(縣市)
        /// </summary>
        public string AddressCity { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string FullAddress { get; set; }
    
    }
}
