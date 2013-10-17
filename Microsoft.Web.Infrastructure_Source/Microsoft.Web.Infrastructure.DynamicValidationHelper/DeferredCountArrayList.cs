namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Collections;

    internal sealed class DeferredCountArrayList : ArrayList
    {
        private readonly Action _disableValidation;
        private readonly Func<int> _fillInActualFormContents;
        private readonly Func<bool> _hasValidationFired;

        public DeferredCountArrayList(Func<bool> hasValidationFired, Action disableValidation, Func<int> fillInActualFormContents)
        {
            this._hasValidationFired = hasValidationFired;
            this._disableValidation = disableValidation;
            this._fillInActualFormContents = fillInActualFormContents;
        }

        public override int Count
        {
            get
            {
                int num = this._fillInActualFormContents();
                if (this._hasValidationFired())
                {
                    return 0;
                }
                this._disableValidation();
                return num;
            }
        }
    }
}

