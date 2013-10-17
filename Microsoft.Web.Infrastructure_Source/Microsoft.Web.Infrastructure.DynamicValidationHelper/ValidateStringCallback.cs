namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Util;

    internal delegate void ValidateStringCallback(string value, string collectionKey, RequestValidationSource requestCollection);
}

