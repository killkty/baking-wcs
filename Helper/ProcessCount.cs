using System;

namespace Helper
{
    /// <summary>
    /// 实现倒计时功能的类
    /// </summary>
    public class ProcessCount
    {
        /// <summary>
        /// 获取小时显示值
        /// </summary>
        /// <returns></returns>
        public static string GetHour(Int32 totalSecond)
        {
            return String.Format("{0:D2}", (totalSecond / 3600));
        }

        /// <summary>
        /// 获取分钟显示值
        /// </summary>
        /// <returns></returns>
        public static string GetMinute(Int32 totalSecond)
        {
            return String.Format("{0:D2}", (totalSecond % 3600) / 60);
        }


        /// <summary>
        /// 获取秒显示值
        /// </summary>
        /// <returns></returns>
        public static string GetSecond(Int32 totalSecond)
        {
            return String.Format("{0:D2}", totalSecond % 60);
        }


    }
}
