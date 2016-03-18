using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Networking.Connectivity;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Edi.UWP.Helpers
{
    public class Utils
    {
        /// <summary>
        /// Copy a string to Windows Clipboard
        /// </summary>
        /// <param name="str"></param>
        public static void CopyToClipBoard(string str)
        {
            var dp = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy,
            };
            dp.SetText(str);
            Clipboard.SetContent(dp);
        }

        public static async Task<string> GetXmlStringAsync(string assetsFileName)
        {
            var storageFile = await Package.Current.InstalledLocation.GetFileAsync($"Assets/{assetsFileName}".Replace('/', '\\'));
            var fStream = await storageFile.OpenReadAsync();

            var stream = fStream.AsStream();

            if (stream == null)
            {
                throw new FileNotFoundException("Could not find embedded mappings resource file.");
            }

            var reader = new StreamReader(stream);
            string s = reader.ReadToEnd();
            return s;
        }

        public static string GetResource(string key)
        {
            return
                Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetValue(key).ValueAsString;
        }

        /// <summary>
        /// Detect if current device is connected to the internet
        /// </summary>
        /// <returns></returns>
        public static bool HasInternetConnection()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        /// <summary>
        /// Show Phone Call UI
        /// </summary>
        /// <param name="phoneNumber">Phone Number</param>
        /// <param name="displayName">Contact Display Name</param>
        public static void MakePhoneCall(string phoneNumber, string displayName)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(phoneNumber, displayName);
        }

        /// <summary>
        /// Save InkCanvas strokes to .ink File
        /// </summary>
        /// <param name="inkCanvas">InkCanvas Object</param>
        /// <param name="location">PickerLocationId</param>
        /// <returns>Success or not</returns>
        public static async Task<Response> SaveToInkFile(InkCanvas inkCanvas, PickerLocationId location)
        {
            IRandomAccessStream stream = new InMemoryRandomAccessStream();

            var strokes = inkCanvas.InkPresenter.StrokeContainer.GetStrokes();
            if (strokes.Any())
            {
                await inkCanvas.InkPresenter.StrokeContainer.SaveAsync(stream);

                var picker = new FileSavePicker
                {

                    SuggestedStartLocation = location
                };
                picker.FileTypeChoices.Add("INK files", new List<string> { ".ink" });
                var file = await picker.PickSaveFileAsync();
                if (file == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = $"{nameof(file)} is null"
                    };
                }

                CachedFileManager.DeferUpdates(file);
                var bt = await Utils.ConvertImagetoByte(stream);
                await FileIO.WriteBytesAsync(file, bt);
                await CachedFileManager.CompleteUpdatesAsync(file);

                return new Response
                {
                    IsSuccess = true
                };
            }
            return new Response
            {
                IsSuccess = false
            };
        }

        /// <summary>
        /// Load strokes from .ink file to InkCanvas
        /// </summary>
        /// <param name="inkCanvas">InkCanvas Object</param>
        /// <param name="location">PickerLocationId</param>
        /// <returns>Task</returns>
        public static async Task LoadInkFile(InkCanvas inkCanvas, PickerLocationId location)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = location
            };
            picker.FileTypeFilter.Add(".ink");
            var pickedFile = await picker.PickSingleFileAsync();
            if (pickedFile != null)
            {
                var file = await pickedFile.OpenReadAsync();
                await inkCanvas.InkPresenter.StrokeContainer.LoadAsync(file);
            }
        }

        /// <summary>
        /// Convert ImageObject to Byte Array
        /// </summary>
        /// <param name="fileStream">IRandomAccessStream</param>
        /// <returns>Byte Array</returns>
        public static async Task<byte[]> ConvertImagetoByte(IRandomAccessStream fileStream)
        {
            var reader = new DataReader(fileStream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)fileStream.Size);
            byte[] pixels = new byte[fileStream.Size];
            reader.ReadBytes(pixels);
            return pixels;
        }

        /// <summary>
        /// Load a picture to WriteableBitmap
        /// </summary>
        /// <param name="relativePath">e.g. "Assets/example.png"</param>
        /// <returns>WriteableBitmap</returns>
        public static async Task<WriteableBitmap> LoadWriteableBitmap(string relativePath)
        {
            var storageFile = await Package.Current.InstalledLocation.GetFileAsync(relativePath.Replace('/', '\\'));
            var stream = await storageFile.OpenReadAsync();
            var wb = new WriteableBitmap(1, 1);
            wb.SetSource(stream);
            return wb;
        }

        /// <summary>
        /// Get Current App Version. e.g. 2.4.1.0
        /// </summary>
        /// <returns>App Version</returns>
        public static string GetAppVersion()
        {
            var ver = Windows.ApplicationModel.Package.Current.Id.Version;
            return $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
        }

        /// <summary>
        /// Get Current App Logo Image Uri
        /// </summary>
        /// <returns>Uri</returns>
        public static Uri GetAppLogoUri() => Windows.ApplicationModel.Package.Current.Logo;

        /// <summary>
        /// Get Current App Display Name
        /// </summary>
        /// <returns>App Display Name</returns>
        public static string GetAppDisplayName() => Windows.ApplicationModel.Package.Current.DisplayName;

        /// <summary>
        /// Get Current App Publisher Name
        /// </summary>
        /// <returns>App Publisher Name</returns>
        public static string GetAppPublisher() => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public static string Architecture
        {
            get
            {
                Package package = Package.Current;
                return package.Id.Architecture.ToString();
            }
        }
    }
}
