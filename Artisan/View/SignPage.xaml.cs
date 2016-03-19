using Artisan.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Artisan.Toolkit;
using Artisan.Toolkit.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Artisan.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SignPage : Page
    {
        public SignPage()
        {
            this.InitializeComponent();
        }
        private SignPageViewModel SignPageVm { get; } = new SignPageViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string userid = UserConfiguration.Settings["Username"] as string;
            string password = UserConfiguration.Settings["Password"] as string;
            UserId.Text = userid ?? "";
            Password.Password = password ?? "";
        }
        private async void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as SignPageViewModel;
            if (Password.Password != Password2.Password)
            {
                ErrorInfo.Text = "两次密码输入不一致";
                return;
            }
            if (UserId.Text.Trim() == string.Empty || Password.Password.Trim() == string.Empty)
            {
                ErrorInfo.Text = "请正确填写用户名和密码";
                return;
            }

            string result = await SignPageVm.SignupAsync(UserId.Text, Password.Password);
            if (result != null)
            {
                ErrorInfo.Text = result;
            }
            else
            {
                ErrorInfo.Text = "注册成功，请自行返回登录= =";
            }
        }
        private async void SigninButton_Click(object sender, RoutedEventArgs e)
        {
            if(UserId.Text.Trim() == string.Empty || Password.Password.Trim() == string.Empty)
            {
                ErrorInfo.Text = "请正确填写用户名和密码";
                return;
            }

            var result = await SignPageVm.SigninAsync(UserId.Text, Password.Password);
            if (result != null)
            {
                ErrorInfo.Text = result;
            }
            else {
                Frame.GoBack();
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToSignup.Visibility = Visibility.Collapsed;
            SigninButton.Visibility = Visibility.Collapsed;
            AutoSignin.Visibility = SavePassword.Visibility = Visibility.Collapsed;
            Password2.Visibility = Visibility.Visible;
            SignupButton.Visibility = Visibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += SignPage_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private void SignPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            SwitchToSignup.Visibility = Visibility.Visible;
            SigninButton.Visibility = Visibility.Visible;
            AutoSignin.Visibility = SavePassword.Visibility = Visibility.Visible;
            Password2.Visibility = Visibility.Collapsed;
            SignupButton.Visibility = Visibility.Collapsed;

            SystemNavigationManager.GetForCurrentView().BackRequested -= SignPage_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            e.Handled = true;
        }

        private void SigninButton_LostFocus(object sender, RoutedEventArgs e)
        {
            ErrorInfo.Text = "     ";
        }

    }
}
