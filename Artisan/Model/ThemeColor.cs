using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Artisan.Toolkit;

namespace Artisan.Model
{
    public class ThemeColor:NotifyPropertyObject
    {
        private Brush _themeColorBrush = new SolidColorBrush((Color)Application.Current.Resources["SystemAccentColor"]);

        public Brush ThemeColorBrush
        {
            get { return _themeColorBrush; }
            set
            {
                if(_themeColorBrush!=value)
                    UpdateProperty(ref _themeColorBrush,value);
            }
        }
    }

}