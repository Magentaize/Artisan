using Artisan.Toolkit.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Artisan.Toolkit;

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
            var result = await HttpWebPost.PostDataToUriAsync(SignupUri, param);
            if (result["result"] == "true") return null;
            else return result["reason"];
        }

        public async Task<Dictionary<string, string> > SigninAsync(string userId, string Password)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("username", userId);
            param.Add("password", Password);
            string SigninUri = ResourceLoader.GetForCurrentView().GetString("SigninUri");
            var result = await HttpWebPost.PostDataToUriAsync(SigninUri, param);
            if (result["result"] == "true") return null;
            else return result;
        }

        public async Task<Dictionary<string, string>> GetTimeLineAsync(int timeLinePage)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("page", timeLinePage.ToString());
            string TimeLineUri = ResourceLoader.GetForCurrentView().GetString("TimeLineUri");
            var result = await HttpWebPost.PostDataToUriAsync(TimeLineUri, param);
            MessageBox.Show(result.ToString());
            return result ?? null;
        }
    }
}
