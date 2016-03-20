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
using Windows.Data.Json;
using Artisan.Interface;
using Newtonsoft.Json.Linq;
using Windows.UI.Popups;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Artisan.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel MainPageVm { get; } = new MainPageViewModel();

        public MainPage()
        {
            if (DateTime.Today.DayOfYear < 82)
            this.InitializeComponent();
           
            this.NavigationCacheMode = NavigationCacheMode.Required;
            CommonFrame.Navigate(typeof(FollowedPage));

            FollowedBtn.Tapped += NewsChildBtnTapped;
            TweetBtn.Tapped += NewsChildBtnTapped;
            CommentBtn.Tapped += NewsChildBtnTapped;
            PraisedBtn.Tapped += NewsChildBtnTapped;

            var myWorkPageEditDialog = new MyWorkPage();
            MyWorkNumber.Tapped +=
                async delegate(object sedner, TappedRoutedEventArgs e)
                {
                    await myWorkPageEditDialog.ShowAsync();
                };

            AttentionNumber.Tapped +=
                async delegate (object sedner, TappedRoutedEventArgs e)
                {
                    await myWorkPageEditDialog.ShowAsync();
                };

            TweetNumber.Tapped +=
                async delegate (object sedner, TappedRoutedEventArgs e)
                {
                    await myWorkPageEditDialog.ShowAsync();
                };
            Gallery.Click +=
                async delegate (object sedner, RoutedEventArgs e)
                {
                    await myWorkPageEditDialog.ShowAsync();
                };

            var EditIntroduction = new IntroductionEditDialog();
            Introduction.Tapped += async delegate (object sedner, TappedRoutedEventArgs e)
              {
                  await EditIntroduction.ShowAsync();
              };

            Upload.Click += Upload_Click;
        }

        private bool _isFirstNavigatedToMainPage = true;
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (_isFirstNavigatedToMainPage)
            {
               
                //var success = await MainPageVm.GetTimeLineAsync();
                //var success2 = await MainPageVm.GetDiscoveryAsync();
                _isFirstNavigatedToMainPage = false;
            }
            bool result = await MainPageVm.AutoLoginAsync();
            if((App.Current as App).CurrentUser != null)
            {
                ToSignPage.Visibility = Visibility.Collapsed;
                ProfilePanel.Visibility = Visibility.Visible;
            }
        }
        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.VideosLibrary;
            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            StorageFile file = await picker.PickSingleFileAsync();
            string result = await MainPageVm.UploadImage(file);
            MessageDialog dialog = new MessageDialog(result ?? "上传成功");
            await dialog.ShowAsync();
        }

        private void Setting_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PersonalTweetPage), "ms-appx:///Assets/img/1.jpg");
        }

        private void TimeLineListItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (Grid)sender;
            var dataContext =
                (PostListItem) (((Image) VisualTree.FindVisualElementFormName(grid, "PostImage")).DataContext);
            this.Frame.Navigate(typeof(PostDetail), dataContext);
        }

        private void DiscoveryListItemTapped(object sender, TappedRoutedEventArgs e)
        {
            var grid = (Grid) sender;
            var dataContext = new PostListItem
            {
                Work = new Work { Pic = ((PostListItem)(((Image)VisualTree.FindVisualElementFormName(grid, "PostImage")).DataContext)).Work.Pic },
                User = new User { NickName = @"KuosLo", },
            };
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

        private void MainPagePostButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
        }

        private void MainPagePostButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ((TextBlock)sender).Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void MainPagePostComment_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (CommentPage));
        }

        private void ToSignPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SignPage));
        }

        private void Signout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ToSignPage.Visibility = Visibility.Visible;
            ProfilePanel.Visibility = Visibility.Collapsed;
            (App.Current as App).CurrentUser = null;
            MainPageVm.Signout();
        }

        private async void Refresh_click(object sender, RoutedEventArgs e)
        {
            int currentIndex = MainPivot.SelectedIndex;
            await MainPageVm.RefreshCurrentView(currentIndex);
        }

        private async void NextPage_click(object sender, RoutedEventArgs e)
        {
            ++MainPageVm.CurrentPage;
            await MainPageVm.RefreshCurrentView(MainPivot.SelectedIndex);
        }

        private async void PrePage_click(object sender, RoutedEventArgs e)
        {
            if (MainPageVm.CurrentPage > 1)
            {
                --MainPageVm.CurrentPage;
                await MainPageVm.RefreshCurrentView(MainPivot.SelectedIndex);
            }

        }

        private async void MainPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            switch (pivot.SelectedIndex)
            {
                case 0: pgBack.Visibility = pgForward.Visibility = pgRefresh.Visibility = Visibility.Visible; break;
                default:
                    pgBack.Visibility = pgForward.Visibility = Visibility.Collapsed;
                    pgRefresh.Visibility = Visibility.Visible; break;

            }           
            await MainPageVm.RefreshCurrentView(pivot.SelectedIndex, false);

        }
    }

}
