namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json.Configuration;
    using System;

    internal sealed class ExporterListSectionHandler : TypeListSectionHandler
    {
        public ExporterListSectionHandler() : base("exporter", typeof(IExporter))
        {
        }
    }
}

