using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 電話資訊
    /// </summary>
    public class Phone
    {

       
        public Phone()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ext"></param>
        public Phone(string telephone)
        {
            this.Telephone = telephone;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telephoneArea"></param>
        /// <param name="telephone"></param>
        public Phone(string telephoneArea, string telephone)
        {
            this.TelephoneArea = telephoneArea;
            this.Telephone = telephone;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="telephoneArea"></param>
        /// <param name="telephone"></param>
        /// <param name="ext"></param>
        public Phone(string telephoneArea, string telephone, string ext)
        {
            this.TelephoneArea = telephoneArea;
            this.Telephone = telephone;
            this.Ext = ext;
        }

        public override string ToString()
        {
            return $"({TelephoneArea}){Telephone}Ext:{Ext}";
        }

        /// <summary>
        /// 分機
        /// </summary>
        public string Ext { get; set; }
        /// <summary>
        /// 電話號碼
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 電話區碼
        /// </summary>
        public string TelephoneArea { get; set; }
     
    }
}
