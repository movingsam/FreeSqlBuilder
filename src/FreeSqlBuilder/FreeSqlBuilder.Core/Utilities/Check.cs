using System;

namespace FreeSqlBuilder.Core.Utilities
{
    public sealed class Check
    {
        /// <summary>
        /// 判断对象是否为空 若为空抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static T NotNull<T>(T value, string message, string parameterName = "") where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }
        /// <summary>
        /// 判断对象是否为空 若为空抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static T? NotNull<T>(T? value, string message, string parameterName = "") where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string NotEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{parameterName}");
            }

            return value;
        }

        public static void CheckCondition(Func<bool> condition, string parameterName)
        {
            if (condition.Invoke())
            {
                throw new ArgumentException($"{parameterName}");
            }
        }

        public static void CheckCondition(Func<bool> condition, string formatErrorText, params string[] parameters)
        {
            if (condition.Invoke())
            {
                throw new ArgumentException($"{formatErrorText}:{string.Join("-", parameters)}");
            }
        }
    }

}