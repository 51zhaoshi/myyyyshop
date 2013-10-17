namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Collections.Generic;

    public class Locator : ReadWriteLocator
    {
        private WeakRefDictionary<object, object> references;

        public Locator() : this(null)
        {
        }

        public Locator(IReadableLocator parentLocator)
        {
            this.references = new WeakRefDictionary<object, object>();
            base.SetParentLocator(parentLocator);
        }

        public override void Add(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.references.Add(key, value);
        }

        public override bool Contains(object key, SearchMode options)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!Enum.IsDefined(typeof(SearchMode), options))
            {
                throw new ArgumentException(Resources.InvalidEnumerationValue, "options");
            }
            return (this.references.ContainsKey(key) || (((options == SearchMode.Up) && (this.ParentLocator != null)) && this.ParentLocator.Contains(key, options)));
        }

        public override object Get(object key, SearchMode options)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!Enum.IsDefined(typeof(SearchMode), options))
            {
                throw new ArgumentException(Resources.InvalidEnumerationValue, "options");
            }
            if (this.references.ContainsKey(key))
            {
                return this.references[key];
            }
            if ((options == SearchMode.Up) && (this.ParentLocator != null))
            {
                return this.ParentLocator.Get(key, options);
            }
            return null;
        }

        public override IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return this.references.GetEnumerator();
        }

        public override bool Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this.references.Remove(key);
        }

        public override int Count
        {
            get
            {
                return this.references.Count;
            }
        }
    }
}

