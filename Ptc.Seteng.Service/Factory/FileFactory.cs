
using Ptc.Seteng.Provider;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Factory
{
    public class FileFactory : IFileFactory
    {

        private readonly ITcallogProvider _callImgProvider;

        public FileFactory(ITcallogProvider CallImgrovider)
        {
            _callImgProvider = CallImgrovider;
        }

        /// <summary>
        /// 取得案件下的圖片
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="CompCd"></param>
        /// <returns></returns>
        public IEnumerable<String> GetCallLogImg(string Sn, string CompCd)
        {
            List<string> result = new List<string>();

            result.AddRange(GetWebImgBase64Str(Sn, CompCd));

            return result;
        }

   
        /// <summary>
        /// 取得網站的案件圖片的Base64字串
        /// </summary>
        /// <returns></returns>
        private IEnumerable<String> GetWebImgBase64Str(string Sn, string CompCd)
        {
            if (string.IsNullOrEmpty(Sn) || string.IsNullOrEmpty(CompCd))
                throw new ArgumentNullException($"找尋案件圖片時,沒有給入條件");

            IEnumerable<Byte[]> result = _callImgProvider.GetWebImageList(CompCd, Sn);

            if (result == null || !result.Any())
                return new List<string>();

            return result.Select(x =>
            {
                return $"data:image/jpeg;base64,{Convert.ToBase64String(x)}";
            });
        }

    }
}
