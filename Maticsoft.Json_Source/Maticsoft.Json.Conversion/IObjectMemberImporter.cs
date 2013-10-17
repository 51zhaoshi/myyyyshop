namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;

    public interface IObjectMemberImporter
    {
        void Import(ImportContext context, JsonReader reader, object target);
    }
}

