namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Security;

    [SecurityCritical]
    internal sealed class LazilyValidatingHashtable : Hashtable
    {
        public LazilyValidatingHashtable(Hashtable innerTable, SimpleValidateStringCallback validateCallback) : base(innerTable.Count, (IEqualityComparer) StringComparer.OrdinalIgnoreCase)
        {
            foreach (DictionaryEntry entry in innerTable)
            {
                base[entry.Key] = new LazilyEvaluatedNameObjectEntry(entry.Value, validateCallback);
            }
        }

        public override object this[object key]
        {
            [SecuritySafeCritical]
            get
            {
                object obj2 = base[key];
                LazilyEvaluatedNameObjectEntry entry = obj2 as LazilyEvaluatedNameObjectEntry;
                if (entry == null)
                {
                    return obj2;
                }
                return entry.GetValidatedObject();
            }
            [SecuritySafeCritical]
            set
            {
                base[key] = value;
            }
        }
    }
}

