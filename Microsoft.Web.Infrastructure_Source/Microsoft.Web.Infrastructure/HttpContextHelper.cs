namespace Microsoft.Web.Infrastructure
{
    using System;
    using System.ComponentModel;
    using System.Security;
    using System.Security.Permissions;
    using System.Web;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class HttpContextHelper
    {
        [SecuritySafeCritical]
        public static void ExecuteInNullContext(Action action)
        {
            HttpContext current = HttpContext.Current;
            try
            {
                SetCurrentContext(null);
                action();
            }
            finally
            {
                SetCurrentContext(current);
            }
        }

        [SecurityCritical, PermissionSet(SecurityAction.Assert, Unrestricted=true)]
        private static void SetCurrentContext(HttpContext context)
        {
            HttpContext.Current = context;
        }
    }
}

