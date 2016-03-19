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
            return token.Type == JTokenType.Null ? null : new UserInfoGeo { City = token["city"].ToString(), Province = token["province"].ToString() };
        }
        public static UserInfo ParseUserInfo(JToken token)
        {
            var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            return new UserInfo()
            {
                Article = int.Parse(token["article_num"].ToString()),
                Fans = int.Parse(token["fans_num"].ToString()),
                Follows = int.Parse(token["follows_num"].ToString()),
                Gender = int.Parse(token["gender"].ToString()),
                Geo = ParseUserGeo(token["geo"]),
                Intro = token["intro"].ToString(),
                Name = token["nickname"].ToString(),
                Pic = hostUri + token["head_pic"].ToString(),
                Works = int.Parse(token["works_num"].ToString())
            };
        }
        public static string[] ParseStringArray(JToken token)
        {
            var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            if (token.Type == JTokenType.Null) return null;

            var array = token.ToArray();
            string[] result = new string[array.Count()];
            for (int i = 0; i < array.Count(); ++i)
            {
                result[i] = hostUri + array[i].ToString();
            }
            
            return result;

        }
        public static ImageSize ParseSize(JToken token)
        {
            string[] array = token.ToString().Split('x');
            if(array != null)
            {
                return new ImageSize
                {
                    Height = int.Parse(array[0]),
                    Width = int .Parse(array[1])
                };
            }
            return null;
        }
        public static Work ParseWork(JToken token)
        {
            if (token.Type == JTokenType.Null) return null;
            return new Work()
            {
                CommentsNum = int.Parse(token["comments_num"].ToString()),
                LikesNum = int.Parse(token["likes_num"].ToString()),
                Intro = token["intro"].ToString(),
                Name = token["name"].ToString(),
                Pics = ParseStringArray(token["pics"]),
                Sell = int.Parse(token["sell"].ToString()),
                Size = ParseSize(token["size"]),
                Uid = token["uid"].ToString(),
                Wid = token["_id"].ToString()
            };
        }
        //public static 
        public static HomePivotListItemUser ParseTimeLineUser(JToken token)
        {
            var user = new HomePivotListItemUser();
            var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            if (token.Type != JTokenType.Null)
            {
                user.Name = token["nickname"].ToString();
                user.Uid = token["_id"].ToString();
                user.Gender = int.Parse(token["gender"].ToString());
                user.Geo = ParseUserGeo(token["geo"]);
                user.Intro = token["intro"].ToString();
                user.Pic = hostUri + token["head_pic"].ToString();
            }
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

        /// <summary>
        /// 对应首页发现数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ObservableCollection<DiscoveryPivotListItem> ParseDiscoveryItem(string data)
        {
            var result = new ObservableCollection<DiscoveryPivotListItem>();
            var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            var jToken = JObject.Parse(data).First.First["works"];//这里直接取了works数组，没考虑users数组 那么user是什么鬼
            foreach (var item in jToken)
            {
                result.Add(new DiscoveryPivotListItem
                {
                    Intro = item["intro"].ToString(),
                    Pic = hostUri + ((JValue)item["pics"][0]).ToString(),                
                });
            }
            return result;
        } 
    }
}
