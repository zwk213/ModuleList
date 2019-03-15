using System;
using System.Collections.Generic;
using System.Text;

namespace ValidateHelper
{
    public static partial class Validate
    {

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

    }
}
