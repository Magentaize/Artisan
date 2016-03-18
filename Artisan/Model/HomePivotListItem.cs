using Artisan.Toolkit;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Interface;

namespace Artisan.Model
{
    public class HomePivotListItem:NotifyPropertyObject,IHomePivotListItem
    {
        public HomePivotListItem()
        {
            ThemeColorBrush = new SolidColorBrush((Color) Application.Current.Resources["SystemAccentColor"]);
        }

        private Brush _themeColorBrush;
        private string _authorName;
        private string _postTime;
        private string _postInfo;
        private string _postSource;

        public Brush ThemeColorBrush
        {
            get { return _themeColorBrush; }
            set { UpdateProperty(ref _themeColorBrush, value); }
        }

        public string AuthorName
        {
            get{ return _authorName; }
            set{ UpdateProperty(ref _authorName, value); }
        }

        public string PostTime
        {
            get { return _postTime; }
            set { UpdateProperty(ref _postTime, value); }
        }

        public string PostInfo
        {
            get { return _postInfo; }
            set { UpdateProperty(ref _postInfo, value); }
        }

        public string PostSource
        {
            get { return _postSource; }
            set { UpdateProperty(ref _postSource, value); }
        }
    }
}