namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using Microsoft.Web.Infrastructure;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Web;

    [SecurityCritical]
    internal sealed class DynamicValidationShimReflectionUtil
    {
        public static readonly DynamicValidationShimReflectionUtil Instance = GetInstance();

        private DynamicValidationShimReflectionUtil()
        {
        }

        private static DynamicValidationShimReflectionUtil GetInstance()
        {
            try
            {
                DynamicValidationShimReflectionUtil util = new DynamicValidationShimReflectionUtil();
                bool throwOnError = false;
                Type type = CommonAssemblies.SystemWeb.GetType("Microsoft.Web.Infrastructure.DynamicValidationHelper.DynamicValidationShim", throwOnError);
                if (type == null)
                {
                    return null;
                }
                Type containingType = type;
                string methodName = "EnableDynamicValidation";
                bool isStatic = true;
                Type[] argumentTypes = new Type[] { typeof(HttpContext) };
                Type returnType = typeof(void);
                MethodInfo method = CommonReflectionUtil.FindMethod(containingType, methodName, isStatic, argumentTypes, returnType);
                util.EnableDynamicValidation = CommonReflectionUtil.MakeDelegate<Action<HttpContext>>(method);
                CommonReflectionUtil.Assert(util.EnableDynamicValidation != null);
                Type type4 = type;
                string str2 = "IsValidationEnabled";
                bool flag3 = true;
                Type[] typeArray4 = new Type[] { typeof(HttpContext) };
                Type type5 = typeof(bool);
                MethodInfo info2 = CommonReflectionUtil.FindMethod(type4, str2, flag3, typeArray4, type5);
                util.IsValidationEnabled = CommonReflectionUtil.MakeDelegate<Func<HttpContext, bool>>(info2);
                CommonReflectionUtil.Assert(util.IsValidationEnabled != null);
                Type type6 = type;
                string str3 = "GetUnvalidatedCollections";
                bool flag4 = true;
                Type[] typeArray6 = new Type[] { typeof(HttpContext), typeof(Func<NameValueCollection>).MakeByRefType(), typeof(Func<NameValueCollection>).MakeByRefType() };
                Type type7 = typeof(void);
                MethodInfo info3 = CommonReflectionUtil.FindMethod(type6, str3, flag4, typeArray6, type7);
                util.GetUnvalidatedCollections = CommonReflectionUtil.MakeDelegate<GetUnvalidatedCollectionsCallback>(info3);
                CommonReflectionUtil.Assert(util.GetUnvalidatedCollections != null);
                return util;
            }
            catch
            {
                return null;
            }
        }

        public Action<HttpContext> EnableDynamicValidation { get; private set; }

        public GetUnvalidatedCollectionsCallback GetUnvalidatedCollections { get; private set; }

        public Func<HttpContext, bool> IsValidationEnabled { get; private set; }
    }
}

