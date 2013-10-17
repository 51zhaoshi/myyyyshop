namespace Maticsoft.Json
{
    using Maticsoft.Json.Conversion;
    using System;
    using System.Runtime.InteropServices;

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct JsonMember
    {
        private readonly string _name;
        private readonly object _value;
        public JsonMember(string name, object value)
        {
            this._name = name;
            this._value = value;
        }

        [JsonExport]
        public string Name
        {
            get
            {
                return Mask.NullString(this._name);
            }
        }
        [JsonExport]
        public object Value
        {
            get
            {
                return this._value;
            }
        }
        public override string ToString()
        {
            if ((this.Name.Length == 0) && (this.Value == null))
            {
                return string.Empty;
            }
            return (this.Name + ": " + this.Value);
        }
    }
}

