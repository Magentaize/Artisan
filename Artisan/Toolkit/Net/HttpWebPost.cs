using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Artisan.Toolkit.Net
{
    public class HttpWebPost
    {
        /// <summary>
        /// 向指定uri post一组参数
        /// </summary>
        /// <param name="paramters">要post的参数</param>
        /// <returns></returns>
        public static async Task<Dictionary<string, string>> PostDataToUriAsync(string uri, Dictionary<string, string> paramters)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var param in paramters)
            {
                sb.Append($"{param.Key}={param.Value}&");
            }
           return await PostDataToUriAsync(uri, sb.ToString());
        }
        public static async Task<Dictionary<string, string>> PostDataToUriAsync(string uri, string paramters)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.Method = "POST";
            try {
                using (var stream = await request.GetRequestStreamAsync())
                {
                    byte[] data = Encoding.UTF8.GetBytes(paramters);
                    stream.Write(data, 0, data.Length);
                }

                var response = await request.GetResponseAsync();

                string result;
                using (var stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                }
                if (result != null)
                {
                    var array = Windows.Data.Json.JsonObject.Parse(result);
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach(var data in array)
                    {
                        dict.Add(data.Key, data.Value.ToString());
                    }
                    return dict;
                }
                return null;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
