using Windows.UI.Popups;
using Windows.Foundation;

namespace Artisan.Toolkit
{
    public class MessageBox
    {
        /// <summary>
        /// Wrapped MessageBox (like winform)
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static void Show(string content,string title)
        {
            IAsyncOperation<IUICommand> command = new MessageDialog(content, title).ShowAsync();
        }

        public static void Show(string content)
        {
            IAsyncOperation<IUICommand> command = new MessageDialog(content).ShowAsync();
        }
    }
}