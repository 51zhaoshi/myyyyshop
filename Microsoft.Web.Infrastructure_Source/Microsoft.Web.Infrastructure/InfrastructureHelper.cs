namespace Microsoft.Web.Infrastructure
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Web;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class InfrastructureHelper
    {
        [SecuritySafeCritical]
        public static bool IsCodeDomDefinedExtension(string extension)
        {
            return CodeDomProvider.IsDefinedExtension(extension);
        }

        [SecuritySafeCritical]
        public static void UnloadAppDomain()
        {
            if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
            {
                HttpRuntime.UnloadAppDomain();
            }
            else
            {
                HttpRuntimeReflectionUtil instance = HttpRuntimeReflectionUtil.Instance;
                if (instance != null)
                {
                    instance.SetUserForcedShutdown();
                    instance.ShutdownAppDomain(ApplicationShutdownReason.UnloadAppDomainCalled, "User code called UnloadAppDomain");
                }
            }
        }

        [SecurityCritical]
        private sealed class HttpRuntimeReflectionUtil
        {
            public static readonly InfrastructureHelper.HttpRuntimeReflectionUtil Instance = GetInstance();

            private HttpRuntimeReflectionUtil()
            {
            }

            private static InfrastructureHelper.HttpRuntimeReflectionUtil GetInstance()
            {
                try
                {
                    InfrastructureHelper.HttpRuntimeReflectionUtil util = new InfrastructureHelper.HttpRuntimeReflectionUtil();
                    Type containingType = typeof(HttpRuntime);
                    string methodName = "SetUserForcedShutdown";
                    bool isStatic = true;
                    Type[] emptyTypes = Type.EmptyTypes;
                    Type returnType = typeof(void);
                    MethodInfo method = CommonReflectionUtil.FindMethod(containingType, methodName, isStatic, emptyTypes, returnType);
                    util.SetUserForcedShutdown = CommonReflectionUtil.MakeDelegate<Action>(method);
                    CommonReflectionUtil.Assert(util.SetUserForcedShutdown != null);
                    Type type3 = typeof(HttpRuntime);
                    string str2 = "ShutdownAppDomain";
                    bool flag2 = true;
                    Type[] argumentTypes = new Type[] { typeof(ApplicationShutdownReason), typeof(string) };
                    Type type4 = typeof(bool);
                    MethodInfo info2 = CommonReflectionUtil.FindMethod(type3, str2, flag2, argumentTypes, type4);
                    util.ShutdownAppDomain = CommonReflectionUtil.MakeDelegate<Func<ApplicationShutdownReason, string, bool>>(info2);
                    CommonReflectionUtil.Assert(util.ShutdownAppDomain != null);
                    return util;
                }
                catch
                {
                    return null;
                }
            }

            public Action SetUserForcedShutdown { get; private set; }

            public Func<ApplicationShutdownReason, string, bool> ShutdownAppDomain { get; private set; }
        }
    }
}

