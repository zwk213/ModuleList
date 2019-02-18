using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreHelper
{
    public static class RegexHelper
    {
        /// <summary>
        /// 匹配 
        /// 正则表达式进行验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText">正则表达式</param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string RegexMatch(this string value, string regText, string errorMsg)
        {
            Regex reg = new Regex(regText);
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
        public static string[] RegexSplit(this string value, string regText)
        {
            string[] arr = Regex.Split(value, regText);
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
        public static string RegexReplace(this string value, string regText, string aimText)
        {
            return Regex.Replace(value, regText, aimText);
        }

        /// <summary>
        ///  获取
        ///  获取符合正则表达式的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="regText"></param>
        /// <returns></returns>
        public static string[] RegexGet(this string value, string regText)
        {
            string[] strReturn = new string[Regex.Matches(value, regText).Count];
            int i = 0;
            foreach (Match mch in Regex.Matches(value, regText))
            {
                strReturn[i] += mch.Value.Trim();
                i++;
            }
            return strReturn;
        }
    }

}
