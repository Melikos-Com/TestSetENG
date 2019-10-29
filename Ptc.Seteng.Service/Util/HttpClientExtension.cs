using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public static class HttpClientExtension
    {
        public static HttpResponseMessage Post<T>(this HttpClient client, string requestUri, T value)
        {
            try
            {
                // List all products.
                HttpResponseMessage response = client.PostAsJsonAsync<T>(requestUri, value).Result;
                if (response.IsSuccessStatusCode == false)
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);

                    return null;
                }
                return response;
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


        public static HttpResponseMessage Get(this HttpClient client, string url, params string[] str)
        {
            try
            {

                // List all products.
                HttpResponseMessage response = client.GetAsync(String.Format(url, str)).Result;  // Blocking call!  


                return response;
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
