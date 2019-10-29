using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Linq;

namespace Ptc.AspnetMvc
{
    public static class Extension
    {
        #region IEnumerable<T> to SelectListsItem

        /// <summary>
        /// 從集合中取得SelectListItem
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItem<T>(
          this IEnumerable<T> enumerable,
          Func<T, string> getText,
          Func<T, string> getValue,
          string selectValue = null)
        {

            if (enumerable == null)
                return new List<SelectListItem>();

            if (selectValue == null)
                return enumerable
                  .Select(x => new SelectListItem { Text = getText(x), Value = getValue(x) })
                  .ToList();
            else
                return enumerable
                    .Select(x => new SelectListItem { Text = getText(x), Value = getValue(x).ToString(), Selected = getValue(x) == selectValue ? true : false })
                    .ToList();
        }

        /// <summary>
        /// 從集合中取得SelectListItem
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns></returns>
        public static List<SelectListItem> ToSelectListItem<T>(
          this IEnumerable<T> enumerable,
          Func<T, string> getText,
          Func<T, int> getValue,
          int? selectValue = null)
        {
            if (enumerable == null)
                return new List<SelectListItem>();

            if (selectValue == null)
                return enumerable
                  .Select(x => new SelectListItem { Text = getText(x), Value = getValue(x).ToString() })
                  .ToList();
            else
                return enumerable
                    .Select(x => new SelectListItem { Text = getText(x), Value = getValue(x).ToString(), Selected = getValue(x) == selectValue ? true : false })
                 .ToList();
        }

        #endregion




    }
}