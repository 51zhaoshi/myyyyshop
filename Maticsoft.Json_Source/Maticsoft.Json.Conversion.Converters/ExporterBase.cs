namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public abstract class ExporterBase : IExporter
    {
        private readonly Type _inputType;

        protected ExporterBase(Type inputType)
        {
            if (inputType == null)
            {
                throw new ArgumentNullException("inputType");
            }
            this._inputType = inputType;
        }

        public virtual void Export(ExportContext context, object value, JsonWriter writer)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            if (JsonNull.LogicallyEquals(value))
            {
                writer.WriteNull();
            }
            else
            {
                this.ExportValue(context, value, writer);
            }
        }

        protected abstract void ExportValue(ExportContext context, object value, JsonWriter writer);

        public Type InputType
        {
            get
            {
                return this._inputType;
            }
        }
    }
}

