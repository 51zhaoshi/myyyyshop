namespace Maticsoft.Json.Conversion
{
    using System;

    [Serializable, AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class JsonIgnoreAttribute : Attribute
    {
    }
}

