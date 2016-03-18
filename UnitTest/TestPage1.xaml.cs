using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace UnitTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class TestPage1 : Page
    {
        public TestPage1()
        {
            this.InitializeComponent();
        }

        private async void TestClick(object sender, RoutedEventArgs e)
        {
            UnitTest1 test = new UnitTest1();
            var result = await test.TestMethod1();
            string dis = "";
             foreach(var item in result)
            {
                dis += $"{item.Key}={item.Value}\n";
            }
            testOut.Text = dis;
        }
    }
}
