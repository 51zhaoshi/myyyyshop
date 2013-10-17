namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections.Specialized;

    internal sealed class ReadOnlyNameValueCollection : NameValueCollection
    {
        public ReadOnlyNameValueCollection(NameValueCollection col) : base(col)
        {
            base.IsReadOnly = true;
        }
    }
}

