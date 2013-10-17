namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Json.Reflection;
    using System;

    public class NullableExporter : ExporterBase
    {
        public NullableExporter(Type inputType) : base(inputType)
        {
            if (!Reflector.IsConstructionOfNullable(inputType))
            {
                throw new ArgumentException(null, "inputType");
            }
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            context.Export(value, writer);
        }
    }
}

