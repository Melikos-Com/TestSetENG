using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class DateAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {

            //有值才做比較
            if (value == null) { return true; }
            
            var data = (decimal)value;

            if (string.IsNullOrEmpty(data.ToString()))
                throw new ArgumentNullException("string is empty format datetime error."); 


            DateTime date;


            string[] format = { "yyyyMMdd", "yyyyMM", "yyyy", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyyMMddHH" };

            if (!DateTime.TryParseExact(data.ToString(),
                                        format,
                                        System.Globalization.CultureInfo.InvariantCulture,
                                        System.Globalization.DateTimeStyles.None,
                                        out date))
            { throw new FormatException("format string to datetime error"); }


            return true;

        }



    }
}
