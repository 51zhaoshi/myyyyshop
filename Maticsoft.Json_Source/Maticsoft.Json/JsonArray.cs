namespace Maticsoft.Json
{
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class JsonArray : CollectionBase, IJsonImportable, IJsonExportable, IList<object>, ICollection<object>, IEnumerable<object>, IEnumerable
    {
        public JsonArray()
        {
        }

        public JsonArray(IEnumerable collection)
        {
            foreach (object obj2 in collection)
            {
                base.InnerList.Add(obj2);
            }
        }

        public virtual void Add(object value)
        {
            base.InnerList.Add(value);
        }

        public virtual JsonArray Concat(params object[] values)
        {
            JsonArray array = new JsonArray(this);
            if (values != null)
            {
                foreach (object obj2 in values)
                {
                    JsonArray array2 = obj2 as JsonArray;
                    if (array2 != null)
                    {
                        foreach (object obj3 in array2)
                        {
                            array.Push(obj3);
                        }
                    }
                    else
                    {
                        array.Push(obj2);
                    }
                }
            }
            return array;
        }

        public virtual bool Contains(object value)
        {
            return base.InnerList.Contains(value);
        }

        public void CopyTo(object[] array, int arrayIndex)
        {
            base.InnerList.CopyTo(array, arrayIndex);
        }

        public virtual void Export(JsonWriter writer)
        {
            this.Export(JsonConvert.CreateExportContext(), writer);
        }

        protected virtual void Export(ExportContext context, JsonWriter writer)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            writer.WriteStartArray();
            foreach (object obj2 in this)
            {
                context.Export(obj2, writer);
            }
            writer.WriteEndArray();
        }

        public virtual JsonArray GetArray(int index)
        {
            return (JsonArray) this.GetValue(index);
        }

        public virtual bool GetBoolean(int index)
        {
            return this.GetBoolean(index, false);
        }

        public virtual bool GetBoolean(int index, bool defaultValue)
        {
            object obj2 = this.GetValue(index);
            if (obj2 == null)
            {
                return defaultValue;
            }
            return Convert.ToBoolean(obj2, CultureInfo.InvariantCulture);
        }

        public virtual double GetDouble(int index)
        {
            return this.GetDouble(index, float.NaN);
        }

        public virtual double GetDouble(int index, float defaultValue)
        {
            object obj2 = this.GetValue(index);
            if (obj2 == null)
            {
                return (double) defaultValue;
            }
            return Convert.ToDouble(obj2, CultureInfo.InvariantCulture);
        }

        public IEnumerator<object> GetEnumerator()
        {
            IEnumerator enumerator = this.InnerList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object current = enumerator.Current;
                yield return current;
            }
        }

        public virtual int GetInt32(int index)
        {
            return this.GetInt32(index, 0);
        }

        public virtual int GetInt32(int index, int defaultValue)
        {
            object obj2 = this.GetValue(index);
            if (obj2 == null)
            {
                return defaultValue;
            }
            return Convert.ToInt32(obj2, CultureInfo.InvariantCulture);
        }

        public virtual JsonObject GetObject(int index)
        {
            return (JsonObject) this.GetValue(index);
        }

        public virtual string GetString(int index)
        {
            return this.GetString(index, string.Empty);
        }

        public virtual string GetString(int index, string defaultValue)
        {
            object obj2 = this.GetValue(index);
            if (obj2 == null)
            {
                return defaultValue;
            }
            return obj2.ToString();
        }

        public virtual object GetValue(int index)
        {
            return this.GetValue(index, null);
        }

        public virtual object GetValue(int index, object defaultValue)
        {
            object obj2 = this[index];
            if (obj2 == null)
            {
                return defaultValue;
            }
            return obj2;
        }

        public virtual bool HasValueAt(int index)
        {
            return (this[index] != null);
        }

        public virtual void Import(JsonReader reader)
        {
            this.Import(JsonConvert.CreateImportContext(), reader);
        }

        protected virtual void Import(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            ArrayList c = new ArrayList();
            reader.ReadToken(JsonTokenClass.Array);
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                c.Add(context.Import(reader));
            }
            reader.Read();
            base.InnerList.Clear();
            base.InnerList.AddRange(c);
        }

        public virtual int IndexOf(object value)
        {
            return base.InnerList.IndexOf(value);
        }

        void IJsonExportable.Export(ExportContext context, JsonWriter writer)
        {
            this.Export(context, writer);
        }

        void IJsonImportable.Import(ImportContext context, JsonReader reader)
        {
            this.Import(context, reader);
        }

        protected override void OnValidate(object value)
        {
        }

        public virtual object Pop()
        {
            if (base.Count == 0)
            {
                return null;
            }
            object obj2 = base.InnerList[base.Count - 1];
            base.RemoveAt(base.Count - 1);
            return obj2;
        }

        public virtual int Push(params object[] values)
        {
            if (values != null)
            {
                foreach (object obj2 in values)
                {
                    this.Push(obj2);
                }
            }
            return base.Count;
        }

        public virtual int Push(object value)
        {
            this.Add(value);
            return base.Count;
        }

        public void Put(object value)
        {
            this.Add(value);
        }

        public virtual void Remove(object value)
        {
            base.InnerList.Remove(value);
        }

        public virtual void Reverse()
        {
            base.InnerList.Reverse();
        }

        public virtual object Shift()
        {
            if (base.Count == 0)
            {
                return null;
            }
            object obj2 = base.InnerList[0];
            base.RemoveAt(0);
            return obj2;
        }

        bool ICollection<object>.Remove(object item)
        {
            int index = this.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            base.RemoveAt(index);
            return true;
        }

        void IList<object>.Insert(int index, object item)
        {
            base.List.Insert(index, item);
        }

        public virtual object[] ToArray()
        {
            return (object[]) this.ToArray(typeof(object));
        }

        public virtual Array ToArray(Type elementType)
        {
            return base.InnerList.ToArray(elementType);
        }

        public override string ToString()
        {
            StringWriter writer = new StringWriter();
            this.Export(JsonText.CreateWriter(writer));
            return writer.ToString();
        }

        public virtual void Unshift(object value)
        {
            base.InnerList.Insert(0, value);
        }

        public virtual void Unshift(params object[] values)
        {
            if (values != null)
            {
                foreach (object obj2 in values)
                {
                    this.Unshift(obj2);
                }
            }
        }

        public virtual object this[int index]
        {
            get
            {
                return base.InnerList[index];
            }
            set
            {
                base.InnerList[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return base.Count;
            }
        }

        bool ICollection<object>.IsReadOnly
        {
            get
            {
                return base.InnerList.IsReadOnly;
            }
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__0 : IEnumerator<object>, IEnumerator, IDisposable
        {
            private int <>1__state;
            private object <>2__current;
            public JsonArray <>4__this;
            public IEnumerator <>7__wrap2;
            public IDisposable <>7__wrap3;
            public object <item>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__0(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private void <>m__Finally4()
            {
                this.<>1__state = -1;
                this.<>7__wrap3 = this.<>7__wrap2 as IDisposable;
                if (this.<>7__wrap3 != null)
                {
                    this.<>7__wrap3.Dispose();
                }
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
                            this.<>7__wrap2 = this.<>4__this.InnerList.GetEnumerator();
                            this.<>1__state = 1;
                            goto Label_0070;

                        case 2:
                            this.<>1__state = 1;
                            goto Label_0070;

                        default:
                            goto Label_0083;
                    }
                Label_0041:
                    this.<item>5__1 = this.<>7__wrap2.Current;
                    this.<>2__current = this.<item>5__1;
                    this.<>1__state = 2;
                    return true;
                Label_0070:
                    if (this.<>7__wrap2.MoveNext())
                    {
                        goto Label_0041;
                    }
                    this.<>m__Finally4();
                Label_0083:
                    flag = false;
                }
                fault
                {
                    this.System.IDisposable.Dispose();
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
                            this.<>m__Finally4();
                        }
                        return;
                }
            }

            object IEnumerator<object>.Current
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
    }
}

