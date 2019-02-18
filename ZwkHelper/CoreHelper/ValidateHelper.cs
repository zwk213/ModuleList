using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreHelper
{
    public static class ValidateHelper
    {
        #region string

        /// <summary>
        /// 是否有值
        /// </summary>
        /// <param name="value">数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string HasValue(this string value, string errorMsg)
        {
            if (string.IsNullOrEmpty(value))
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
                return int.Parse(value);
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
                return float.Parse(value);
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
                return bool.Parse(value);
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
                return decimal.Parse(value);
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

        #endregion

        #region datetime

        /// <summary>
        /// 大于等于某个时间
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static DateTime GreaterThan(this DateTime value, DateTime start, string errorMsg)
        {
            if (value >= start)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 小于等于某个时间
        /// </summary>
        /// <param name="value"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static DateTime LessThan(this DateTime value, DateTime end, string errorMsg)
        {
            if (value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 在某个时间范围内
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static DateTime Between(this DateTime value, DateTime start, DateTime end, string errorMsg)
        {
            if (start <= value && value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        #endregion

        #region int

        /// <summary>
        /// 大于等于某个整数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static int GreaterThan(this int value, int number, string errorMsg)
        {
            if (value >= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 小于等于某个整数
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static int LessThan(this int value, int number, string errorMsg)
        {
            if (value <= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 在两个整数之间
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static int Between(this int value, int start, int end, string errorMsg)
        {
            if (start <= value && value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        #endregion

        #region float

        /// <summary>
        /// 大于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static float GreaterThan(this float value, float number, string errorMsg)
        {
            if (value >= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 小于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static float LessThan(this float value, float number, string errorMsg)
        {
            if (value <= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 在两个数之间
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static float Between(this float value, float start, float end, string errorMsg)
        {
            if (start <= value && value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        #endregion

        #region double

        /// <summary>
        /// 大于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static double GreaterThan(this double value, double number, string errorMsg)
        {
            if (value >= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 小于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static double LessThan(this double value, double number, string errorMsg)
        {
            if (value <= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 在两个数之间
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static double Between(this double value, double start, double end, string errorMsg)
        {
            if (start <= value && value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        #endregion

        #region decimal

        /// <summary>
        /// 大于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static decimal GreaterThan(this decimal value, decimal number, string errorMsg)
        {
            if (value >= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 小于等于某个数
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="number"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static decimal LessThan(this decimal value, decimal number, string errorMsg)
        {
            if (value <= number)
                return value;
            throw new Exception(errorMsg);
        }

        /// <summary>
        /// 在两个数之间
        /// 否则报异常
        /// </summary>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static decimal Between(this decimal value, decimal start, decimal end, string errorMsg)
        {
            if (start <= value && value <= end)
                return value;
            throw new Exception(errorMsg);
        }

        #endregion
    }
}
