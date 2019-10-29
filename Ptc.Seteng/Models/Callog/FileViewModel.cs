using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class FileViewModel
    {

        public FileViewModel() { }

        public FileViewModel(IEnumerable<string> imgList)
        {

            this.ImageBase64List = imgList;
        }


        /// <summary>
        /// 檔案圖片
        /// </summary>
        public IEnumerable<string> ImageBase64List { get; set; } = new List<string>();
    }
}