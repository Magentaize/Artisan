using System.Text;
using Windows.Data.Xml.Dom;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Notifications;

namespace Edi.UWP.Helpers
{
    public static class ToastHelper
    {
        /// <summary>
        /// Show notification in Action Center
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="content">Notification content</param>
        /// <returns>ToastNotification</returns>
        public static ToastNotification PopToast(string title, string content)
        {
            return PopToast(title, content, null, null);
        }

        /// <summary>
        /// Show notification in Action Center
        /// </summary>
        /// <param name="title">Notification title</param>
        /// <param name="content">Notification content</param>
        /// <param name="tag">Tag</param>
        /// <param name="group">Group</param>
        /// <returns>ToastNotification</returns>
        public static ToastNotification PopToast(string title, string content, string tag, string group)
        {
            string xml = $@"<toast activationType='foreground'>
                                            <visual>
                                                <binding template='ToastGeneric'>
                                                </binding>
                                            </visual>
                                        </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var binding = doc.SelectSingleNode("//binding");

            var el = doc.CreateElement("text");
            el.InnerText = title;

            binding.AppendChild(el);

            el = doc.CreateElement("text");
            el.InnerText = content;
            binding.AppendChild(el);

            return PopCustomToast(doc, tag, group);
        }

        /// <summary>
        /// Show notification by custom xml
        /// </summary>
        /// <param name="xml">notification xml</param>
        /// <returns>ToastNotification</returns>
        public static ToastNotification PopCustomToast(string xml)
        {
            return PopCustomToast(xml, null, null);
        }

        /// <summary>
        /// Show notification by custom xml
        /// </summary>
        /// <param name="xml">notification xml</param>
        /// <param name="tag">tag</param>
        /// <param name="group">group</param>
        /// <returns>ToastNotification</returns>
        public static ToastNotification PopCustomToast(string xml, string tag, string group)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);


            return PopCustomToast(doc, tag, group);
        }

        /// <summary>
        /// Show notification by custom xml
        /// </summary>
        /// <param name="doc">notification xml</param>
        /// <param name="tag">tag</param>
        /// <param name="group">group</param>
        /// <returns>ToastNotification</returns>
        [DefaultOverload]
        public static ToastNotification PopCustomToast(XmlDocument doc, string tag, string group)
        {
            var toast = new ToastNotification(doc);

            if (tag != null)
                toast.Tag = tag;

            if (group != null)
                toast.Group = group;

            ToastNotificationManager.CreateToastNotifier().Show(toast);

            return toast;
        }

        public static string ToString(ValueSet valueSet)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var pair in valueSet)
            {
                if (builder.Length != 0)
                    builder.Append('\n');

                builder.Append(pair.Key);
                builder.Append(": ");
                builder.Append(pair.Value);
            }

            return builder.ToString();
        }
    }
}
