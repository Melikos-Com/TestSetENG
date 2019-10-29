using Ptc.Seteng.Filter;
using Ptc.Seteng.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace Ptc.Seteng.Helpers
{
    public static class ExtensionsOfPropertyInfo
    {
      

        /// <summary>
        /// prop替代元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static KeyValuePair<AvatarModel, object> Avatar<T>(this PropertyInfo propertyInfo, object model) where T : AvatarAttribute
        {
            //取得Attribute物件
            var obj = propertyInfo.GetAttributes<T>().SingleOrDefault(x => x.GetType() == typeof(AvatarAttribute));

            //取得該Property的value
            var value = model.GetType().GetProperty(propertyInfo.Name).GetValue(model, null);

            //判斷Attribute與物件的value是否存在
            if (obj == null || value == null)
                return new KeyValuePair<AvatarModel, object>(null, null);

            //取得將要對應於DB的欄位
            string targetName = ((AvatarAttribute)obj)._substituteName;

            //如果attribute沒有給,就依照原本的propName
            if (string.IsNullOrEmpty(targetName))
                targetName = propertyInfo.Name;

            //回傳物件
            return new KeyValuePair<AvatarModel, object>(new AvatarModel()
            {
                SubstituteName = targetName,
                ExpressionType = ((AvatarAttribute)obj)._nodeType,
                PredicateType = ((AvatarAttribute)obj)._predicateType,
               
            }, value
            );

        }

      

    }
}