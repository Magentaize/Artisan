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

namespace Artisan.ViewModel
{
    public class MainPageViewModel : NotifyPropertyObject
    {
        private Brush _themeColorBrush = new SolidColorBrush((Color) Application.Current.Resources["SystemAccentColor"]);

        private ObservableCollection<HomePivotListItem> _homePivotListItems =
            new ObservableCollection<HomePivotListItem>();

        private ObservableCollection<DiscoveryPivotListItem> _discoveryPivotListItems =
            new ObservableCollection<DiscoveryPivotListItem>();

        public Brush ThemeColorBrush
        {
            get { return _themeColorBrush; }
            set { UpdateProperty(ref _themeColorBrush, value); }
        }

        public ObservableCollection<HomePivotListItem> HomePivotListItems
        {
            get { return _homePivotListItems; }
            set
            {
                if (_homePivotListItems != value)
                {
                    UpdateProperty(ref _homePivotListItems, value);
                }
            }
        }

        public ObservableCollection<DiscoveryPivotListItem> DiscoveryPivotListItems
        {
            get { return _discoveryPivotListItems; }
            set
            {
                if (_discoveryPivotListItems != value)
                {
                    UpdateProperty(ref _discoveryPivotListItems, value);
                }
            }
        }

        public MainPageViewModel()
        {
            
            ////DiscoveryPivotListItems= new ObservableCollection<DiscoveryPivotListItem>();
            ////ThemeColorBrush = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
            //Random random = new Random((int) DateTime.Now.Ticks);
            //for (int j = 1; j <= 8;)
            //{
            //    string str = "../Assets/img/" + random.Next(1, 7).ToString() + ".jpg";
            //    HomePivotListItems.Add(new HomePivotListItem
            //    {
            //        Text = "IMG" + j.ToString() + ":0x" + Address(),
            //        CreatTime = DateFormat.GetFormattedTime(),
            //        Pics = str,
            //        User = new HomePivotListItemUser { Name = "Artist " + j.ToString(), },
            //    });

            //    DiscoveryPivotListItems.Add(new DiscoveryPivotListItem
            //    {
            //        Intro = "今日推荐:IMG" + j++.ToString(),
            //        Pic = str
            //    });
            //}
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
                        var result = await HttpWebPost.PostJsonToUriAsync(SigninUri, param);
                        if (result["result"].ToString() == "true")
                        {
                            UserInfo user = new UserInfo();
                            user.Name = userId;
                            user.Uid = result["uid"].GetString();
                            (App.Current as App).CurrentUser = user;
                        }
                    }
                }
                return false;
            }
            else return true;
        }

        private static string Address()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public static HomePivotListItem GetItem()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            HomePivotListItem item = null;
            int i = random.Next(1, 7);
            string str = "../Assets/img/" + i.ToString() + ".jpg";
            item = new HomePivotListItem
            {
                Text = "IMG" + i.ToString() + ":0x" + Address(),
                CreatTime = DateFormat.GetFormattedTime(),
                Pics = str,
                User = new HomePivotListItemUser { Name = "Artist " + i.ToString(),},
            };
            return item;
        }

        public async Task<bool> GetTimeLineAsync(int timeLinePage)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("page", timeLinePage.ToString());
            string TimeLineUri = ResourceLoader.GetForCurrentView().GetString("TimeLineUri");
            var result = await HttpWebPost.GetJsonStringFromUriAsync(TimeLineUri, param);
            //MessageBox.Show(result.ToString());
           var items = JsonObjectParser.ParseTimeLineItem(result);
            foreach(var item in items)
            {
                HomePivotListItems.Add(item);
            }
            return  HomePivotListItems != null;
        }

        internal async void Signout()
        {
            await HttpWebPost.PostDataToUriAsync(ResourceLoader.GetForCurrentView().GetString("SignoutUri"), null);
        }
    }
} 