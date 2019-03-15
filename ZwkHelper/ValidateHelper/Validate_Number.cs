using System;
using System.Collections.Generic;
using System.Text;

namespace ValidateHelper
{
    public static partial class Validate
    {
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
