using System;
using System.ComponentModel.DataAnnotations;

namespace EFHelper.Model
{
    public abstract class BaseModel
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        public void Create(string createBy)
        {
            PrimaryKey = Guid.NewGuid().ToString();
            CreateBy = createBy;
            CreateDate = DateTime.Now;
            UpdateBy = createBy;
            UpdateDate = DateTime.Now;
        }

        public void Update(string updateBy)
        {
            UpdateBy = updateBy;
            UpdateDate = DateTime.Now;
        }

    }
}
