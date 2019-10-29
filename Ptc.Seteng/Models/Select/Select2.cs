using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class Select2
    {

        public Select2() { }

        public Select2(string id, string text , object data) {

            this.id = id;
            this.text = text;
            this.data = data;

        }

        public string id { get; set; }

        public string text { get; set; }

        public object data { get; set; }


    }
}