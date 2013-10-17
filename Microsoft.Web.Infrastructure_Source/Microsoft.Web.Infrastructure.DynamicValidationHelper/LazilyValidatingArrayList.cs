namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Security;

    [SecurityCritical]
    internal sealed class LazilyValidatingArrayList : ArrayList
    {
        public LazilyValidatingArrayList(ArrayList innerList, SimpleValidateStringCallback validateCallback) : base(innerList.Count)
        {
            for (int i = 0; i < innerList.Count; i++)
            {
                this.Add(new LazilyEvaluatedNameObjectEntry(innerList[i], validateCallback));
            }
        }

        public override object this[int index]
        {
            [SecuritySafeCritical]
            get
            {
                object obj2 = base[index];
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
                base[index] = value;
            }
        }
    }
}

