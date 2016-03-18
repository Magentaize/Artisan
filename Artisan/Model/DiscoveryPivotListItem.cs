using Artisan.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Interface;

namespace Artisan.Model
{
    public class DiscoveryPivotListItem:NotifyPropertyObject, IDiscoveryPivotListItem
    {
        public DiscoveryPivotListItem()
        {
            ThemeColorBrush = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);
        }

        private Brush _themeColorBrush;

        private int _id;
        public int Id
        {
            get { return _id; }
            set { UpdateProperty(ref _id, value);}
        }

        private string _intro;
        public string Intro
        {
            get { return _intro; }
            set { UpdateProperty(ref _intro, value); }
        }

        private string _pic;
        public string Pic
        {
            get { return _pic; }
            set { UpdateProperty(ref _pic, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title;}
            set { UpdateProperty(ref _title, value);}
        }
        public Brush ThemeColorBrush
        {
            get { return _themeColorBrush; }
            set { UpdateProperty(ref _themeColorBrush, value); }
        }


    }
}