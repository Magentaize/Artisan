using Artisan.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Data.Json;

namespace Artisan.Toolkit.Helper
{
    public class JsonObjectParser
    {
        public static UserInfoGeo ParseUserGeo(JToken token)
        {
            return token.Type ==  JTokenType.Null ? null : new UserInfoGeo { City = token["city"].ToString(), Province = token["province"].ToString() };
        }
        public static UserInfo ParseUserInfo(IJsonValue data)
        {
            return new UserInfo();
        }
        //public static 
        public static HomePivotListItemUser ParseTimeLineUser(JToken token)
        {
            var user = new HomePivotListItemUser();
            user.Name = token["nickname"]?.ToString();
            user.Uid = token["_id"]?.ToString();
            user.Gender = int.Parse(token["gender"]?.ToString());
            user.Geo = ParseUserGeo(token["geo"]);
            user.Intro = token["intro"]?.ToString();
            user.Pic = token["head_pic"]?.ToString();
            return user;
        }
        /// <summary>
        /// 对应首页时间线数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  ObservableCollection<HomePivotListItem> ParseTimeLineItem(string data)
        {
            var result = new ObservableCollection<HomePivotListItem>();

            var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            var jToken = JObject.Parse(data).First.First;
            foreach (var item in jToken)
            {
                result.Add(new HomePivotListItem
                {
                    Text = item["work"]["intro"].ToString(),
                    CreatTime = item["post_time"].ToString(),
                    Pics = hostUri + ((JValue)item["work"]["pics"][0]).ToString(),
                    User = ParseTimeLineUser(item["user"])
                });
            }

            return result;
        }
    }
}
