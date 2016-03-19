﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;

namespace Artisan.Toolkit.Net
{
    public class HttpWebPost//懒得改名字了
    {
        private static CookieContainer cookies = new CookieContainer();
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
                    return array;
                }
                return null;
            }
            catch(Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 使用html方式发送get参数,返回jsonObject
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static async Task<JsonObject> GetJsonFromUriAsync(string uri, Dictionary<string, string> paramters)
        {
            StringBuilder sb = new StringBuilder(uri);
            sb.Append("?");
            foreach(var param in paramters)
            {
                sb.Append($"{param.Key}={param.Value}&");
            }
            sb.Remove(sb.Length - 1, 1);
            HttpWebRequest request = HttpWebRequest.CreateHttp(sb.ToString());
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
    }
}
