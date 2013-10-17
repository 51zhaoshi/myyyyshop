namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IReadableLocator : IEnumerable<KeyValuePair<object, object>>, IEnumerable
    {
        bool Contains(object key);
        bool Contains(object key, SearchMode options);
        IReadableLocator FindBy(Predicate<KeyValuePair<object, object>> predicate);
        IReadableLocator FindBy(SearchMode options, Predicate<KeyValuePair<object, object>> predicate);
        TItem Get<TItem>();
        TItem Get<TItem>(object key);
        object Get(object key);
        TItem Get<TItem>(object key, SearchMode options);
        object Get(object key, SearchMode options);

        int Count { get; }

        IReadableLocator ParentLocator { get; }

        bool ReadOnly { get; }
    }
}

