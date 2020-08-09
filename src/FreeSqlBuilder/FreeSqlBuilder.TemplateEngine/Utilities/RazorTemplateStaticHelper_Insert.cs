using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreeSql.Internal.Model;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    public static partial class RazorTemplateStaticHelper
    {
        /// <summary>
        /// 获取插入字符串
        /// #ToDo 这里暂时只算了一层如果是深度联级插入需要修改代码
        /// #ToDo 后续完善我现在不想写
        /// </summary>
        /// <param name="table"></param>
        /// <param name="freeSqlObjString"></param>
        /// <param name="dataStr"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        public static string GetInsertStr(this TableInfo table, string freeSqlObjString, string dataStr, bool isAsync = false)
        {
            var res = new List<string>();
            var subInsert = table.GetNavigates()?.ToList();
            if (subInsert?.Count > 0)//开启进程事物无法使用异步
            {
                isAsync = false;
            }
            var main = $"var main = {MakeFreeSqlInsertString(freeSqlObjString, table.CsName, dataStr, isAsync)}";
            var subRes = new List<string>();
            if (subInsert != null)
            {
                foreach (var subRef in subInsert)
                {
                    var tempSb = new StringBuilder();
                    var mainPk = subRef.Columns.FirstOrDefault()?.CsName ?? "<主表属性>";
                    var subProperty = subRef.Property.Name;
                    var nickName = subProperty.ToLower();
                    var subKey = subRef.RefColumns.FirstOrDefault()?.CsName ?? "<主表主键>";
                    switch (subRef.RefType)
                    {
                        //#ToDo 联级插入暂时没做
                        //case TableRefType.ManyToOne:
                        //    break;
                        //case TableRefType.OneToOne:
                        //    tempSb.AppendLine($"var {nickName}Res = {MakeFreeSqlInsertString(freeSqlObjString, subRef.RefEntityType.Name, $"{dataStr}.{subProperty}", isAsync)}");
                        //    tempSb.AppendLine($"{dataStr}.{mainPk} = {nickName}Res.FirstOrDefault()?.{subKey} ?? 0;  ");
                        //    break;
                        //case TableRefType.OneToMany:
                        //    tempSb.AppendLine($"var {nickName}Res = {MakeFreeSqlInsertString(freeSqlObjString, subRef.RefEntityType.Name, $"{dataStr}.{subProperty}", isAsync)}");
                        //    tempSb.AppendLine($@"foreach (var {nickName} in {dataStr}.{subProperty})
                        //{{
                        //     main.{subKey} = {nickName}.{mainPk};
                        //}}");
                        //    break;
                        case TableRefType.ManyToMany:
                            var middleNames = $"{subRef.RefMiddleEntityType.Name}s";
                            tempSb.AppendLine(
                                $@"var {middleNames} = {dataStr}.{subRef.Property.Name}.Select(x=>new {subRef.RefMiddleEntityType.Name}(){{
                                {subRef.MiddleColumns[0].CsName} = main.{subRef.Columns.FirstOrDefault()?.CsName};
                                {subRef.MiddleColumns[1].CsName} = x.{subRef.Columns.FirstOrDefault()?.CsName};
                        }}).ToList();");
                            tempSb.AppendLine($"{MakeFreeSqlInsertString(freeSqlObjString, subRef.RefMiddleEntityType.Name, $"{middleNames}", isAsync)}");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    subRes.Add(tempSb.ToString());
                }
            }
            if (subInsert?.Any(x => x.RefType == TableRefType.OneToOne || x.RefType == TableRefType.OneToMany) ?? false)
            {
                res.AddRange(subRes);
                res.Add(main);
            }
            else
            {
                res.Add(main);
                res.AddRange(subRes);
            }
            var sb = new StringBuilder();
            var t = new StringBuilder();
            foreach (var i in res)
            {
                t.Append(i);
            }
            sb.AppendLine($@"{freeSqlObjString}.Transaction(() =>
                {{
                        {t.ToString()}
                }});");
            return sb.ToString();

        }
        /// <summary>
        /// 生成FreeSql插入语法
        /// </summary>
        /// <param name="freeSqlObjString"></param>
        /// <param name="tableName"></param>
        /// <param name="dataStr"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        public static string MakeFreeSqlInsertString(string freeSqlObjString, string tableName, string dataStr, bool isAsync = false)
        {
            var aSync = "Async";
            var aWait = "await ";
            if (!isAsync)
            {
                aSync = "";
                aWait = "";
            }
            var res = $"{aWait}{freeSqlObjString}.Insert<{tableName}>({dataStr}).ExecuteInserted{aSync}();";
            return res;
        }

    }
}