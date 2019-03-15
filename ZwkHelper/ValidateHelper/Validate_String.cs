using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ValidateHelper
{

    public static partial class Validate
    {

        /// <summary>
        /// 是否有值
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string HasValue(this string value, string errorMsg)
        {
            if (String.IsNullOrEmpty(value))
                throw new Exception(errorMsg);
            return value;
        }

        /// <summary>
        /// 判断string类型的数据能否转成Int类型
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>是返回int</returns>
        public static int IsInt(this string value, string errorMsg)
        {
            try
            {
                return Int32.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 判断string类型的数据能否转成Long类型
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>是返回int</returns>
        public static long IsLong(this string value, string errorMsg)
        {
            try
            {
                return Int64.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 判断string类型的数据能否转成float类型
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static float IsFloat(this string value, string errorMsg)
        {
            try
            {
                return Single.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 判断string类型的数据能否转成Int类型
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>    
        public static double IsDouble(this string value, string errorMsg)
        {
            try
            {
                return Double.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 判断string类型的数据能否转成DateTime类型
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static DateTime IsDateTime(this string value, string errorMsg)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 是否是bool值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static bool IsBool(this string value, string errorMsg)
        {
            try
            {
                return Boolean.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 是否是decimal值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static decimal IsDecimal(this string value, string errorMsg)
        {
            try
            {
                return Decimal.Parse(value);
            }
            catch (Exception)
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 是否是枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static T IsEnum<T>(this string value, string errorMsg)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                throw new Exception(errorMsg);
            }
        }

        /// <summary>
        /// 字符串最大长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length">个数</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string MaxLength(this string value, int length, string errorMsg)
        {
            if (value.Length <= length)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 字符串最小长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length">个数</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string MinLength(this string value, int length, string errorMsg)
        {
            if (value.Length >= length)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        ///  字符串限制长度
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static string LimitLength(this string value, int length, string errorMsg)
        {
            if (length == value.Length)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 是否包含指定字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="substring">子字符串</param>
        /// <param name="splitChar">分隔符</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string InSplitStr(this string value, string substring, char splitChar, string errorMsg)
        {
            string[] strItems = value.Split(splitChar);
            if (strItems.Any(item => item == substring))
            {
                return value;
            }
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 是否在字符串数组中
        /// </summary>
        /// <param name="value"></param>
        /// <param name="strs"></param>
        /// <param name="errormsg"></param>
        /// <returns></returns>
        public static string InStrs(this string value, string[] strs, string errormsg)
        {
            if (strs.Any(p => p == value))
                return value;
            throw new Exception(errormsg);
        }
    }

}
