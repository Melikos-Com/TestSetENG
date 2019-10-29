using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class BaiduLocation
    {
        public BaiduLocation(string x , string y)
        {
            this.X = x;
            this.Y = y;
        }

        public string X { get; set; }

        public string Y { get; set; }


        public override string ToString()
        {
            return $"Baidu Location [ X:{X} , Y:{Y} ]";
        }
    }
}
