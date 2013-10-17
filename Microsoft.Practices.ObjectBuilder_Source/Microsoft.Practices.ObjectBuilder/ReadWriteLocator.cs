namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class ReadWriteLocator : ReadableLocator, IReadWriteLocator, IReadableLocator, IEnumerable<KeyValuePair<object, object>>, IEnumerable
    {
        protected ReadWriteLocator()
        {
        }

        public abstract void Add(object key, object value);
        public abstract bool Remove(object key);

        public override bool ReadOnly
        {
            get
            {
                return false;
            }
        }
    }
}

