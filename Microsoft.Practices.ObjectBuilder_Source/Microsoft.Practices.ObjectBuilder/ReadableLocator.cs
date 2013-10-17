namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public abstract class ReadableLocator : IReadableLocator, IEnumerable<KeyValuePair<object, object>>, IEnumerable
    {
        private IReadableLocator parentLocator;

        protected ReadableLocator()
        {
        }

        public bool Contains(object key)
        {
            return this.Contains(key, SearchMode.Up);
        }

        public abstract bool Contains(object key, SearchMode options);
        public IReadableLocator FindBy(Predicate<KeyValuePair<object, object>> predicate)
        {
            return this.FindBy(SearchMode.Up, predicate);
        }

        public IReadableLocator FindBy(SearchMode options, Predicate<KeyValuePair<object, object>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            if (!Enum.IsDefined(typeof(SearchMode), options))
            {
                throw new ArgumentException(Resources.InvalidEnumerationValue, "options");
            }
            Locator results = new Locator();
            for (IReadableLocator locator2 = this; locator2 != null; locator2 = (options == SearchMode.Local) ? null : locator2.ParentLocator)
            {
                this.FindInLocator(predicate, results, locator2);
            }
            return new ReadOnlyLocator(results);
        }

        private void FindInLocator(Predicate<KeyValuePair<object, object>> predicate, Locator results, IReadableLocator currentLocator)
        {
            foreach (KeyValuePair<object, object> pair in currentLocator)
            {
                if (!results.Contains(pair.Key) && predicate(pair))
                {
                    results.Add(pair.Key, pair.Value);
                }
            }
        }

        public TItem Get<TItem>()
        {
            return (TItem) this.Get(typeof(TItem));
        }

        public TItem Get<TItem>(object key)
        {
            return (TItem) this.Get(key);
        }

        public object Get(object key)
        {
            return this.Get(key, SearchMode.Up);
        }

        public abstract object Get(object key, SearchMode options);
        public TItem Get<TItem>(object key, SearchMode options)
        {
            return (TItem) this.Get(key, options);
        }

        public abstract IEnumerator<KeyValuePair<object, object>> GetEnumerator();
        protected void SetParentLocator(IReadableLocator parentLocator)
        {
            this.parentLocator = parentLocator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract int Count { get; }

        public virtual IReadableLocator ParentLocator
        {
            get
            {
                return this.parentLocator;
            }
        }

        public abstract bool ReadOnly { get; }
    }
}

