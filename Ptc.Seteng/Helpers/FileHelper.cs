using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.QrCode;

namespace Ptc.Seteng.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// 轉換ContentType
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetContentType(this string path)
        {
            switch (System.IO.Path.GetExtension(path))
            {
                case ".bmp": return "Image/bmp";
                case ".gif": return "Image/gif";
                case ".jpg": return "Image/jpeg";
                case ".png": return "Image/png";
                default: break;
            }
            return "application/octet-stream";
        }

        /// <summary>
        /// 擷取副檔名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileNameExtension(this string path)
        {
            return System.IO.Path.GetExtension(path);
        }

        /// <summary>
        /// 產生QRCODE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string QrcodeMaker(string value)
        {
            MemoryStream ms = new MemoryStream();

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300,
                }
            };


            var data = string.Format(value);
                writer.Write(data);
                Bitmap bmp = new Bitmap(writer.Write(data));
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                ms.Dispose();
                return Convert.ToBase64String(arr);
            }
           
        

    }
}