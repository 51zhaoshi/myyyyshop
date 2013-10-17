namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IReadWriteLocator : IReadableLocator, IEnumerable<KeyValuePair<object, object>>, IEnumerable
    {
        void Add(object key, object value);
        bool Remove(object key);
    }
}

