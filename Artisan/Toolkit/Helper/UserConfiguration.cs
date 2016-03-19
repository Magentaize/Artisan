using Edi.UWP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Collections;
using Windows.Storage;

namespace Artisan.Toolkit.Helper
{
    public class UserConfiguration
    {
        public static IPropertySet Settings
        {
            get { return ApplicationData.Current.RoamingSettings.Values; }

        }

        //private JsonDataRepository<Dictionary<string, string>> Jdr = new JsonDataRepository<Dictionary<string, string>>("UserConfiguration.json");
        //private Dictionary<string, string> configuration;
        //public Dictionary<string, string> Configuration
        //{
        //    get { return configuration ?? getConfigurationAsync().Result; }
        //    set { setConfigurationAsync(value); }
        //}
        //public IPropertySet  getConfigurationAsync()
        //{
        //    return ApplicationData.Current.RoamingSettings.Values;
        //}
        //public async void setConfigurationAsync(Dictionary<string, string> data)
        //{
        //    await Jdr.SaveDataAsync(data);
        //}
    }
}
