namespace Microsoft.Web.Infrastructure.DynamicValidationHelper
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Sequential), SecurityCritical]
    internal struct FieldAccessor<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;
        public FieldAccessor(Func<T> getter, Action<T> setter)
        {
            this._getter = getter;
            this._setter = setter;
        }

        public T Value
        {
            get
            {
                return this._getter();
            }
            set
            {
                this._setter(value);
            }
        }
    }
}

