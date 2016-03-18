using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Artisan.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AuthorDetail : Page
    {
        public AuthorDetail()
        {
            this.InitializeComponent();
        }

        private void DoAttentionTapped(object sender, TappedRoutedEventArgs e)
        {
            var btn = (Button)sender;
            if (btn.Content.ToString() == "关注")
                btn.Content = "取消关注";
            if (btn.Content.ToString() == "取消关注")
                btn.Content = "关注";
        }

        private void TweetTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PersonalTweetPage));
        }
    }
}
