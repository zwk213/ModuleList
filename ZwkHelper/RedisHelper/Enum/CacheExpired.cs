using System;

namespace CacheHelper.Enum
{
    public enum CacheExpired
    {
        /// <summary>
        /// 分钟
        /// </summary>
        Minite,
        /// <summary>
        /// 5分钟
        /// </summary>
        FiveMinite,
        /// <summary>
        /// 一刻钟
        /// </summary>
        Quarter,
        /// <summary>
        /// 小时
        /// </summary>
        Hour,
        /// <summary>
        /// 天
        /// </summary>
        Day,
        /// <summary>
        /// 星期
        /// </summary>
        Week,
    }

    public static class RedisExtend
    {
        public static TimeSpan ToTimeSpan(this CacheExpired expired)
        {
            switch (expired)
            {
                case CacheExpired.Minite:
                    return new TimeSpan(0, 1, 0);
                case CacheExpired.FiveMinite:
                    return new TimeSpan(0, 5, 0);
                case CacheExpired.Quarter:
                    return new TimeSpan(0, 15, 0);
                case CacheExpired.Hour:
                    return new TimeSpan(1, 0, 0);
                case CacheExpired.Day:
                    return new TimeSpan(1, 0, 0, 0);
                case CacheExpired.Week:
                    return new TimeSpan(1, 0, 0, 0).Multiply(7);
                default:
                    return new TimeSpan();
            }
        }
    }
}
