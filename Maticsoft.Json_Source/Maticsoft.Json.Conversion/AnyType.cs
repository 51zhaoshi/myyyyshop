namespace Maticsoft.Json.Conversion
{
    using System;

    public sealed class AnyType
    {
        public static readonly Type Value = typeof(object);

        private AnyType()
        {
            throw new NotImplementedException();
        }
    }
}

