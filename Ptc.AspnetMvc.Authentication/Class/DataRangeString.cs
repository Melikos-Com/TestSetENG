using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication
{
    public class DataRangeString
    {

        public DataRangeString() { }

        public DataRangeString(int level, string dataRange, string rootID)
        {
            this.Level = level;
            this.DataRange = dataRange;
            this.rootID = rootID;
        }

        public int Level { get; set; }

        public string DataRange { get; set; }

        public string rootID { get; set; }

    }
}
