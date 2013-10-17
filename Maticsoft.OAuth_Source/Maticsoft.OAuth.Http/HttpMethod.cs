namespace Maticsoft.OAuth.Http
{
    using Maticsoft.OAuth.Util;
    using System;
    using System.Globalization;

    [Serializable]
    public class HttpMethod : IEquatable<HttpMethod>
    {
        public static readonly HttpMethod CONNECT = new HttpMethod("CONNECT");
        public static readonly HttpMethod DELETE = new HttpMethod("DELETE");
        public static readonly HttpMethod GET = new HttpMethod("GET");
        public static readonly HttpMethod HEAD = new HttpMethod("HEAD");
        private string method;
        public static readonly HttpMethod OPTIONS = new HttpMethod("OPTIONS");
        public static readonly HttpMethod POST = new HttpMethod("POST");
        public static readonly HttpMethod PUT = new HttpMethod("PUT");
        public static readonly HttpMethod TRACE = new HttpMethod("TRACE");

        public HttpMethod(string method)
        {
            ArgumentUtils.AssertNotNull(method, "method");
            this.method = method;
        }

        public bool Equals(HttpMethod other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            return (object.ReferenceEquals(this.method, other.method) || string.Equals(this.method, other.method, StringComparison.OrdinalIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as HttpMethod);
        }

        public override int GetHashCode()
        {
            return this.method.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
        }

        public static bool operator ==(HttpMethod method1, HttpMethod method2)
        {
            if (object.ReferenceEquals(method1, null))
            {
                return object.ReferenceEquals(method2, null);
            }
            return method1.Equals(method2);
        }

        public static bool operator !=(HttpMethod method1, HttpMethod method2)
        {
            return !(method1 == method2);
        }

        public override string ToString()
        {
            return this.method;
        }
    }
}

