using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class AvatarModel
    {

        public string SubstituteName { get; set; }

        public ExpressionType ExpressionType { get; set; }

        public AppendPredicateWith  PredicateType { get; set; }

    }
}