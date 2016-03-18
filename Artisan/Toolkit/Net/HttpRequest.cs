using System.Net.Http;
using System.Threading.Tasks;
using Artisan.Toolkit.Utilities;

namespace Artisan.Toolkit.Net
{
    public class HttpRequest
    {
        private string UserAgent = "Artisan/" + GetVersion.AssemblyVersion();

        public static async Task<string> HttpGet(string url)
        {
            HttpClient httpRequest = new HttpClient();
            string result = await httpRequest.GetStringAsync(url);
            return result;
        }
    }
}