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
using Newtonsoft.Json.Linq;
using Artisan.Toolkit.Helper;

namespace Artisan.ViewModel
{
    public class SignPageViewModel:NotifyPropertyObject
    {
        private bool? savePassword;
        public bool? SavePassword
        {
            get
            {
                    return savePassword;
            }
            set
            {
                if (value == false)
                    UpdateProperty(ref autoSignin, value,"AutoSignin");
                UpdateProperty(ref savePassword, value);
            }
        }
        private bool? autoSignin;
        public bool? AutoSignin
        {
            get
            {
                return autoSignin;
            }
            set
            {
                if (value == true)
                    UpdateProperty(ref savePassword, value,"SavePassword");
                UpdateProperty(ref autoSignin,value);
            }
        }


        public SignPageViewModel()
        {
           SavePassword = UserConfiguration.Settings["SavePassword"] as bool? ?? true;
            AutoSignin = UserConfiguration.Settings["AutoSignin"] as bool? ?? true;
     
        }
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


        public async Task<string> SigninAsync(string userId, string Password)
        {
            if (userId == null || Password == null) return "用户名或密码无效";
            UserConfiguration.Settings["SavePassword"] = SavePassword;
            UserConfiguration.Settings["AutoSignin"] = AutoSignin;

            if (SavePassword.Value)
            {
                UserConfiguration.Settings["Password"] = Password;
                UserConfiguration.Settings["Username"] = userId;

            }
            else
            {
                UserConfiguration.Settings["Password"] = "";
            }

            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", userId);
            param.Add("password", Password);
            string SigninUri = ResourceLoader.GetForCurrentView().GetString("SigninUri");
            var result = await HttpWebPost.PostJsonToUriAsync(SigninUri, param);
            if (result["result"].ToString() == "true")
            {
                UserInfo user = new UserInfo();
                user.Name = userId;
                user.Uid = result["uid"].GetString();
                (App.Current as App).CurrentUser = user;
                return null;
            }
            else
            {
                return result["reason"].GetString();
            }
        }

        //public async Task<JsonObject> GetTimeLineAsync(int timeLinePage)
        //{
        //    Dictionary<string, string> param = new Dictionary<string, string>();
        //    param.Add("page", timeLinePage.ToString());
        //    string TimeLineUri = ResourceLoader.GetForCurrentView().GetString("TimeLineUri");
        //    var result = await HttpWebPost.GetJsonFromUriAsync(TimeLineUri, param);
        //    var jObj = JsonObject.Parse(result.ToString());
        //    //var result = await HttpWebPost.PostJsonToUriAsync(TimeLineUri, param);
        //    MessageBox.Show(result.ToString());
        //    return result ?? null;
        //}

    }
}
