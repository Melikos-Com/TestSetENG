using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ptc.Seteng.Filter
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct |
                           System.AttributeTargets.Property)]
    public class AvatarAttribute : System.Attribute
    {
        public string _substituteName;
        public ExpressionType _nodeType;
        public AppendPredicateWith _predicateType;

        /// <summary>
        /// 替代標籤
        /// </summary>
        /// <param name="substituteName"></param>
        /// <param name="nodeType"></param>
        /// <param name="predicateType"></param>
        public AvatarAttribute(string substituteName,
                               ExpressionType nodeType,
                               AppendPredicateWith predicateType)
        {
            this._substituteName = substituteName;
            this._nodeType = nodeType;
            this._predicateType = predicateType;
        }
    }
}