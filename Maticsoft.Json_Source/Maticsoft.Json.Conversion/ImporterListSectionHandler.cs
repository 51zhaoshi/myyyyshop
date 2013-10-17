namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json.Configuration;
    using System;

    internal sealed class ImporterListSectionHandler : TypeListSectionHandler
    {
        public ImporterListSectionHandler() : base("importer", typeof(IImporter))
        {
        }
    }
}

