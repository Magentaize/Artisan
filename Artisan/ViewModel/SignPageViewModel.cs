using Artisan.Toolkit.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Artisan.Toolkit;
using Artisan.Model;
using Windows.Data.Json;

namespace Artisan.ViewModel
{
    public class SignPageViewModel
    {
        public async Task<string> SignupAsync(string userId, string Password)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", userId);
            param.Add("password", Password);
            param.Add("nickname", userId);
            string SignupUri = ResourceLoader.GetForCurrentView().GetString("SignupUri");
            var result = await HttpWebPost.PostJsonToUriAsync(SignupUri, param);
            if (result["result"].ToString() == "true") return null;
            else return result["reason"].ToString();
        }

        public async Task<UserInfo> SigninAsync(string userId, string Password)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", userId);
            param.Add("password", Password);
            string SigninUri = ResourceLoader.GetForCurrentView().GetString("SigninUri");
            var result = await HttpWebPost.PostJsonToUriAsync(SigninUri, param);
            if (result["result"].ToString() == "true") return null;
            else return new UserInfo();//未完成
        }

    }
}
