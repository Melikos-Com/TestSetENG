using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class EnumSelect
    {
        public EnumSelect()
        {
            this.Id = "";
            this.Name = "全部";
        }

        public EnumSelect(Enum value)
        {

            this.Id = ((int)value.GetType()
                                 .GetField(value.ToString())
                                 .GetValue(value))
                                 .ToString();

            this.Name = value.GetName();

        }


        public string Id { get; set; }

        public string Name { get; set; }


    }
}