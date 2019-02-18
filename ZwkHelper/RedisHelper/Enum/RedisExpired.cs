using System;

namespace RedisHelper.Enum
{
    public enum RedisExpired
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
        public static TimeSpan ToTimeSpan(this RedisExpired expired)
        {
            switch (expired)
            {
                case RedisExpired.Minite:
                    return new TimeSpan(0, 1, 0);
                case RedisExpired.FiveMinite:
                    return new TimeSpan(0, 5, 0);
                case RedisExpired.Quarter:
                    return new TimeSpan(0, 15, 0);
                case RedisExpired.Hour:
                    return new TimeSpan(1, 0, 0);
                case RedisExpired.Day:
                    return new TimeSpan(1, 0, 0, 0);
                case RedisExpired.Week:
                    return new TimeSpan(1, 0, 0, 0).Multiply(7);
                default:
                    return new TimeSpan();
            }
        }
    }
}
