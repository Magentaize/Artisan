using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Newtonsoft.Json.Linq;
using Windows.Storage;

namespace Artisan.Toolkit.Net
{
    public class HttpWebPost//懒得改名字了
    {
        public static CookieContainer cookies = new CookieContainer();

        /// <summary>
        /// 向指定uri post一组参数
        /// </summary>
        /// <param name="paramters">要post的参数</param>
        /// <returns></returns>
        public static async Task<JsonObject> PostJsonToUriAsync(string uri, Dictionary<string, string> paramters)
        {
    
            JsonObject jsonObj = new JsonObject();
            foreach (var param in paramters)
            {
                jsonObj.Add(param.Key, JsonValue.CreateStringValue(param.Value));
            }
           return await PostDataToUriAsync(uri, jsonObj.ToString());
        }
        public static async Task<JsonObject> PostDataToUriAsync(string uri, string paramters)
        {

            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            request.ContentType = "application/json";
            request.CookieContainer = cookies;
            request.Method = "POST";
            try {
                if (paramters != null)
                {
                    using (var stream = await request.GetRequestStreamAsync())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(paramters);
                        stream.Write(data, 0, data.Length);
                    }

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
                    return array;
                }
                return null;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        public static async Task<string> PostMutipartDataToUriAsync(string uri, Dictionary<string, string> paramters, Dictionary<string, StorageFile> attachs)
        {
            HttpWebRequest request = HttpWebRequest.CreateHttp(uri);
            string boundary = "x_-" + DateTime.Now.Ticks.ToString("x");//分隔符

            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.CookieContainer = cookies;
            request.Method = "POST";
            try
            {
                using (var stream = await request.GetRequestStreamAsync())
                {
                    StringBuilder sb = new StringBuilder();
                    if (paramters != null)
                    {                    
                        foreach (var param in paramters)
                        {
                            sb.Append($"--{boundary}\r\n");
                            sb.Append($"Content-Disposition: form-data; name=\"{param.Key}\"\r\n\r\n");
                            sb.Append($"{param.Value}\r\n");
                        }
                        //参数部分数据
                        byte[] paramData = Encoding.UTF8.GetBytes(sb.ToString());
                        stream.Write(paramData, 0, paramData.Length);
                    }
                    foreach (var attach in attachs)
                    {
                        sb.Clear();
                        sb.Append($"\r\n--{boundary}\r\n");
                        sb.Append($"Content-Disposition: form-data; name=\"{attach.Key}\"; filename=\"{attach.Value.Name}\"\r\n");
                        sb.Append($"Content-Type: application/octet-stream\r\n\r\n");
                        //文件头
                        byte[] FileHeader = Encoding.UTF8.GetBytes(sb.ToString());
                        stream.Write(FileHeader, 0, FileHeader.Length);
                        //文件数据               
                        var fileStream = await attach.Value.OpenStreamForReadAsync();
                        byte[] attachData = new byte[2048];
                        int length;
                        while((length = fileStream.Read(attachData, 0, attachData.Length)) != 0)
                        {
                            stream.Write(attachData, 0, length);
                        }
                    }
                    //结束标记
                    byte[] endboundary = Encoding.UTF8.GetBytes($"\r\n--{boundary}--\r\n");
                    stream.Write(endboundary, 0, endboundary.Length);

                }             

                var response = await request.GetResponseAsync();

                string result;
                using (var stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        /// <summary>
        /// 使用html方式发送get带参数,返回jsonObject
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static async Task<JsonObject> GetJsonFromUriAsync(string uri, Dictionary<string, string> paramters)
        {
            HttpWebRequest request;
            if (paramters != null)
            {
                StringBuilder sb = new StringBuilder(uri);
                sb.Append("?");
                foreach (var param in paramters)
                {
                    sb.Append($"{param.Key}={param.Value}&");
                }
                sb.Remove(sb.Length - 1, 1);
                request = HttpWebRequest.CreateHttp(sb.ToString());
            }
            request = HttpWebRequest.CreateHttp(uri);
            request.CookieContainer = cookies;
            request.Method = "GET";
            try {
                var response = await request.GetResponseAsync();
                string result;
                using (var stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                }
                var obj = JsonObject.Parse(result);
                return obj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 使用html方式发送get带参数,返回string
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static async Task<string> GetJsonStringFromUriAsync(string uri, Dictionary<string, string> paramters)
        {
            StringBuilder sb = new StringBuilder(uri);
            sb.Append("?");
            foreach (var param in paramters)
            {
                sb.Append($"{param.Key}={param.Value}&");
            }
            sb.Remove(sb.Length - 1, 1);
            HttpWebRequest request = HttpWebRequest.CreateHttp(sb.ToString());
            request.CookieContainer = cookies;
            request.Method = "GET";
            try
            {
                var response = await request.GetResponseAsync();
                string result;
                using (var stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 使用html方式发送get不带参数,返回string
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> GetJsonStringFromUriAsync(string uri)
        {
            StringBuilder sb = new StringBuilder(uri);
            HttpWebRequest request = HttpWebRequest.CreateHttp(sb.ToString());
            request.CookieContainer = cookies;
            request.Method = "GET";
            try
            {
                var response = await request.GetResponseAsync();
                string result;
                using (var stream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    result = sr.ReadToEnd();
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
