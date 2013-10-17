namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class ListImporter : ImportAwareImporter
    {
        public ListImporter() : base(typeof(IList))
        {
        }

        protected override IJsonImportable CreateObject()
        {
            return new JsonArray();
        }
    }
}

