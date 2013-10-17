namespace Microsoft.Practices.ObjectBuilder
{
    using Microsoft.Practices.ObjectBuilder.Properties;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class WeakRefDictionary<TKey, TValue>
    {
        private Dictionary<TKey, WeakReference> inner;

        public WeakRefDictionary()
        {
            this.inner = new Dictionary<TKey, WeakReference>();
        }

        public void Add(TKey key, TValue value)
        {
            TValue local;
            if (this.TryGet(key, out local))
            {
                throw new ArgumentException(Resources.KeyAlreadyPresentInDictionary);
            }
            this.inner.Add(key, new WeakReference(this.EncodeNullObject(value)));
        }

        private void CleanAbandonedItems()
        {
            List<TKey> list = new List<TKey>();
            foreach (KeyValuePair<TKey, WeakReference> pair in this.inner)
            {
                if (pair.Value.Target == null)
                {
                    list.Add(pair.Key);
                }
            }
            foreach (TKey local in list)
            {
                this.inner.Remove(local);
            }
        }

        public bool ContainsKey(TKey key)
        {
            TValue local;
            return this.TryGet(key, out local);
        }

        private TObject DecodeNullObject<TObject>(object innerValue)
        {
            if (innerValue == typeof(NullObject<TKey, TValue>))
            {
                return default(TObject);
            }
            return (TObject) innerValue;
        }

        private object EncodeNullObject(object value)
        {
            if (value == null)
            {
                return typeof(NullObject<TKey, TValue>);
            }
            return value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            Dictionary<TKey, WeakReference>.Enumerator enumerator = this.inner.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<TKey, WeakReference> current = enumerator.Current;
                object target = current.Value.Target;
                if (target != null)
                {
                    yield return new KeyValuePair<TKey, TValue>(current.Key, this.DecodeNullObject<TValue>(target));
                }
            }
            enumerator.Dispose();
        }

        public bool Remove(TKey key)
        {
            return this.inner.Remove(key);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            WeakReference reference;
            value = default(TValue);
            if (!this.inner.TryGetValue(key, out reference))
            {
                return false;
            }
            object target = reference.Target;
            if (target == null)
            {
                this.inner.Remove(key);
                return false;
            }
            value = this.DecodeNullObject<TValue>(target);
            return true;
        }

        public int Count
        {
            get
            {
                this.CleanAbandonedItems();
                return this.inner.Count;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue local;
                if (!this.TryGet(key, out local))
                {
                    throw new KeyNotFoundException();
                }
                return local;
            }
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__0 : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private KeyValuePair<TKey, TValue> <>2__current;
            public WeakRefDictionary<TKey, TValue> <>4__this;
            public Dictionary<TKey, WeakReference>.Enumerator <>7__wrap3;
            public object <innerValue>5__2;
            public KeyValuePair<TKey, WeakReference> <kvp>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    switch (this.<>1__state)
                    {
                        case 0:
                            this.<>1__state = -1;
                            this.<>7__wrap3 = this.<>4__this.inner.GetEnumerator();
                            this.<>1__state = 1;
                            goto Label_00B2;

                        case 2:
                            this.<>1__state = 1;
                            goto Label_00B2;

                        default:
                            goto Label_00D7;
                    }
                Label_0044:
                    this.<kvp>5__1 = this.<>7__wrap3.Current;
                    this.<innerValue>5__2 = this.<kvp>5__1.Value.Target;
                    if (this.<innerValue>5__2 != null)
                    {
                        this.<>2__current = new KeyValuePair<TKey, TValue>(this.<kvp>5__1.Key, this.<>4__this.DecodeNullObject<TValue>(this.<innerValue>5__2));
                        this.<>1__state = 2;
                        return true;
                    }
                Label_00B2:
                    if (this.<>7__wrap3.MoveNext())
                    {
                        goto Label_0044;
                    }
                    this.<>1__state = -1;
                    this.<>7__wrap3.Dispose();
                Label_00D7:
                    flag = false;
                }
                fault
                {
                    ((IDisposable) this).Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
                switch (this.<>1__state)
                {
                    case 1:
                    case 2:
                        try
                        {
                        }
                        finally
                        {
                            this.<>1__state = -1;
                            this.<>7__wrap3.Dispose();
                        }
                        return;
                }
            }

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this.<>2__current;
                }
            }
        }

        private class NullObject
        {
        }
    }
}

