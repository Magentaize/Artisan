using Edi.UWP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artisan.Toolkit.Helper
{
    public class UserConfiguration
    {
        private JsonDataRepository<Dictionary<string, string>> Jdr = new JsonDataRepository<Dictionary<string, string>>("UserConfiguration.json");
        private Dictionary<string, string> configuration;
        public Dictionary<string, string> Configuration
        {
            get { return configuration ?? getConfigurationAsync().Result; }
            set { setConfigurationAsync(value); }
        }
        public async Task<Dictionary<string, string>>  getConfigurationAsync()
        {
           return await Jdr.LoadDataAsync();
        }
        public async void setConfigurationAsync(Dictionary<string, string> data)
        {
            await Jdr.SaveDataAsync(data);
        }
    }
}
