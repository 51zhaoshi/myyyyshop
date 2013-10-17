namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface INonObjectMemberImporter
    {
        bool Import(ImportContext context, string name, JsonReader reader);
    }
}

