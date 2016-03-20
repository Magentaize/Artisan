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


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PostDetailPageVM.PostItem = e.Parameter as PostListItem;
            await PostDetailPageVM.GetAuthorOtherPostAsync();
            //var dataContext = e.Parameter as IPostListItem;
            //try
            //{
            //    PostDetailPageVM.PostItem.User.NickName = dataContext.User.NickName;
            //    PostDetailPageVM.PostItem.Work.Pic = dataContext.Work.Pic;
            //}
            //catch (NullReferenceException ex)
            //{
            //    //TODO
            //}
        }

        private void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (Grid)sender;
            var dataContext = new PostListItem
            {
                Work = PostDetailPageVM.PostItem.Work,
                User = PostDetailPageVM.PostItem.User
                //User = new User { NickName = PostDetailPageVM.PostItem.User.NickName, },
            };
            //var img =VisualTree.FindVisualElement<Image>(grid);
            //var str = ((BitmapImage)img.Source).UriSource.ToString();
            this.Frame.Navigate(typeof(PostDetail), dataContext);
        }

    }
}
