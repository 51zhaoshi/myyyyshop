namespace Microsoft.Practices.ObjectBuilder
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class LifetimeContainer : ILifetimeContainer, IEnumerable<object>, IEnumerable, IDisposable
    {
        private List<object> items = new List<object>();

        public void Add(object item)
        {
            this.items.Add(item);
        }

        public bool Contains(object item)
        {
            return this.items.Contains(item);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                List<object> list = new List<object>(this.items);
                list.Reverse();
                foreach (object obj2 in list)
                {
                    IDisposable disposable = obj2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                this.items.Clear();
            }
        }

        public IEnumerator<object> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        public void Remove(object item)
        {
            if (this.items.Contains(item))
            {
                this.items.Remove(item);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }
    }
}

