namespace Maticsoft.Json.Conversion
{
    using System;

    [Serializable, AttributeUsage(AttributeTargets.Property)]
    public sealed class JsonExportAttribute : Attribute
    {
    }
}

