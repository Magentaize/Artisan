using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Artisan.Toolkit.Helper
{
    public class TemporaryFileManager
    {
        //public static async Task<string> GetLocalTemporaryFileAsync(string uri)
        //{
        //    var tempFolder = ApplicationData.Current.TemporaryFolder;
        //    var currentFolder = ApplicationData.Current.TemporaryFolder;
        //    var filePathArray = uri.Split('/');
        //    for (int i = 0; i < filePathArray.Length - 1; i++)
        //    {
        //        try
        //        {
        //            currentFolder = await currentFolder.GetFolderAsync(filePathArray[i]);
        //        }
        //        catch (Exception e)
        //        {
        //            currentFolder = await currentFolder.CreateFolderAsync(filePathArray[i], CreationCollisionOption.OpenIfExists);
        //        }
        //    } //创建文件夹结构 


        //    try
        //    {
        //        var result = await currentFolder.GetFileAsync(filePathArray[filePathArray.Length - 1]);
        //        return result.ToString();//TODO:返回文件绝对路径
        //    }
        //    catch (Exception e)
        //    {
        //        var hostUri = ResourceLoader.GetForCurrentView().GetString("HostUri");
        //        var currentFile = await currentFolder.CreateFileAsync(filePathArray[filePathArray.Length - 1]);
        //        FileIO.WriteBufferAsync();


        //        //currentFile =
        //        //    await
        //        //        currentFile.ReplaceWithStreamedFileFromUriAsync(currentFile,
        //        //           new Uri(hostUri + filePathArray[filePathArray.Length - 1]), null);
        //        using (IRandomAccessStream fileStream = await currentFile.OpenAsync(FileAccessMode.ReadWrite))
        //        {
        //        //    using (DataWriter dw = new DataWriter(textStream))
        //        //    {
        //        //        var stream = await GetFileFromUriAsync(hostUri + filePathArray[filePathArray.Length - 1]);
        //        //        var buffer = new byte[32768];
        //        //        while (true)
        //        //        {
        //        //            int read = await stream.ReadAsync(buffer, 0, buffer.Length);
        //        //            if(read<=0)
        //        //                break;;
        //        //        }
        //        //        dw.WriteBuffer(buffer);
        //        //    }
        //        }
        //    }


        //    StorageFile localFile = await tempFolder.CreateFileAsync("");
        //}

        private static async Task<Stream> GetFileFromUriAsync(string uri)
        {
            HttpWebRequest request;
            request = HttpWebRequest.CreateHttp(uri);
            request.Method = "GET";
            try
            {
                var response = await request.GetResponseAsync();
                return response.GetResponseStream();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}