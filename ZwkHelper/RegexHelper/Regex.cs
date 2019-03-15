using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexHelper
{
    public static class Regex
    {
        /// <summary>
        /// 匹配 
        /// 正则表达式进行验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText">正则表达式</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string Match(this string value, string regText, string errorMsg)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(regText);
            if (reg.IsMatch(value))
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 切割     
        /// 分割字符串返回string[]的对象
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText">分隔符</param>
        /// <returns></returns>
        public static string[] Split(this string value, string regText)
        {
            string[] arr = System.Text.RegularExpressions.Regex.Split(value, regText);
            return arr;
        }

        /// <summary>
        /// 替换
        /// 替换符合正则表达式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText">正则表达式</param>
        /// <param name="aimText">替换后的字符串</param>
        /// <returns></returns>
        public static string Replace(this string value, string regText, string aimText)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, regText, aimText);
        }

        /// <summary>
        ///  获取
        ///  获取符合正则表达式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText"></param>
        /// <returns></returns>
        public static string[] Get(this string value, string regText)
        {
            string[] strReturn = new string[System.Text.RegularExpressions.Regex.Matches(value, regText).Count];
            int i = 0;
            foreach (Match mch in System.Text.RegularExpressions.Regex.Matches(value, regText))
            {
                strReturn[i] += mch.Value.Trim();
                i++;
            }
            return strReturn;
        }
    }
}
