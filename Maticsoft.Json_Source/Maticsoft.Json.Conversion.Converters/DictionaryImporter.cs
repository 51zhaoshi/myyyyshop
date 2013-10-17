namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class DictionaryImporter : ImportAwareImporter
    {
        public DictionaryImporter() : base(typeof(IDictionary))
        {
        }

        protected override IJsonImportable CreateObject()
        {
            return new JsonObject();
        }
    }
}

