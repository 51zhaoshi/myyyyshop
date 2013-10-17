namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections.Generic;

    public class ReadOnlyLocator : ReadableLocator
    {
        private IReadableLocator innerLocator;

        public ReadOnlyLocator(IReadableLocator innerLocator)
        {
            if (innerLocator == null)
            {
                throw new ArgumentNullException("innerLocator");
            }
            this.innerLocator = innerLocator;
        }

        public override bool Contains(object key, SearchMode options)
        {
            return this.innerLocator.Contains(key, options);
        }

        public override object Get(object key, SearchMode options)
        {
            return this.innerLocator.Get(key, options);
        }

        public override IEnumerator<KeyValuePair<object, object>> GetEnumerator()
        {
            return this.innerLocator.GetEnumerator();
        }

        public override int Count
        {
            get
            {
                return this.innerLocator.Count;
            }
        }

        public override IReadableLocator ParentLocator
        {
            get
            {
                return new ReadOnlyLocator(this.innerLocator.ParentLocator);
            }
        }

        public override bool ReadOnly
        {
            get
            {
                return true;
            }
        }
    }
}

