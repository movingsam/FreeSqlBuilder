using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FreeSqlBuilder.Core.Utilities
{
    /// <summary>
    /// 反射操作
    /// </summary>
    public static class Reflection
    {
        /// <summary>
        /// 获取类型描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return GetDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(Common.GetType<T>(), memberName);
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription(Type type, string memberName)
        {
            if (type == null)
                return string.Empty;
            if (string.IsNullOrWhiteSpace(memberName))
                return string.Empty;
            return GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
        }

        /// <summary>
        /// 获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="member">成员</param>
        public static string GetDescription(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is { } attribute ? attribute.Description : member.Name;
        }

        /// <summary>
        /// 获取显示名称，使用DisplayNameAttribute设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return GetDisplayName(Common.GetType<T>());
        }

        /// <summary>
        /// 获取显示名称，使用DisplayAttribute或DisplayNameAttribute设置显示名称
        /// </summary>
        public static string GetDisplayName(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            if (member.GetCustomAttribute<DisplayAttribute>() is { } displayAttribute)
                return displayAttribute.Name;
            if (member.GetCustomAttribute<DisplayNameAttribute>() is { } displayNameAttribute)
                return displayNameAttribute.DisplayName;
            return string.Empty;
        }

        /// <summary>
        /// 获取显示名称或描述,使用DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayNameOrDescription<T>()
        {
            return GetDisplayNameOrDescription(Common.GetType<T>());
        }

        /// <summary>
        /// 获取属性显示名称或描述,使用DisplayAttribute或DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        public static string GetDisplayNameOrDescription(MemberInfo member)
        {
            var result = GetDisplayName(member);
            return string.IsNullOrWhiteSpace(result) ? GetDescription(member) : result;
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <typeparam name="TFind">查找类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<Type> FindTypes<TFind>(params Assembly[] assemblies)
        {
            var findType = typeof(TFind);
            return FindTypes(findType, assemblies);
        }

        /// <summary>
        /// 查找类型列表
        /// </summary>
        /// <param name="findType">查找类型</param>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<Type> FindTypes(Type findType, params Assembly[] assemblies)
        {
            var result = new List<Type>();
            foreach (var assembly in assemblies)
                result.AddRange(GetTypes(findType, assembly));
            return result.Distinct().ToList();
        }
        /// <summary>
        /// 获取类型列表
        /// </summary>
        private static List<Type> GetTypes(Type findType, Assembly assembly)
        {
            var result = new List<Type>();
            if (assembly == null)
                return result;
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
            foreach (var type in types)
                AddType(result, findType, type);
            return result;
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        private static void AddType(List<Type> result, Type findType, Type type)
        {
            if (type.IsInterface || type.IsAbstract)
                return;
            if (!BaseFrome(type, findType.Name))
                return;
            result.Add(type);
        }

        /// <summary>
        /// 泛型匹配
        /// </summary>
        private static bool MatchGeneric(Type findType, Type type)
        {
            if (findType.IsGenericTypeDefinition == false)
                return false;
            var definition = findType.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (implementedInterface.IsGenericType == false)
                    continue;
                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }

        /// <summary>
        /// 获取实现了接口的所有实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="assemblies">待查找的程序集列表</param>
        public static List<TInterface> GetInstancesByInterface<TInterface>(params Assembly[] assemblies)
        {
            return FindTypes<TInterface>(assemblies)
                .Select(t => CreateInstance<TInterface>(t)).ToList();
        }

        /// <summary>
        /// 动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>        
        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return ConvertHelper.To<T>(Activator.CreateInstance(type, parameters));
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly(string assemblyName)
        {

            return Assembly.Load(new AssemblyName(assemblyName));
        }


        public static bool IsString(MemberInfo member)
        {
            if (member == null)
            {
                return false;
            }
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.String";
                case MemberTypes.Property:
                    return IsString((PropertyInfo)member);
            }

            return false;
        }
        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsString(PropertyInfo property)
        {
            return property.PropertyType == typeof(string);
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member == typeof(bool) || member == typeof(bool?);
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否枚举类型
        /// </summary>
        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return true;
            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsEnum;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member == typeof(DateTime) || member == typeof(DateTime?);
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
                return true;
            if (property.PropertyType == typeof(DateTime?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    if (member == typeof(int))
                        return true;
                    if (member == typeof(int?))
                        return true;
                    if (member == typeof(short))
                        return true;
                    if (member == typeof(short?))
                        return true;
                    if (member == typeof(long))
                        return true;
                    if (member == typeof(long?))
                        return true;
                    return false;
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }

        /// <summary>
        /// 是否整型
        /// </summary>
        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
                return true;
            if (property.PropertyType == typeof(int?))
                return true;
            if (property.PropertyType == typeof(short))
                return true;
            if (property.PropertyType == typeof(short?))
                return true;
            if (property.PropertyType == typeof(long))
                return true;
            if (property.PropertyType == typeof(long?))
                return true;
            return false;
        }

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
                return false;
            if (IsInt(member))
                return true;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return IsNumberType(member);
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }

        private static bool IsNumberType(MemberInfo member)
        {
            if (member == typeof(double))
                return true;
            if (member == typeof(double?))
                return true;
            if (member == typeof(decimal))
                return true;
            if (member == typeof(decimal?))
                return true;
            if (member == typeof(float))
                return true;
            if (member == typeof(float?))
                return true;
            return false;

        }
        /// <summary>
        /// 是否数值类型
        /// </summary>
        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
                return true;
            if (property.PropertyType == typeof(double?))
                return true;
            if (property.PropertyType == typeof(decimal))
                return true;
            if (property.PropertyType == typeof(decimal?))
                return true;
            if (property.PropertyType == typeof(float))
                return true;
            if (property.PropertyType == typeof(float?))
                return true;
            return false;
        }
        /// <summary>
        /// 是否Guid类型
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsGuid(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    if (member == typeof(Guid))
                        return true;
                    if (member == typeof(Guid?))
                        return true;
                    return false;
                case MemberTypes.Property:
                    return IsGuid((PropertyInfo)member);
            }
            return false;

        }

        /// <summary>
        /// 是否Guid类型
        /// </summary>
        private static bool IsGuid(PropertyInfo property)
        {
            if (property.PropertyType == typeof(Guid))
                return true;
            if (property.PropertyType == typeof(Guid?))
                return true;
            return false;
        }


        /// <summary>
        /// 是否集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsCollection(Type type)
        {
            
            if (type.IsArray)
                return true;
            return IsGenericCollection(type);
        }

        /// <summary>
        /// 是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(Type type)
        {
            if (!type.IsGenericType)
                return false;
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IEnumerable<>)
                   || typeDefinition == typeof(IReadOnlyCollection<>)
                   || typeDefinition == typeof(IReadOnlyList<>)
                   || typeDefinition == typeof(ICollection<>)
                   || typeDefinition == typeof(IList<>)
                   || typeDefinition == typeof(List<>) 
                   || typeDefinition == typeof(IDictionary<,>);
        }

        /// <summary>
        /// 从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList()
                .Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"))
                .Select(path => Assembly.Load(new AssemblyName(path))).ToList();
        }

        ///// <summary>
        ///// 获取公共属性列表
        ///// </summary>
        ///// <param name="instance">实例</param>
        //public static List<Item> GetPublicProperties(object instance)
        //{
        //    var properties = instance.GetType().GetProperties();
        //    return properties.ToList().Select(t => new Item(t.Name, t.GetValue(instance))).ToList();
        //}

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetTopBaseType<T>()
        {
            return GetTopBaseType(typeof(T));
        }

        /// <summary>
        /// 获取顶级基类
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetTopBaseType(Type type)
        {
            if (type == null)
                return null;
            if (type.IsInterface)
                return type;
            if (type.BaseType == typeof(object))
                return type;
            return GetTopBaseType(type.BaseType);
        }
        public static bool BaseFrom(Type type, Type baseType)
        {
            if (BaseFrome(type, baseType.Name))
            {
                return true;
            }
            if (baseType.IsAssignableFrom(type))
            {
                return true;
            }
            if (type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == baseType)
            {
                return true;
            }
            var typeInfo = type.GetTypeInfo();
            return typeInfo.ImplementedInterfaces.Any() && typeInfo.ImplementedInterfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == baseType);
        }
        public static bool BaseFrome(Type type, string baseClassName)
        {
            if (type == null)
                return false;
            if (type.IsAbstract)
                return false;
            if (type.BaseType == typeof(object))
                return false;
            if (type.BaseType.Name == baseClassName)
                return true;
            return BaseFrome(type.BaseType, baseClassName);
        }


        /// <summary>
        /// 是否是基础类型
        /// </summary>
        /// <returns></returns>
        public static bool IsBaseType(Type type)
        {
            if (IsCollection(type)) return false;
            return IsString(type) || IsGuid(type) || IsBool(type) || IsDate(type) || IsEnum(type) || IsInt(type) || IsNumber(type);
        }
        /// <summary>
        /// 系统类型转换字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SystemCsType(Type type)
        {
            if (type == typeof(string)) return "string";
            if (type == typeof(bool)) return "bool";
            if (type == typeof(Guid)) return "Guid";
            if (type == typeof(Guid?)) return "Guid?";
            if (type == typeof(short)) return "short";
            if (type == typeof(short?)) return "short?";
            if (type == typeof(int)) return "int";
            if (type == typeof(int?)) return "int?";
            if (type == typeof(long)) return "long";
            if (type == typeof(long?)) return "long?";
            if (type == typeof(double)) return "double";
            if (type == typeof(double?)) return "double?";
            if (type == typeof(decimal)) return "decimal";
            if (type == typeof(decimal?)) return "decimal?";
            if (type == typeof(float)) return "float";
            if (type == typeof(float?)) return "float?";
            if (type == typeof(DateTime)) return "DateTime";
            if (type == typeof(DateTime?)) return "DateTime?";
            if (type == typeof(DateTimeOffset)) return "DateTimeOffset";
            if (type == typeof(DateTimeOffset?)) return "DateTimeOffset?";
            return string.Empty;
        }
        /// <summary>
        /// 转CSharp类型字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToCsType(Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }
            var res = SystemCsType(type);
            if (!string.IsNullOrWhiteSpace(res)) return res;
            if (IsCollection(type))
            {
                var typeDefinition = type.GetGenericTypeDefinition();
                var types = string.Join(',', type.GetGenericArguments().Select(x => x.Name));
                var collectionType = typeDefinition == typeof(IEnumerable<>) ? "IEnumerable" :
                    typeDefinition == typeof(IReadOnlyCollection<>) ? "IReadOnlyCollection" :
                    typeDefinition == typeof(IReadOnlyList<>) ? "IReadOnlyList" :
                    typeDefinition == typeof(ICollection<>) ? "ICollection" :
                    typeDefinition == typeof(IList<>) ? "IList" :
                    typeDefinition == typeof(List<>) ? "List" :
                    typeDefinition == typeof(IDictionary<,>)? "IDictionary" : "" ;
                return $"{collectionType}<{types}>";
            }
            return type.Name;
        }


    }
}
