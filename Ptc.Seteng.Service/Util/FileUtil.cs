using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ptc.Seteng.Util
{
    public static class FileUtil
    {
        /// <summary>
        /// 儲存檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        public static void Save(string filePath, string fileName, HttpPostedFileBase file)
        {
            CheckDirOrCreate(filePath);

            file.SaveAs(filePath + fileName);

        }

        /// <summary>
        /// 儲存檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        public static void Save(string filePath, string fileName, byte[] file)
        {
            CheckDirOrCreate(filePath);

            System.IO.File.WriteAllBytes(filePath + fileName, file);
        }

        /// <summary>
        /// 儲存檔案
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="file"></param>
        public static void Save(string filePath, string fileName, Stream file)
        {

            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = file.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                Save(filePath, fileName, ms.ToArray());

                ms.Dispose();
            }
        }

        /// <summary>
        /// 刪除檔案
        /// </summary>
        /// <param name="filePath"></param>
        public static void Delete(string filePath)
        => System.IO.File.Delete(filePath);

        /// <summary>
        /// 檢查目錄是否存在
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static Boolean CheckDir(string dirPath)
        => Directory.Exists(dirPath);

        /// <summary>
        /// 檢查目錄是否存在,不存在則新增
        /// </summary>
        /// <param name="dirPath"></param>
        public static void CheckDirOrCreate(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        /// <summary>
        /// 取得目錄下檔案名稱
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static string[] GetDirFilesName(string dirPath)
        => Directory.GetFiles(dirPath);

        /// <summary>
        /// 取得目錄下檔案實體
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Byte[] GetDirFilesBytes(string path)
        {
            Byte[] result;

            if (!File.Exists(path))
                return null;
     

            using (MemoryStream ms = new MemoryStream())
            {
                Image img = Image.FromFile(path);

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                result = ms.ToArray();

                img.Dispose();
                ms.Dispose();
            }

            return result;
           
        }

    }


}
