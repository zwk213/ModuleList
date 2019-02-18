using System.Collections.Generic;

namespace EFHelper.Model
{
    public class PageData<T>
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数据
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
