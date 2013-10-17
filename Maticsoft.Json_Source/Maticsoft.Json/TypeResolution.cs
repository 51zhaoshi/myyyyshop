namespace Maticsoft.Json
{
    using System;

    public sealed class TypeResolution
    {
        private static TypeResolutionHandler _current = (_default = new TypeResolutionHandler(Type.GetType));
        private static readonly TypeResolutionHandler _default;

        private TypeResolution()
        {
            throw new NotSupportedException();
        }

        public static Type FindType(string typeName)
        {
            return Current(typeName, false, false);
        }

        public static Type GetType(string typeName)
        {
            return Current(typeName, true, false);
        }

        public static TypeResolutionHandler Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _current = value;
            }
        }

        public static TypeResolutionHandler Default
        {
            get
            {
                return _default;
            }
        }
    }
}

