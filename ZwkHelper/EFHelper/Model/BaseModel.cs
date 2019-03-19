using System;
using System.ComponentModel.DataAnnotations;
using ValidateHelper;

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

        public virtual void Validate()
        {
            PrimaryKey.HasValue("主键必填").MaxLength(50, "主键最大长度50");
            CreateBy.HasValue("创建人必填").MaxLength(50, "创建人最大长度50");
            UpdateBy.HasValue("更新人必填").MaxLength(50, "更新人最大长度50");
        }

        public virtual void UpdateFrom(BaseModel model)
        {
            UpdateBy = model.UpdateBy;
            UpdateDate = model.UpdateDate;
        }

    }
}
