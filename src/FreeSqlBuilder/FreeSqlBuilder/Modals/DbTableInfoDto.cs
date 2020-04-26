using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.DatabaseModel;

namespace FreeSqlBuilder.Modals
{
    public class DbTableInfoDto
    {
        public DbTableInfoDto(DbTableInfo info)
        {
            this.Id = info.Id;
            this.Schema = info.Schema;
            this.Name = info.Name;
            this.Comment = info.Comment;
            this.Type = info.Type;
            this.Columns = info.Columns.Select(s => new DbColumnInfoDto(s)).ToList();
            this.Primarys = info.Primarys.Select(s => new DbColumnInfoDto(s)).ToList();
            var dic = new Dictionary<string, DbForeignInfoDto>();
            foreach (var dbForeignInfo in info.ForeignsDict)
            {
                dic.Add(dbForeignInfo.Key, new DbForeignInfoDto(dbForeignInfo.Value));
            }
            this.ForeignsDict = dic;

        }
        /// <summary>唯一标识</summary>
        public string Id { get; set; }

        /// <summary>SqlServer下是Owner、PostgreSQL下是Schema、MySql下是数据库名</summary>
        public string Schema { get; set; }

        /// <summary>表名</summary>
        public string Name { get; set; }

        /// <summary>表备注，SqlServer下是扩展属性 MS_Description</summary>
        public string Comment { get; set; }

        /// <summary>表/视图</summary>
        public DbTableType Type { get; set; }

        /// <summary>列</summary>
        public List<DbColumnInfoDto> Columns { get; set; } = new List<DbColumnInfoDto>();

        /// <summary>主键/组合</summary>
        public List<DbColumnInfoDto> Primarys { get; set; } = new List<DbColumnInfoDto>();

        /// <summary>外键</summary>
        public Dictionary<string, DbForeignInfoDto> ForeignsDict { get; set; } = new Dictionary<string, DbForeignInfoDto>();



        public List<DbForeignInfoDto> Foreigns
        {
            get
            {
                return this.ForeignsDict.Values.ToList<DbForeignInfoDto>();
            }
        }


    }
    /// <summary>
    /// 数据库列相关属性dto
    /// </summary>
    public class DbColumnInfoDto
    {
        public DbColumnInfoDto(DbColumnInfo info)
        {
            this.Name = info.Name;
            this.Coment = info.Coment;
            this.DbTypeTextFull = info.DbTypeTextFull;
            this.MaxLength = info.MaxLength;
            this.IsPrimary = info.IsPrimary;
            this.IsIdentity = info.IsIdentity;
            this.IsNullable = info.IsNullable;
            this.DefaultValue = info.DefaultValue;
        }

        /// <summary>列名</summary>
        public string Name { get; set; }

        /// <summary>数据库类型，字符串，varchar(255)</summary>
        public string DbTypeTextFull { get; set; }

        /// <summary>最大长度</summary>
        public int MaxLength { get; set; }

        /// <summary>主键</summary>
        public bool IsPrimary { get; set; }

        /// <summary>自增标识</summary>
        public bool IsIdentity { get; set; }

        /// <summary>是否可DBNull</summary>
        public bool IsNullable { get; set; }

        /// <summary>备注</summary>
        public string Coment { get; set; }

        /// <summary>数据库默认值</summary>
        public string DefaultValue { get; set; }

    }

    public class DbForeignInfoDto
    {
        public DbForeignInfoDto(DbForeignInfo info)
        {
            this.Table = new DbTableInfoDto(info.Table); ;
            this.ReferencedTable = new DbTableInfoDto(info.ReferencedTable);
        }
        public DbTableInfoDto Table { get; set; }
        //public List<DbColumnInfo> Columns { get; set; } = new List<DbColumnInfo>();
        public DbTableInfoDto ReferencedTable { get; set; }
        //public List<DbColumnInfo> ReferencedColumns { get; set; } = new List<DbColumnInfo>();

    }

}
