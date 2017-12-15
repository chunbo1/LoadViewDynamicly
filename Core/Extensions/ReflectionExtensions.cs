#pragma warning disable 1591

using System;
using System.Linq;
using System.Reflection;

namespace Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static TResult GetCustomAttributeValue<TAttr, TResult>(this MemberInfo memberInfo, Func<TAttr, TResult> func) where TAttr : Attribute
        {
            var lAttrs = memberInfo.GetCustomAttributes(false).OfType<TAttr>();
            if (lAttrs != null && lAttrs.Any())
            {
                return func(lAttrs.First());
            }
            return default(TResult);
        }
    }
}

#pragma warning restore 1591
