namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web;

    internal delegate void GetUnvalidatedCollectionsCallback(HttpContext context, out Func<NameValueCollection> formGetter, out Func<NameValueCollection> queryStringGetter);
}

