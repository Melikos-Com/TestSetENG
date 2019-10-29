using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{

    /// <summary>
    /// 基本驗証包裝
    /// </summary>
    public static class WebApiUtil
    {

        /// <summary>
        /// HttpGet + BasicCredentials
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T GetRemote<T>(string url, out string message)
        {
            try
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    HttpResponseMessage response = null;
                    client.DefaultRequestHeaders.Authorization = CreateBasicCredentials(ServerProfile.GetInstance().REMOTE_USER_NAME, ServerProfile.GetInstance().REMOTE_USER_PASSWORD);
                    client.BaseAddress = new Uri(ServerProfile.GetInstance().REMOTE_API_SITE);

                    client.Timeout = new System.TimeSpan(2, 0, 0);

                    response = client.Get(url);

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}{1}", ServerProfile.GetInstance().REMOTE_API_SITE, url);
                        return default(T);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}", response.StatusCode, response.ReasonPhrase);
                        return default(T);
                    }

                    message = string.Empty;

                    return response.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }

        }

        public static T GetLocal<T>(string url, out string message)
        {
            try
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    HttpResponseMessage response = null;
                    client.DefaultRequestHeaders.Authorization = CreateBasicCredentials(ServerProfile.GetInstance().REMOTE_USER_NAME, ServerProfile.GetInstance().REMOTE_USER_PASSWORD);
                    client.BaseAddress = new Uri(ServerProfile.GetInstance().LOCAL_API_SITE);

                    client.Timeout = new System.TimeSpan(2, 0, 0);

                    response = client.Get(ServerProfile.GetInstance().LOCAL_API_SITE + url);

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}{1}", ServerProfile.GetInstance().REMOTE_API_SITE, url);
                        return default(T);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}", response.StatusCode, response.ReasonPhrase);
                        return default(T);
                    }

                    message = string.Empty;

                    return response.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }

        }

        /// <summary>
        /// HttpPOST + BasicCredentials
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T PostRemote<T, K>(string path, K obj, out string message)
        {
            return Post<T, K>(ServerProfile.GetInstance().REMOTE_API_SITE, path, obj, out message);
        }

        public static T PostLocal<T, K>(string path, K obj, out string message)
        {
            return Post<T, K>(ServerProfile.GetInstance().LOCAL_API_SITE, path, obj, out message);
        }


        /// <summary>
        /// HttpPOST + BasicCredentials
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T PutRemote<T, K>(string path, K obj, out string message)
        {
            return Put<T, K>(ServerProfile.GetInstance().REMOTE_API_SITE, path, obj, out message);
        }

        public static T PutLocal<T, K>(string path, K obj, out string message)
        {
            return Put<T, K>(ServerProfile.GetInstance().LOCAL_API_SITE, path, obj, out message);
        }



        private static T Post<T, K>(string host, string path, K obj, out string message)
        {
            string address = host + path;

            try
            {
                HttpResponseMessage response = null;

                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    client.DefaultRequestHeaders.Authorization =
                        CreateBasicCredentials(
                            ServerProfile.GetInstance().REMOTE_USER_NAME,
                            ServerProfile.GetInstance().REMOTE_USER_PASSWORD);
                    client.Timeout = new System.TimeSpan(2, 0, 0);
                    client.BaseAddress = new Uri(host);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.Post<K>(address, obj);

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}", address);
                        return default(T);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}",
                            response.StatusCode, response.ReasonPhrase);
                        return default(T);
                    }
                    else
                    {
                        message = string.Empty;
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }
        }


        private static T Put<T, K>(string host, string path, K obj, out string message)
        {
            string address = host + path;

            try
            {
                HttpResponseMessage response = null;

                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    client.DefaultRequestHeaders.Authorization =
                        CreateBasicCredentials(
                            ServerProfile.GetInstance().REMOTE_USER_NAME,
                            ServerProfile.GetInstance().REMOTE_USER_PASSWORD);
                    client.Timeout = new System.TimeSpan(2, 0, 0);
                    client.BaseAddress = new Uri(host);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    response = client.PutAsJsonAsync<K>(address, obj).Result;

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}", address);
                        return default(T);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}",
                            response.StatusCode, response.ReasonPhrase);
                        return default(T);
                    }
                    else
                    {
                        message = string.Empty;
                        return response.Content.ReadAsAsync<T>().Result;
                    }
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }
        }

        static AuthenticationHeaderValue CreateBasicCredentials(string userName, string password)
        {
            string toEncode = userName + ":" + password;
            // The current HTTP specification says characters here are ISO-8859-1.
            // However, the draft specification for the next version of HTTP indicates this encoding is infrequently
            // used in practice and defines behavior only for ASCII.
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            byte[] toBase64 = encoding.GetBytes(toEncode);
            string parameter = Convert.ToBase64String(toBase64);

            return new AuthenticationHeaderValue("Basic", parameter);
        }

        /// <summary>
        /// HttpGet + BasicCredentials
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="host"></param>
        /// <param name="url"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T GetRemote<T>(string host, string url, out string message)
        {
            try
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    HttpResponseMessage response = null;
                    client.BaseAddress = new Uri(host);

                    client.Timeout = new System.TimeSpan(2, 0, 0);

                    response = client.Get(host + url);

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}{1}", host, url);
                        return default(T);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}", response.StatusCode, response.ReasonPhrase);
                        return default(T);
                    }

                    message = string.Empty;

                    return response.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }

        }


        /// <summary>
        /// 取得網頁上的字串資訊
        /// </summary>
        /// <param name="url"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetContentStr(string url, out string message)
        {
            try
            {
                using (HttpClient client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                                             | DecompressionMethods.Deflate
                }))
                {
                    HttpResponseMessage response = null;
                 
                    client.Timeout = new System.TimeSpan(2, 0, 0);

                    response = client.Get(url);

                    if (response == null)
                    {
                        message = string.Format("執行結果無回傳值: {0}{1}", ServerProfile.GetInstance().REMOTE_API_SITE, url);
                        return default(string);
                    }
                    else if (response.IsSuccessStatusCode == false)
                    {
                        message = string.Format("回傳錯誤 :{0:D} {1}", response.StatusCode, response.ReasonPhrase);
                        return default(string);
                    }

                    message = "ok";

                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    if (ex.InnerException.InnerException != null)
                        if (ex.InnerException.InnerException is System.Net.WebException)
                            throw ex.InnerException.InnerException;
                        else
                            throw ex.InnerException;
                    else
                        throw ex.InnerException;
                else
                    throw ex;
            }

        }
    }
}
