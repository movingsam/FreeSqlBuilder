using System;

namespace FreeSqlBuilder.Core.CodeFirst
{
    /// <summary>
    /// 标记的为主要逻辑表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ServiceAttribute : Attribute
    {
    }
}
