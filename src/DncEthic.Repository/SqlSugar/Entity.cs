using System;
using SqlSugar;
using System.Collections.Generic;
using System.Text;

namespace DncEthic.Repository.SqlSugar
{
   public class Entity
    {
        /// <summary>
        /// 创建者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string CreatorBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 编辑者ID
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string EditorId { get; set; }
        /// <summary>
        /// 编辑者
        /// </summary>
        [SugarColumn(Length = 50, IsNullable = true)]
        public string EditorBy { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public DateTime? EditDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否删除（0:否1:是）
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public int IsDelete { get; set; }

    }
}
