using Artisan.Toolkit;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Resources;
using Windows.Data.Json;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Model;
using Artisan.Toolkit.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Artisan.Toolkit.Net;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;
using Artisan.View;

namespace Artisan.ViewModel
{
    public class MainPageViewModel : NotifyPropertyObject
    {
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                UpdateProperty(ref _currentPage, value);
            }
        }
        private ObservableCollection<HomePivotListItem> _homePivotList =
            new ObservableCollection<HomePivotListItem>();

        private ObservableCollection<DiscoveryPivotListItem> _discoveryPivotList =
            new ObservableCollection<DiscoveryPivotListItem>();

        public ObservableCollection<HomePivotListItem> HomePivotList
        {
            get { return _homePivotList; }
            set { UpdateProperty(ref _homePivotList, value); }
        }

        public ObservableCollection<DiscoveryPivotListItem> DiscoveryPivotList
        {
            get { return _discoveryPivotList; }
            set { UpdateProperty(ref _discoveryPivotList, value); }
        }

        internal async Task<bool> AutoLoginAsync()
        {
            if ((App.Current as App).CurrentUser == null)
            {
                bool? AutoLogin = UserConfiguration.Settings["AutoSignin"] as bool?;
                if (AutoLogin != null)
                {
                    if (AutoLogin == true)
                    {
                        string userId = UserConfiguration.Settings["Username"] as string;
                        string Password = UserConfiguration.Settings["Password"] as string;
                        if (userId == null || Password == null) return false;
                        Dictionary<string, string> param = new Dictionary<string, string>();
                        param.Add("username",userId);
                        param.Add("password", Password);
                        string SigninUri = ResourceLoader.GetForCurrentView().GetString("SigninUri");
                        string HostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
                        var result = await HttpWebPost.PostJsonToUriAsync(HostUri + SigninUri, param);
                        if (result == null) return false;

                        if (result["result"].ToString() == "true")
                        {
                            UserInfo user = new UserInfo();
                            user.NickName = userId;
                            user.Uid = result["uid"].GetString();
                            (App.Current as App).CurrentUser = user;
                            return true;
                        }
                    }
                }
                return false;
            }
            else return true;
        }

        public async Task<bool> GetTimeLineAsync(bool forceRefresh = true)
        {
            if (HomePivotList.Count != 0 && !forceRefresh) return false;
            HomePivotList.Clear();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("page", CurrentPage.ToString());
            var TimeLineUri = ResourceLoader.GetForCurrentView().GetString("TimeLineUri");
            var HostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
            var result = await HttpWebPost.GetJsonStringFromUriAsync(HostUri + TimeLineUri, param);
            if (result == null) return false;

            var items = JsonObjectParser.ParseTimeLineItem(result);
            foreach (var item in items)
            {
                HomePivotList.Add(item);
            }
            return  HomePivotList != null;
        }

        public async Task<bool> GetDiscoveryAsync(bool forceRefresh = true)
        {
            if (DiscoveryPivotList.Count != 0 && !forceRefresh) return false;
            DiscoveryPivotList.Clear();
            string targetUri = ResourceLoader.GetForCurrentView().GetString("HostUri") + ResourceLoader.GetForCurrentView().GetString("FindUri");
            var result = await HttpWebPost.GetJsonStringFromUriAsync(targetUri);
            if (result == null) return false;

            var items = JsonObjectParser.ParseDiscoveryItem(result);
            foreach (var item in items)
            {
                DiscoveryPivotList.Add(item);
            }
            return DiscoveryPivotList != null;
        }

        internal async Task<string> UploadImage(StorageFile file)
        {
            UserInfo user = (App.Current as App).CurrentUser;
            if (user == null) return "未登录或登录失效，请尝试重新登录";
            if (user.Uid == null) return "未登录或登录失效，请尝试重新登录";
            BitmapImage bi = new BitmapImage();
            bi.SetSource(await file.OpenReadAsync());

            int height = bi.PixelHeight;
            int width = bi.PixelWidth;
            UploadDialog dia = new UploadDialog();
            var res = await dia.ShowAsync();
            if (res != Windows.UI.Xaml.Controls.ContentDialogResult.Primary) return "用户取消上传";
            
            Dictionary<string, string> param = new Dictionary<string, string>();
            Dictionary<string, StorageFile> attach = new Dictionary<string, StorageFile>();
            param.Add("uid", user.Uid);
            param.Add("name", dia.WorkTitle);
            param.Add("size", $"{height}X{width}");
            param.Add("sell", "0");
            param.Add("intro",dia.Intro);

            attach.Add("pic", file);
            string targetUri = ResourceLoader.GetForCurrentView().GetString("HostUri")
                +   ResourceLoader.GetForCurrentView().GetString("update_timelineUri");
            string result = await HttpWebPost.PostMutipartDataToUriAsync(targetUri, param, attach);
            if (result == null) return "服务器没有响应";
            if (result.Contains("true"))
                return null;     
            return JObject.Parse(result)["reason"].ToString();
            //Guid id = null;
            //switch (file.FileType)
            //{
            //    case "png":id = BitmapEncoder.PngEncoderId; break;
            //    case "bmp":id = BitmapEncoder.BmpEncoderId; break;
            //    case "jpeg":
            //    case "jpg":id = BitmapEncoder.JpegEncoderId; break;
            //}
           // var encoder = await BitmapEncoder.CreateAsync(id, await file.OpenReadAsync());
            
        }

        internal async void Signout()
        {
            await HttpWebPost.PostDataToUriAsync(ResourceLoader.GetForCurrentView().GetString("HostUri") + ResourceLoader.GetForCurrentView().GetString("SignoutUri"), null);
        }

        internal async Task<bool?> RefreshCurrentView(int currentIndex, bool forceRefresh = true)
        {
            switch (currentIndex)
            {
                case 0: return await GetTimeLineAsync(forceRefresh);
                case 1:return await GetDiscoveryAsync(forceRefresh);
                default:return null;
            }
        }
    }
} 