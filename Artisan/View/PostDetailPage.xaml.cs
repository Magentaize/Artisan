using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Artisan.Model;
using Artisan.ViewModel;
using Artisan.Toolkit.Helper;
using Windows.UI.Xaml.Media.Imaging;
using Artisan.Interface;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Artisan.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PostDetail : Page
    {
        PostDetailPageViewModel PostDetailPageVM = new PostDetailPageViewModel();

        public PostDetail()
        {
            this.InitializeComponent();

            ShareBtn.Tapped += ShareBtnTapped;
        }

        #region AppBarButton

        private void ShareBtnTapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }
        #endregion


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var dataContext = e.Parameter as IHomePivotListItem;
            PostDetailPageVM.Name = dataContext.User.Name;
            PostDetailPageVM.Pics = dataContext.Pics;
        }

        private void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (Grid)sender;
            var dataContext = new HomePivotListItem
            {
             Pics= (grid.DataContext as AuthorOtherPostListItem).PostSource,
             User=new HomePivotListItemUser { Name = PostDetailPageVM.Name,},
            };
            //var img =VisualTree.FindVisualElement<Image>(grid);
            //var str = ((BitmapImage)img.Source).UriSource.ToString();
            this.Frame.Navigate(typeof(PostDetail), dataContext);
        }
    
    }
}
