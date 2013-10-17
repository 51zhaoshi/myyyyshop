namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public class ImportAwareImporter : ImporterBase
    {
        public ImportAwareImporter(Type type) : base(type)
        {
        }

        protected virtual IJsonImportable CreateObject()
        {
            return (IJsonImportable) Activator.CreateInstance(base.OutputType);
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            return this.ReflectImport(context, reader);
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return this.ReflectImport(context, reader);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return this.ReflectImport(context, reader);
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            return this.ReflectImport(context, reader);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return this.ReflectImport(context, reader);
        }

        private object ReflectImport(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            IJsonImportable importable = this.CreateObject();
            importable.Import(context, reader);
            return importable;
        }
    }
}

