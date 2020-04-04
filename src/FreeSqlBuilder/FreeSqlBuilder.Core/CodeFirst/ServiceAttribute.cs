using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSql.Generator.Core.CodeFirst
{
    /// <summary>
    /// 标记的为主要逻辑表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
    }
}
