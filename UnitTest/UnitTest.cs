using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Artisan.Toolkit.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task<Dictionary<string,string> > TestMethod1()
        {
           var result = await HttpWebPost.PostDataToUriAsync("http://artisan.chinacloudapp.cn/signup.json", "nickname=zix&username=rukamihara&password=20132013");
            return result;
        }
    }
}
