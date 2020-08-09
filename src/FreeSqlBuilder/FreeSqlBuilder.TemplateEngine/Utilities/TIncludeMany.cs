using System.Collections.Generic;

namespace FreeSqlBuilder.TemplateEngine.Utilities
{
    /// <summary>
    /// IncludeMany对象
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TIncludeMany : TIIncludeMany
    {

        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        // ReSharper disable once IdentifierTypo
        public List<TIIncludeMany> IncludeManys { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Include { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsRoot { get; set; } = false;
    }
    /// <summary>
    /// IncludeMany的抽象接口
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface TIIncludeMany
    {
        /// <summary>
        /// 节点Key
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 值 存列对象
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// IncludeMany对象
        /// </summary>
        // ReSharper disable once IdentifierTypo
        List<TIIncludeMany> IncludeManys { get; set; }
        /// <summary>
        /// Include对象
        /// </summary>
        List<string> Include { get; set; }
        /// <summary>
        /// 标识是否为初始根节点
        /// </summary>
        bool IsRoot { get; set; }
    }
}