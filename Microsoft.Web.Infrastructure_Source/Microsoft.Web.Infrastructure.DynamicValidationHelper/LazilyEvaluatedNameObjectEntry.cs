namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections;
    using System.Security;
    using System.Text;

    [SecurityCritical]
    internal sealed class LazilyEvaluatedNameObjectEntry
    {
        private bool _hasBeenValidated;
        private readonly object _nameObjectEntry;
        private readonly NameObjectEntryWrapper _nameObjectEntryWrapper;
        private readonly SimpleValidateStringCallback _validateCallback;

        public LazilyEvaluatedNameObjectEntry(object nameObjectEntry, SimpleValidateStringCallback validateCallback)
        {
            this._nameObjectEntry = nameObjectEntry;
            this._nameObjectEntryWrapper = NameObjectEntryWrapper.Wrap(nameObjectEntry);
            this._validateCallback = validateCallback;
        }

        private static string GetAsOneString(ArrayList list)
        {
            int num = (list != null) ? list.Count : 0;
            if (num == 1)
            {
                return (string) list[0];
            }
            if (num <= 1)
            {
                return null;
            }
            StringBuilder builder = new StringBuilder((string) list[0]);
            for (int i = 1; i < num; i++)
            {
                builder.Append(',');
                builder.Append((string) list[i]);
            }
            return builder.ToString();
        }

        private static string GetAsOneString(object value)
        {
            ArrayList list = value as ArrayList;
            if (list != null)
            {
                return GetAsOneString(list);
            }
            return null;
        }

        public object GetValidatedObject()
        {
            if (!this._hasBeenValidated)
            {
                this.ValidateObject();
                this._hasBeenValidated = true;
            }
            return this._nameObjectEntry;
        }

        private void ValidateObject()
        {
            string asOneString = GetAsOneString(this._nameObjectEntryWrapper.Value);
            this._validateCallback(asOneString, this._nameObjectEntryWrapper.Key);
        }
    }
}

