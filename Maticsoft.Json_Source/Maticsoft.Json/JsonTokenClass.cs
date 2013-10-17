namespace Maticsoft.Json
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class JsonTokenClass : IObjectReference
    {
        private readonly string _name;
        [NonSerialized]
        private readonly Superclass _superclass;
        public static readonly ICollection All = new JsonTokenClass[] { BOF, EOF, Null, Boolean, Number, String, Array, EndArray, Object, EndObject, Member };
        public static readonly JsonTokenClass Array = new JsonTokenClass("Array");
        public static readonly JsonTokenClass BOF = new JsonTokenClass("BOF", Superclass.Terminator);
        public static readonly JsonTokenClass Boolean = new JsonTokenClass("Boolean", Superclass.Scalar);
        public static readonly JsonTokenClass EndArray = new JsonTokenClass("EndArray", Superclass.Terminator);
        public static readonly JsonTokenClass EndObject = new JsonTokenClass("EndObject", Superclass.Terminator);
        public static readonly JsonTokenClass EOF = new JsonTokenClass("EOF", Superclass.Terminator);
        public static readonly JsonTokenClass Member = new JsonTokenClass("Member");
        public static readonly JsonTokenClass Null = new JsonTokenClass("Null");
        public static readonly JsonTokenClass Number = new JsonTokenClass("Number", Superclass.Scalar);
        public static readonly JsonTokenClass Object = new JsonTokenClass("Object");
        public static readonly JsonTokenClass String = new JsonTokenClass("String", Superclass.Scalar);

        private JsonTokenClass(string name) : this(name, Superclass.Unspecified)
        {
        }

        private JsonTokenClass(string name, Superclass superclass)
        {
            this._name = name;
            this._superclass = superclass;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        object IObjectReference.GetRealObject(StreamingContext context)
        {
            foreach (JsonTokenClass class2 in All)
            {
                if (string.CompareOrdinal(class2.Name, this.Name) == 0)
                {
                    return class2;
                }
            }
            throw new SerializationException(string.Format("{0} is not a valid {1} instance.", this.Name, typeof(JsonTokenClass).FullName));
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal bool IsScalar
        {
            get
            {
                return (this._superclass == Superclass.Scalar);
            }
        }

        internal bool IsTerminator
        {
            get
            {
                return (this._superclass == Superclass.Terminator);
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        private enum Superclass
        {
            Unspecified,
            Scalar,
            Terminator
        }
    }
}

