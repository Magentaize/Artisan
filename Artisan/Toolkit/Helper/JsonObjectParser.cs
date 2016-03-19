using Artisan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Artisan.Toolkit.Helper
{
    public class JsonObjectParser
    {
        public UserInfoGeo ParseUserInfoGeo(IJsonValue data)
        {
            return new UserInfoGeo();
        }
        public UserInfo ParseUserInfo(IJsonValue data)
        {
            return new UserInfo();
        }
        public HomePivotListItemUser ParseTimeLineUser(IJsonValue data)
        {
            return new HomePivotListItemUser();
        }
        /// <summary>
        /// 对应首页时间线数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ObservableCollection<HomePivotListItem> ParseTimeLineItem(IJsonValue data)
        {
            var result = new ObservableCollection<HomePivotListItem>();
            return result;
        }
    }
}
