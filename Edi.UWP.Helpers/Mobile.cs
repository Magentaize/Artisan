using System;
using System.Threading.Tasks;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Edi.UWP.Helpers
{
    public class Mobile
    {
        /// <summary>
        /// Apply a background color to Phone's system status bar
        /// </summary>
        /// <param name="backgroundColor">Background Color</param>
        /// <param name="foregroundColor">Foreground Color</param>
        public static void SetWindowsMobileStatusBarColor(Color? backgroundColor, Color? foregroundColor)
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = backgroundColor;
                statusBar.ForegroundColor = foregroundColor;
                statusBar.BackgroundOpacity = 1;
            }
        }

        /// <summary>
        /// Hide System Status Bar on Phone, make App full screen
        /// </summary>
        /// <returns>Task</returns>
        public static async Task HideWindowsMobileStatusBar()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await StatusBar.GetForCurrentView().HideAsync();
            }
        }

        /// <summary>
        /// Press Back to Exit the App
        /// </summary>
        /// <param name="pageFrame">Frame</param>
        /// <param name="e">BackPressedEventArgs</param>
        public static void ExitOnBackPressed(Frame pageFrame, BackPressedEventArgs e)
        {
            e.Handled = true;

            if (!pageFrame.CanGoBack)
            {
                Application.Current.Exit();
            }
        }

        /// <summary>
        /// Set Windows Phone Status Bar with Text and Progress Indicator
        /// </summary>
        /// <param name="backgroundColor">BackgroundColor</param>
        /// <param name="foregroundColor">ForegroundColor</param>
        /// <param name="opacity"></param>
        /// <param name="text">Status Bar Text</param>
        /// <param name="isIndeterminate">Is .  . . ... . . .  . always showing</param>
        /// <param name="showProgress">Show Busy Indicator</param>
        /// <returns></returns>
        public static async Task ShowSystemTrayAsync(Color backgroundColor, Color foregroundColor,
            double opacity = 1, string text = "", bool isIndeterminate = false, bool showProgress = false)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            statusBar.BackgroundColor = backgroundColor;
            statusBar.ForegroundColor = foregroundColor;
            statusBar.BackgroundOpacity = opacity;

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                if (showProgress)
                {
                    statusBar.ProgressIndicator.Text = text;
                    if (!isIndeterminate)
                    {
                        statusBar.ProgressIndicator.ProgressValue = 0;
                    }
                    await statusBar.ProgressIndicator.ShowAsync();
                }
            }
        }
    }
}
