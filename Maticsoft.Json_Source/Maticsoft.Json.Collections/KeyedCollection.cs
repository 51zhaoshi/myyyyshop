namespace Maticsoft.Json.Collections
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class KeyedCollection : CollectionBase, IDeserializationCallback
    {
        [NonSerialized]
        private Hashtable _valueByKey;

        protected KeyedCollection()
        {
        }

        protected void Add(object value)
        {
            base.List.Add(value);
        }

        protected bool Contains(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this.ValueByKey.ContainsKey(key);
        }

        protected object GetByKey(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            return this.ValueByKey[key];
        }

        protected abstract object KeyFromValue(object value);
        protected void ListKeysByIndex(Array keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException("keys");
            }
            if (keys.Rank != 1)
            {
                throw new ArgumentException(null, "keys");
            }
            for (int i = 0; i < Math.Min(base.Count, keys.Length); i++)
            {
                keys.SetValue(this.KeyFromValue(base.InnerList[i]), i);
            }
        }

        protected override void OnClearComplete()
        {
            this.ValueByKey.Clear();
            base.OnClearComplete();
        }

        protected virtual void OnDeserializationCallback(object sender)
        {
            foreach (object obj2 in this)
            {
                this.ValueByKey[this.KeyFromValue(obj2)] = obj2;
            }
        }

        protected override void OnInsertComplete(int index, object value)
        {
            this.ValueByKey.Add(this.KeyFromValue(value), value);
            base.OnInsertComplete(index, value);
        }

        protected override void OnRemoveComplete(int index, object value)
        {
            this.ValueByKey.Remove(this.KeyFromValue(value));
            base.OnRemoveComplete(index, value);
        }

        protected override void OnSetComplete(int index, object oldValue, object newValue)
        {
            this.ValueByKey.Remove(this.KeyFromValue(oldValue));
            this.ValueByKey.Add(this.KeyFromValue(newValue), newValue);
            base.OnSetComplete(index, oldValue, newValue);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (this.KeyFromValue(value) == null)
            {
                throw new ArgumentException(null, "value");
            }
        }

        protected bool Remove(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            object byKey = this.GetByKey(key);
            if (byKey == null)
            {
                return false;
            }
            base.List.Remove(byKey);
            return true;
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            this.OnDeserializationCallback(sender);
        }

        private Hashtable ValueByKey
        {
            get
            {
                if (this._valueByKey == null)
                {
                    this._valueByKey = new Hashtable(4);
                }
                return this._valueByKey;
            }
        }
    }
}

