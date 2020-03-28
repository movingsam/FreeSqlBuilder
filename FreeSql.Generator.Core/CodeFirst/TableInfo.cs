using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FreeSql.DataAnnotations;
using FreeSql.DatabaseModel;
using FreeSql.Generator.Core.Utilities;
using GRES.Framework.Utils;
using MySqlX.XDevAPI.Relational;

namespace FreeSql.Generator.Core.CodeFirst
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        public TableInfo()
        {
        }
        public TableInfo(Type tableType)
        {
            this.Name = tableType.Name;
            var dic = Utilities.Comment.GetProperyCommentBySummary(tableType);
            //dic?.FirstOrDefault(x => x.Key == Name).Value;
            Comment = dic?.FirstOrDefault(x => x.Key == Name).Value ?? "";
            this.ColumnInfos = tableType.GetProperties().Where(t => Reflection.IsBaseType(t.PropertyType)).Select(x => new ColumnInfo(x, dic?.FirstOrDefault(d => d.Key == x.Name).Value)).ToList();
            this.NavigateInfos = tableType.GetProperties().Where(t => !Reflection.IsBaseType(t.PropertyType)).Select(x => new NavigateColumnInfo(x, dic?.FirstOrDefault(d => d.Key == x.Name).Value)).ToList();
            this.DbTableName = tableType.GetCustomAttribute<TableAttribute>()?.Name ?? Name;
            ImportUsings = this.ColumnInfos.Select(x => x.UsingNameSpace).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            PrimaryTypeName = Reflection.ToCsType(this.ColumnInfos?.FirstOrDefault(x => x.ColumnAttribute?.IsPrimary ?? false)?.Type);
            IsServiceTable = tableType.GetCustomAttribute<ServiceAttribute>() != null;
            NameSpace = tableType.Namespace;
        }
        public TableInfo(DbTableInfo tableType, string nameSpace)
        {
            this.Name = tableType.Name;
            Comment = tableType.Comment;
            this.ColumnInfos = tableType.Columns.Select(x => new ColumnInfo(x)).ToList();
            this.NavigateInfos = tableType.Columns.Select(t => new NavigateColumnInfo(t)).ToList();
            this.DbTableName = tableType.Name;
            ImportUsings = this.ColumnInfos.Select(x => x.UsingNameSpace).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            PrimaryTypeName = Reflection.ToCsType(this.ColumnInfos.FirstOrDefault(x => x.ColumnAttribute?.IsPrimary ?? false)?.Type);
            IsServiceTable = true;

        }


        /// <summary>
        /// 是否为主表
        /// </summary>
        public bool IsServiceTable { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; }
        /// <summary>
        /// 涉及到的命名空间
        /// </summary>
        public List<string> ImportUsings { get; set; }
        /// <summary>
        /// 主键类型
        /// </summary>
        public string PrimaryTypeName { get; set; }

        /// <summary>
        /// 备注 
        /// </summary>
        public string Comment { get; set; } = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string DbTableName { get; set; }
        /// <summary>
        /// 所有表字段
        /// </summary>
        public List<ColumnInfo> ColumnInfos { get; set; }

        public List<NavigateColumnInfo> NavigateInfos { get; set; }
    }
    /// <summary>
    /// 导航字段
    /// </summary>
    public class NavigateColumnInfo : ColumnInfo
    {
        public NavigateColumnInfo(DbColumnInfo info) : base(info)
        {

        }
        public NavigateColumnInfo(PropertyInfo info, string comment) : base(info, comment)
        {
        }
        /// <summary>
        /// 数据库名
        /// </summary>
        public override string DbColumnName => string.Empty;

        /// <summary>
        /// 引用的命名空间
        /// </summary>
        internal override string UsingNameSpace => Type.Namespace;
        /// <summary>
        /// 导航属性
        /// </summary>
        public TableInfo NavigateTableInfo => Reflection.IsGenericCollection(Type) ? GloabalTableInfo.AllTableInfos.FirstOrDefault(x => x.Name == Type.GetGenericArguments().FirstOrDefault()?.Name) : GloabalTableInfo.AllTableInfos.FirstOrDefault(x => x.Name == Type.Name);

        /// <summary>
        /// 导航类分类
        /// </summary>
        public NavigateCategory NavigateCategory => Reflection.IsGenericCollection(Type) ? NavigateCategory.OneToMany : NavigateCategory.OneToOne;
    }


    /// <summary>
    /// 字段
    /// </summary>
    public class ColumnInfo
    {
        public ColumnInfo(DbColumnInfo info)
        {
            this.Comment = info.Coment;
            this.ColumnAttribute = new ColumnAttribute
            {
                DbType = info.DbTypeTextFull,
                IsPrimary = info.IsPrimary,
                IsNullable = info.IsNullable,
                IsIdentity = info.IsIdentity,
                Name = info.Name,
                StringLength = info.MaxLength
            };
            this.Type = info.CsType;
            this.ColumnName = info.Name;
            this.CsType = info.CsType.Name;
        }
        public ColumnInfo(PropertyInfo info, string comment)
        {
            Comment = comment;
            ColumnAttribute = info.GetCustomAttribute<ColumnAttribute>();
            Type = info.PropertyType;
            ColumnName = info.Name;
            CsType = Reflection.ToCsType(Type);
        }
        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 字段备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 数据库字段名
        /// </summary>
        public virtual string DbColumnName => ColumnAttribute?.Name ?? ColumnName;
        /// <summary>
        /// Column特性
        /// </summary>
        public ColumnAttribute ColumnAttribute { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 类型字符串
        /// </summary>
        public string CsType { get; set; }
        /// <summary>
        /// 涉及到的命名空间
        /// </summary>
        internal virtual string UsingNameSpace => string.Empty;
    }

    public enum NavigateCategory
    {
        OneToOne, OneToMany, None
    }
}

