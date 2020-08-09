using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace FreeSqlBuilder.Core.Utilities
{
    public static class Comment
    {
        /// <summary>
        /// 通过属性的注释文本，通过 xml 读取
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Dict：key=属性名，value=注释</returns>
        public static Dictionary<string, string> GetProperyCommentBySummary(Type type)
        {
            var regex = new Regex(@"\.(dll|exe)", RegexOptions.IgnoreCase);
            var xmlPath = regex.Replace(type.Assembly.Location, ".xml");
            if (File.Exists(xmlPath) == false)
            {
                if (string.IsNullOrEmpty(type.Assembly.CodeBase)) return null;
                xmlPath = regex.Replace(type.Assembly.CodeBase, ".xml");
                if (xmlPath.StartsWith("file:///") && Uri.TryCreate(xmlPath, UriKind.Absolute, out var tryuri))
                    xmlPath = tryuri.LocalPath;
                if (File.Exists(xmlPath) == false) return null;
            }
            var dic = new Dictionary<string, string>();
            var sReader = new StringReader(File.ReadAllText(xmlPath));
            using (var xmlReader = XmlReader.Create(sReader))
            {
                XPathDocument xpath = null;
                try
                {
                    xpath = new XPathDocument(xmlReader);
                }
                catch
                {
                    return null;
                }
                var xmlNav = xpath.CreateNavigator();
                var props = type.GetPropertiesDictIgnoreCase().Values;
                foreach (var prop in props)
                {
                    var className = (prop.DeclaringType.IsNested ? $"{prop.DeclaringType.Namespace}.{prop.DeclaringType.DeclaringType.Name}.{prop.DeclaringType.Name}" : $"{prop.DeclaringType.Namespace}.{prop.DeclaringType.Name}").Trim('.');
                    var node = xmlNav.SelectSingleNode($"/doc/members/member[@name='P:{className}.{prop.Name}']/summary");
                    if (node == null) continue;
                    var comment = node.InnerXml.Trim(' ', '\r', '\n', '\t');
                    if (string.IsNullOrEmpty(comment)) continue;
                    dic.Add(prop.Name, comment);
                }
                var typeCommentNode = xmlNav.SelectSingleNode($"/doc/members/member[@name='T:{type.FullName}']/summary");
                if (typeCommentNode != null) dic.Add(type.Name, typeCommentNode.InnerXml.Trim(' ', '\r', '\n', '\t'));
            }
            return dic;
        }
    }
}
