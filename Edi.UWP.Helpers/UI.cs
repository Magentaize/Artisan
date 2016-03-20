using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace Edi.UWP.Helpers
{
    public class UI
    {
        /// <summary>
        /// Get current device screen height in pixel
        /// </summary>
        /// <returns></returns>
        public static int GetScreenHeight()
        {
            var rect = PointerDevice.GetPointerDevices().Last().ScreenRect;
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)(rect.Height * scale);
        }

        public static int GetScreenWidth()
        {
            var rect = PointerDevice.GetPointerDevices().Last().ScreenRect;
            var scale = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            return (int)(rect.Width * scale);
        }

        public static Color GetAccentColor()
        {
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                return (Color)Application.Current.Resources["SystemAccentColor"];
            }
            else
            {
                return new UISettings().GetColorValue(UIColorType.Accent);
            }
        }

        /// <summary>
        /// Set App Window Preferred Launch View Size
        /// </summary>
        /// <param name="height">Window Height</param>
        /// <param name="width">Window Width</param>
        public static void SetWindowLaunchSize(int height, int width)
        {
            ApplicationView.PreferredLaunchViewSize = new Size { Height = height, Width = width };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        /// <summary>
        /// Set Color to App Title Bar
        /// </summary>
        /// <param name="titleBackgroundColor">Background Color</param>
        /// <param name="titleForegroundColor">Foreground Color</param>
        /// <param name="titleInactiveBackgroundColor">Inactive Background Color</param>
        /// <param name="titleInactiveForegroundColor">Inactive Foreground Color</param>
        public static void ApplyColorToTitleBar(Color? titleBackgroundColor,
            Color? titleForegroundColor,
            Color? titleInactiveBackgroundColor,
            Color? titleInactiveForegroundColor)
        {
            var view = ApplicationView.GetForCurrentView();

            // active
            view.TitleBar.BackgroundColor = titleBackgroundColor;
            view.TitleBar.ForegroundColor = titleForegroundColor;

            // inactive
            view.TitleBar.InactiveBackgroundColor = titleInactiveBackgroundColor;
            view.TitleBar.InactiveForegroundColor = titleInactiveForegroundColor;
        }

        /// <summary>
        /// Set Color to App Title Bar Buttons (Maxium, Minus, Close)
        /// </summary>
        /// <param name="titleButtonBackgroundColor"></param>
        /// <param name="titleButtonForegroundColor"></param>
        /// <param name="titleButtonHoverBackgroundColor"></param>
        /// <param name="titleButtonHoverForegroundColor"></param>
        /// <param name="titleButtonPressedBackgroundColor"></param>
        /// <param name="titleButtonPressedForegroundColor"></param>
        /// <param name="titleButtonInactiveBackgroundColor"></param>
        /// <param name="titleButtonInactiveForegroundColor"></param>
        public static void ApplyColorToTitleButton(Color? titleButtonBackgroundColor,
            Color? titleButtonForegroundColor,
            Color? titleButtonHoverBackgroundColor,
            Color? titleButtonHoverForegroundColor,
            Color? titleButtonPressedBackgroundColor,
            Color? titleButtonPressedForegroundColor,
            Color? titleButtonInactiveBackgroundColor,
            Color? titleButtonInactiveForegroundColor)
        {
            var view = ApplicationView.GetForCurrentView();

            // button
            view.TitleBar.ButtonBackgroundColor = titleButtonBackgroundColor;
            view.TitleBar.ButtonForegroundColor = titleButtonForegroundColor;

            view.TitleBar.ButtonHoverBackgroundColor = titleButtonHoverBackgroundColor;
            view.TitleBar.ButtonHoverForegroundColor = titleButtonHoverForegroundColor;

            view.TitleBar.ButtonPressedBackgroundColor = titleButtonPressedBackgroundColor;
            view.TitleBar.ButtonPressedForegroundColor = titleButtonPressedForegroundColor;

            view.TitleBar.ButtonInactiveBackgroundColor = titleButtonInactiveBackgroundColor;
            view.TitleBar.ButtonInactiveForegroundColor = titleButtonInactiveForegroundColor;
        }

        public enum NotificationAudioNames
        {
            Default,
            IM,
            Mail,
            Reminder,
            SMS,
            Looping_Alarm,
            Looping_Alarm2,
            Looping_Alarm3,
            Looping_Alarm4,
            Looping_Alarm5,
            Looping_Alarm6,
            Looping_Alarm7,
            Looping_Alarm8,
            Looping_Alarm9,
            Looping_Alarm10,
            Looping_Call,
            Looping_Call2,
            Looping_Call3,
            Looping_Call4,
            Looping_Call5,
            Looping_Call6,
            Looping_Call7,
            Looping_Call8,
            Looping_Call9,
            Looping_Call10,
        }

        /// <summary>
        /// Pop up a toast notification, which will stay in Notification Center
        /// </summary>
        /// <param name="assetsImageFileName">The image filename under Assets folder</param>
        /// <param name="text">Notification text</param>
        /// <param name="audioName">Notification sound</param>
        public static void ShowToastNotification(string assetsImageFileName, string text, NotificationAudioNames audioName)
        {
            // 1. create element
            ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            // 2. provide text
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(text));

            // 3. provide image
            XmlNodeList toastImageAttributes = toastXml.GetElementsByTagName("image");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("src", $"ms-appx:///assets/{assetsImageFileName}");
            ((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "logo");

            // 4. duration
            IXmlNode toastNode = toastXml.SelectSingleNode("/toast");
            ((XmlElement)toastNode).SetAttribute("duration", "short");

            // 5. audio
            XmlElement audio = toastXml.CreateElement("audio");
            audio.SetAttribute("src", $"ms-winsoundevent:Notification.{audioName.ToString().Replace("_", ".")}");
            toastNode.AppendChild(audio);

            // 6. app launch parameter
            //((XmlElement)toastNode).SetAttribute("launch", "{\"type\":\"toast\",\"param1\":\"12345\",\"param2\":\"67890\"}");

            // 7. send toast
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        /// <summary>
        /// Apply a background color to Phone's system status bar
        /// </summary>
        /// <param name="backgroundColor">Background Color</param>
        /// <param name="foregroundColor">Foreground Color</param>
        public static void SetWindowsMobileStatusBarColor(Color? backgroundColor, Color? foregroundColor)
        {
            Mobile.SetWindowsMobileStatusBarColor(backgroundColor, foregroundColor);
        }

        /// <summary>
        /// Hide System Status Bar on Phone, make App full screen
        /// </summary>
        /// <returns>Task</returns>
        public static async Task HideWindowsMobileStatusBar()
        {
            await Mobile.HideWindowsMobileStatusBar();
        }
    }
}
