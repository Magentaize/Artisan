using System;
using System.Text;

namespace Artisan.Toolkit.Helper
{
    public static class DateFormat
    {
        public static string GetFormattedTime()
        {
            var random = new Random(DateTime.Now.Second);
            string result = random.Next(10, 59).ToString() + " 分钟之前";
            return result;;
        }

        /// <summary>
        /// 获取指定格式的时间
        /// </summary>
        /// <param name="originDate"></param>
        /// <returns></returns>
        public static string GetFormattedDate(string originDate)
        {
            //var year = Int32.Parse(originDate.Substring(0, 4));
            //var month = Int32.Parse(originDate.Substring(5, 2));
            //var day = Int32.Parse(originDate.Substring(8, 2));
            //var hour = Int32.Parse(originDate.Substring(11, 2));
            //var minute = Int32.Parse(originDate.Substring(14, 2));
            //var second = Int32.Parse(originDate.Substring(17, 2));
            //DateTime tempDate = new DateTime(year,month,day,hour,minute,second);

            DateTime originDateTime = DateTime.Parse(originDate);
            DateTime nowDateTime = DateTime.Now;
            TimeSpan resultTimeSpan =originDateTime - nowDateTime;

            string result;
            if (resultTimeSpan.Days > 0)
            {
                result = originDateTime.ToString();
            }
            else if(resultTimeSpan.Hours>0)
            {
                result = new StringBuilder(resultTimeSpan.Hours.ToString() + " 小时前").ToString();
            }
            else if (resultTimeSpan.Minutes > 0)
            {
                result = new StringBuilder(resultTimeSpan.Minutes.ToString() + " 分钟前").ToString();
            }
            else
            {
                result = new StringBuilder(resultTimeSpan.Seconds.ToString() + " 秒前").ToString();
            }

            return result;

        }
    }
}