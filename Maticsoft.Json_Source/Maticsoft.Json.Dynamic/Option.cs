namespace Maticsoft.Json.Dynamic
{
    using System;

    internal static class Option
    {
        public static Option<T> Value<T>(T value)
        {
            return new Option<T>(true, value);
        }
    }
}

