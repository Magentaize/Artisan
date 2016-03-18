using Artisan.Toolkit;
using System;
using System.Collections.ObjectModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Model;
using Artisan.Toolkit.Helper;

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
            //DiscoveryPivotListItems= new ObservableCollection<DiscoveryPivotListItem>();
            //ThemeColorBrush = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
            Random random = new Random((int) DateTime.Now.Ticks);
            for (int j = 1; j <= 8;)
            {
                string str = "../Assets/img/" + random.Next(1, 7).ToString() + ".jpg";
                HomePivotListItems.Add(new HomePivotListItem
                {
                    AuthorName = "Artist " + j.ToString(),
                    Text = "IMG" + j.ToString() + ":0x" + Address(),
                    CreatTime = DateFormat.GetFormattedTime(),
                    Pics = str
                });

                DiscoveryPivotListItems.Add(new DiscoveryPivotListItem
                {
                    Intro = "今日推荐:IMG" + j++.ToString(),
                    Pic = str
                });
            }
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
                AuthorName = "Artist " + i.ToString(),
                Text = "IMG" + i.ToString() + ":0x" + Address(),
                CreatTime = DateFormat.GetFormattedTime(),
                Pics = str,
                User = new HomePivotListItemUser { Name = "Artist " + i.ToString(),},
            };
            return item;
        }

    }
} 