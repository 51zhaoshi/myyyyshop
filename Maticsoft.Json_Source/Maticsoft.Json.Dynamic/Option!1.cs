namespace Maticsoft.Json.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    internal struct Option<T> : IEquatable<Option<T>>
    {
        public static readonly Option<T> None;
        private readonly T _value;
        internal Option(bool hasValue, T value)
        {
            this = (Option) new Option<T>();
            this.HasValue = hasValue;
            this._value = value;
        }

        public bool HasValue
        {
            [CompilerGenerated]
            get
            {
                return this.<HasValue>k__BackingField;
            }
            [CompilerGenerated]
            private set
            {
                this.<HasValue>k__BackingField = value;
            }
        }
        public T Value
        {
            get
            {
                if (!this.HasValue)
                {
                    throw new InvalidOperationException("Value is undefined.");
                }
                return this._value;
            }
        }
        public bool Equals(Option<T> other)
        {
            return ((this.HasValue == other.HasValue) && EqualityComparer<T>.Default.Equals(this._value, other.Value));
        }

        public override bool Equals(object obj)
        {
            return ((obj is Option<T>) && this.Equals((Option<T>) obj));
        }

        public override int GetHashCode()
        {
            return (this.HasValue.GetHashCode() ^ (this._value.GetHashCode() * 0x18d));
        }

        public static bool operator ==(Option<T> left, Option<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Option<T> left, Option<T> right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            if (!this.HasValue)
            {
                return string.Empty;
            }
            return string.Format("{0}", this.Value);
        }

        static Option()
        {
            Option<T>.None = new Option<T>();
        }
    }
}

