﻿using FreeSql.Internal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeSqlBuilder.Modals
{
    /// <summary>
    /// TableInfo领域模型
    /// </summary>
    public class TableInfoDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public TableInfoDto(TableInfo info)
        {
            if (info != null)
            {
                this.DisableSyncStructure = info.DisableSyncStructure;
                this.DbOldName = info.DbOldName;
                this.DbName = info.DbName;
                this.CsName = info.CsName;
                this.Primarys = info.Primarys.Select(x => new ColumnInfoDto(x)).ToArray();
                this.Comment = info.Comment;
                this.ColumnsByCs = info.ColumnsByCs.Select(x => new ColumnInfoDto(x.Value)).ToList();
            }
        }
        /// <summary>
        /// 是否禁用数据库同步
        /// </summary>
        public bool DisableSyncStructure { get; set; }
        /// <summary>
        /// 旧名表
        /// </summary>
        public string DbOldName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string DbName { get; set; }
        /// <summary>
        /// Csharp名
        /// </summary>
        public string CsName { get; set; }
        /// <summary>
        /// 主键属性
        /// </summary>
        public ColumnInfoDto[] Primarys { get; set; }
        /// <summary>
        /// 代码中的属性信息
        /// </summary>
        public List<ColumnInfoDto> ColumnsByCs { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
    }
    /// <summary>
    /// 属性信息
    /// </summary>

    public class ColumnInfoDto
    {
        /// <summary>
        /// 带参构造
        /// </summary>
        /// <param name="info"></param>
        public ColumnInfoDto(ColumnInfo info)
        {
            if (info != null)
            {
                this.CsName = info.CsName;
                this.Comment = info.Comment;
                this.DbTypeText = info.DbTypeText;
                this.DbDefaultValue = info.DbDefaultValue;
                this.DbInsertValue = info.DbInsertValue;
                this.DbUpdateValue = info.DbUpdateValue;
                this.DbSize = info.DbSize;
                this.DbPrecision = info.DbPrecision;
                this.DbScale = info.DbScale;
            }
        }
        /// <summary>
        /// 代码中的属性名
        /// </summary>
        public string CsName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 数据库类型字符串
        /// </summary>
        public string DbTypeText { get; set; }
        /// <summary>
        /// 数据库默认值
        /// </summary>
        public string DbDefaultValue { get; set; }
        /// <summary>
        /// 数据库插入值
        /// </summary>
        public string DbInsertValue { get; set; }
        /// <summary>
        /// 数据库更新值
        /// </summary>
        public string DbUpdateValue { get; set; }
        /// <summary>
        /// 数据库大小
        /// </summary>
        public int DbSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte DbPrecision { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte DbScale { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>

    public class IndexInfoDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public IndexInfoDto(IndexInfo info)
        {
            if (info == null) return;
            this.Name = info.Name;
            this.IsUnique = info.IsUnique;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsUnique { get; set; }
    }
}
