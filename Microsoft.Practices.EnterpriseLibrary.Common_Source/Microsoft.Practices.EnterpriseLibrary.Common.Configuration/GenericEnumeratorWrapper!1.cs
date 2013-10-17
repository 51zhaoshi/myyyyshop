namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class GenericEnumeratorWrapper<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private IEnumerator wrappedEnumerator;

        internal GenericEnumeratorWrapper(IEnumerator wrappedEnumerator)
        {
            this.wrappedEnumerator = wrappedEnumerator;
        }

        bool IEnumerator.MoveNext()
        {
            return this.wrappedEnumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            this.wrappedEnumerator.Reset();
        }

        void IDisposable.Dispose()
        {
            this.wrappedEnumerator = null;
        }

        T IEnumerator<T>.Current
        {
            get
            {
                return (T) this.wrappedEnumerator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.wrappedEnumerator.Current;
            }
        }
    }
}

