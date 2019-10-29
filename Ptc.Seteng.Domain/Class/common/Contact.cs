using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 聯繫人資訊
    /// </summary>
    public class Contact
    {
        public Contact(string name , Phone phone)
        {

        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public Phone Phone { get; set; }
        /// <summary>
        /// 住址
        /// </summary>
        public Address Address { get; set; }
    }
}
