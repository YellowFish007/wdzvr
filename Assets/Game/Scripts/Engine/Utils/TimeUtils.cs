using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Engine
{
    public static class TimeUtils
    {

        public static long GetNowTimeStamp()
        {
            DateTime now = DateTime.Now;
            DateTime startTime = new DateTime(1970, 1, 1);
            long timeStamp = (long)(now - startTime).TotalSeconds;

            return timeStamp;
        }

        /// <summary>
        /// 获取当前时间字符串，格式：yyyy/MM/dd HH:mm:ss
        /// </summary>
        /// <returns></returns>
        public static string GetNowDateTime()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// 将秒数转换为倒计时格式 MM:SS
        /// </summary>
        /// <param name="seconds">总秒数</param>
        /// <returns>格式化的倒计时字符串，如 "05:30"</returns>
        public static string SecondsToCountdownFormat(int seconds)
        {
            // 确保秒数不为负数
            seconds = Mathf.Max(0, seconds);
            
            int minutes = seconds / 60;
            int remainingSeconds = seconds % 60;
            
            return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
        }

        /// <summary>
        /// 将浮点秒数转换为倒计时格式 MM:SS
        /// </summary>
        /// <param name="seconds">总秒数（浮点数）</param>
        /// <returns>格式化的倒计时字符串，如 "05:30"</returns>
        public static string SecondsToCountdownFormat(float seconds)
        {
            return SecondsToCountdownFormat(Mathf.FloorToInt(seconds));
        }

    }
}