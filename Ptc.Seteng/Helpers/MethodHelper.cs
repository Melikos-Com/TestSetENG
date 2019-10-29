using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Ptc.Seteng.Helpers
{
    public static class MethodHelper
    {
        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Boolean IsNullOrEmpty(params string[] array)
        {
            if (array == null || array.Length == 0)
                return false;

            Boolean result = false;

            array.ForEach(x => {

               if (string.IsNullOrEmpty(x))
                {
                    result = true;
                    return;
                }

            });

            return result;
        }
        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static Boolean IsAnynull(params string[] array)
        {
            if (array == null || array.Length == 0)
                return true;

            Boolean result = false;

            array.ForEach(x => {

                if (string.IsNullOrEmpty(x))
                {
                    result = true;
                    return;
                }

            });

            return result;
        }
        /// <summary>
        /// XLWorkbook => byte
        /// </summary>
        /// <param name="wb"></param>
        /// <returns></returns>
        public static byte[] GetExcel(XLWorkbook wb)
        {
            using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// GetEnumDescription
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}