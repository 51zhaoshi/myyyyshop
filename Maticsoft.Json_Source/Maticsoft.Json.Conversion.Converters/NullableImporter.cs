namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using Maticsoft.Json.Reflection;
    using System;

    public class NullableImporter : ImporterBase
    {
        private readonly Type _underlyingType;

        public NullableImporter(Type outputType) : base(outputType)
        {
            if (!Reflector.IsConstructionOfNullable(outputType))
            {
                throw new ArgumentException(null, "outputType");
            }
            this._underlyingType = Nullable.GetUnderlyingType(outputType);
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            return this.ImportUnderlyingType(context, reader);
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return this.ImportUnderlyingType(context, reader);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return this.ImportUnderlyingType(context, reader);
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            return this.ImportUnderlyingType(context, reader);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return this.ImportUnderlyingType(context, reader);
        }

        private object ImportUnderlyingType(ImportContext context, JsonReader reader)
        {
            return context.Import(this._underlyingType, reader);
        }
    }
}

