using FreeSql.Internal.Model;
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
                this.ColumnsByCsIgnore = new Dictionary<string, ColumnInfoDto>(info.ColumnsByCsIgnore.Select(x => new KeyValuePair<string, ColumnInfoDto>(x.Key, new ColumnInfoDto(x.Value)))).ToList();
                this.DisableSyncStructure = info.DisableSyncStructure;
                this.DbOldName = info.DbOldName;
                this.DbName = info.DbName;
                this.CsName = info.CsName;
                this.Indexes = info.Indexes;
                this.Primarys = info.Primarys.Select(x => new ColumnInfoDto(x)).ToArray();
                this.ColumnsByPosition = info.ColumnsByPosition.Select(x => new ColumnInfoDto(x)).ToArray();
                this.Comment = info.Comment;
                this.VersionColumn = new ColumnInfoDto(info.VersionColumn);
                this.ColumnsByCs = new Dictionary<string, ColumnInfoDto>(info.ColumnsByCs.Select(x => new KeyValuePair<string, ColumnInfoDto>(x.Key, new ColumnInfoDto(x.Value)))).ToList();
                this.Columns = new Dictionary<string, ColumnInfoDto>(info.Columns.Select(x => new KeyValuePair<string, ColumnInfoDto>(x.Key, new ColumnInfoDto(x.Value)))).ToList();
            }
        }
        public List<KeyValuePair<string,ColumnInfoDto>> ColumnsByCsIgnore { get; set; }
        public bool DisableSyncStructure { get; set; }
        public string DbOldName { get; set; }
        public string DbName { get; set; }
        public string CsName { get; set; }
        public IndexInfo[] Indexes { get; set; }
        public ColumnInfoDto[] Primarys { get; set; }
        public ColumnInfoDto[] ColumnsByPosition { get; set; }
        public ColumnInfoDto VersionColumn { get; set; }
        public List<KeyValuePair<string, ColumnInfoDto>> ColumnsByCs { get; set; }
        public List<KeyValuePair<string, ColumnInfoDto>> Columns { get; set; }
        public string Comment { get; set; }
    }

    public class ColumnInfoDto
    {
        public ColumnInfoDto(ColumnInfo info)
        {
            if (info != null)
            {
                //this.Table = info.Table;
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

        //public TableInfo Table { get; set; }
        public string CsName { get; set; }
        public string Comment { get; set; }
        public string DbTypeText { get; set; }
        public string DbDefaultValue { get; set; }
        public string DbInsertValue { get; set; }
        public string DbUpdateValue { get; set; }
        public int DbSize { get; set; }
        public byte DbPrecision { get; set; }
        public byte DbScale { get; set; }
    }

    public class IndexInfoDto
    {
        public IndexInfoDto(IndexInfo info)
        {
            if (info != null)
            {
                this.Name = info.Name;
                this.IsUnique = info.IsUnique;
            }
        }

        public string Name { get; set; }
        public bool IsUnique { get; set; }
    }
}
