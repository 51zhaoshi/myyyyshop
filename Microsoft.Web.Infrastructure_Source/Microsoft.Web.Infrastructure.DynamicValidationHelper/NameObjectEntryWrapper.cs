namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Security;

    [SecurityCritical]
    internal sealed class NameObjectEntryWrapper
    {
        private readonly object _nameObjectEntry;
        private static readonly GranularValidationReflectionUtil _reflectionUtil = GranularValidationReflectionUtil.Instance;

        private NameObjectEntryWrapper(object nameObjectEntry)
        {
            this._nameObjectEntry = nameObjectEntry;
        }

        public static NameObjectEntryWrapper Wrap(object nameObjectEntry)
        {
            if (nameObjectEntry == null)
            {
                return null;
            }
            return new NameObjectEntryWrapper(nameObjectEntry);
        }

        public string Key
        {
            get
            {
                return _reflectionUtil.GetNameObjectEntryKey(this._nameObjectEntry);
            }
        }

        public object Value
        {
            get
            {
                return _reflectionUtil.GetNameObjectEntryValue(this._nameObjectEntry);
            }
            set
            {
                _reflectionUtil.SetNameObjectEntryValue(this._nameObjectEntry, value);
            }
        }
    }
}

