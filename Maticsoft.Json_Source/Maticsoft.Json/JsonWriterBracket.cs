namespace Maticsoft.Json
{
    using System;

    [Serializable]
    public enum JsonWriterBracket
    {
        Pending,
        Array,
        Object,
        Member,
        Closed
    }
}

