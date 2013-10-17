namespace Maticsoft.Json
{
    using System;

    public sealed class JsonBoolean
    {
        public const string FalseText = "false";
        public const string TrueText = "true";

        private JsonBoolean()
        {
            throw new NotSupportedException();
        }
    }
}

