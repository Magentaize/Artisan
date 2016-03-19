using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Artisan.Model;
using Artisan.Toolkit;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Artisan.Toolkit.Helper;
using Artisan.Toolkit.Net;
using Artisan.Toolkit.Utilities;
using Artisan.View.NewsPage;
using Artisan.ViewModel;
using Artisan.View.AboutMePage;
using Windows.ApplicationModel.DataTransfer;
using Artisan.Interface;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Artisan.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel MainPageVm { get; } = new MainPageViewModel();
        private ThemeColor ThemeColorVM { get; set; }=new ThemeColor();

        public MainPage()
        {
            this.InitializeComponent();

            //MessageBox.Show(HttpRequest.HttpGet(@"http://www.kanglesoft.com/forum.phpzdv").Result);
            
            this.NavigationCacheMode = NavigationCacheMode.Required;
            CommonFrame.Navigate(typeof(FollowedPage));

            FollowedBtn.Tapped += NewsChildBtnTapped;
            TweetBtn.Tapped += NewsChildBtnTapped;
            CommentBtn.Tapped += NewsChildBtnTapped;
            PraisedBtn.Tapped += NewsChildBtnTapped;

            MyWorkNumber.Tapped += delegate (object obj, TappedRoutedEventArgs e) { this.Frame.Navigate(typeof(MyWorkPage)); };
            AttentionNumber.Tapped += delegate (object obj, TappedRoutedEventArgs e) { this.Frame.Navigate(typeof(MyWorkPage)); };
            TweetNumber.Tapped += delegate (object obj, TappedRoutedEventArgs e) { this.Frame.Navigate(typeof(MyWorkPage)); };


            IntroductionEditDialog EditIntroduction = new IntroductionEditDialog();
            Introduction.Tapped += async delegate (object sedner, TappedRoutedEventArgs e)
              {
                  await EditIntroduction.ShowAsync();
              };

            Upload.Click += Upload_Click;
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            SignPageViewModel spvm = new SignPageViewModel();
            await spvm.GetTimeLineAsync(1);
        }
        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
        }

        private void Setting_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PersonalTweetPage), "ms-appx:///Assets/img/1.jpg");
        }

        private void ItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (Grid) sender;
            var dataContext = grid.DataContext;
            //var img = VisualTree.FindVisualElement<Image>(grid);
            //var str = ((BitmapImage) img.Source).UriSource.ToString();
            //this.Frame.Navigate(typeof (PostDetail), str);
            this.Frame.Navigate(typeof(PostDetail), dataContext);
        }

        private void HomePageAuthorDeatiltapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AuthorDetail));
        }

        private void SettingButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutMeSetting));
        }

        private int _previousSelectBtn = 0;

        private void NewsChildBtnTapped(object sender, TappedRoutedEventArgs e)
        {
            Button btn = (Button) sender;
            int CurrentSelectBtn = Grid.GetColumn(btn);
            if (CurrentSelectBtn != _previousSelectBtn)
            {
                var brush = new SolidColorBrush(Colors.White);
                brush.Opacity = 0.4;
                switch (_previousSelectBtn)
                {
                    case 0: FollowedBtn.Background = brush;
                        break;
                    case 1:
                        TweetBtn.Background = brush;
                        break;
                    case 2:
                        CommentBtn.Background = brush;
                        break;
                    case 3:
                        PraisedBtn.Background = brush;
                        break;
                }
                btn.Background=new SolidColorBrush(Colors.Transparent);
                _previousSelectBtn = CurrentSelectBtn;

                switch (CurrentSelectBtn)
                {
                    case 0:
                        CommonFrame.Navigate(typeof (FollowedPage));
                        break;
                    case 1:
                        CommonFrame.Navigate(typeof (AllTweetPage));
                        break;
                    case 2:
                        CommonFrame.Navigate(typeof (CommentMePage));
                        break;
                    case 3:
                        CommonFrame.Navigate(typeof (PraisedPage));
                        break;
                }
            }
        }

        //bool _chatPageBtnIsFirstOne = true;
        //private void NotificationTapped(object sender, TappedRoutedEventArgs e)
        //{
        //    if(_chatPageBtnIsFirstOne!=true)
        //    {
        //        var brush = new SolidColorBrush(Colors.White);
        //        brush.Opacity = 0.4;
        //        NotificationBtn.Background = new SolidColorBrush(Colors.Transparent);
        //        TweetBtn.Background = brush;
        //        CommonFrame.Navigate(typeof(ChatNotification));
        //        _chatPageBtnIsFirstOne = true;
        //    }
        //}

        //private void TweetTapped(object sender, TappedRoutedEventArgs e)
        //{
        //    if (_chatPageBtnIsFirstOne == true)
        //    {
        //        var brush = new SolidColorBrush(Colors.White);
        //        brush.Opacity = 0.4;
        //        TweetBtn.Background = new SolidColorBrush(Colors.Transparent);
        //        NotificationBtn.Background = brush;
        //        CommonFrame.Navigate(typeof(ChatTweet));
        //        _chatPageBtnIsFirstOne = false;
        //    }
        //}

        bool PraisedTxb = false;
        private void MainPagePostPraised_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock txb = (TextBlock)sender;

            if (PraisedTxb == false)
            {
                txb.Foreground = new SolidColorBrush(Colors.Crimson);
                PraisedTxb = true;
            }
            else
            {
                txb.Foreground = new SolidColorBrush(Colors.DarkGray);
                PraisedTxb = false;
            }
        }

        private void MainPagePostShare_Tapped(object sender, TappedRoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        private void MainPagePostShare_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
        }

        private void MainPagePostShare_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void MainPagePostComment_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
        }

        private void MainPagePostComment_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void MainPagePostComment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (CommentPage));
        }
    }

}
