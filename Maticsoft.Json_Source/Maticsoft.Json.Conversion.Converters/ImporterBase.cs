namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;

    public abstract class ImporterBase : IImporter
    {
        private readonly Type _outputType;

        protected ImporterBase(Type outputType)
        {
            if (outputType == null)
            {
                throw new ArgumentNullException("outputType");
            }
            this._outputType = outputType;
        }

        protected virtual JsonException GetImportException(string jsonValueType)
        {
            return new JsonException(string.Format("Cannot import {0} from a JSON {1} value.", this.OutputType, jsonValueType));
        }

        public virtual object Import(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (!reader.MoveToContent())
            {
                throw new JsonException("Unexpected EOF.");
            }
            if (reader.TokenClass == JsonTokenClass.Null)
            {
                return this.ImportNull(context, reader);
            }
            if (reader.TokenClass == JsonTokenClass.String)
            {
                return this.ImportFromString(context, reader);
            }
            if (reader.TokenClass == JsonTokenClass.Number)
            {
                return this.ImportFromNumber(context, reader);
            }
            if (reader.TokenClass == JsonTokenClass.Boolean)
            {
                return this.ImportFromBoolean(context, reader);
            }
            if (reader.TokenClass == JsonTokenClass.Array)
            {
                return this.ImportFromArray(context, reader);
            }
            if (reader.TokenClass != JsonTokenClass.Object)
            {
                throw new JsonException(string.Format("{0} not expected.", reader.TokenClass));
            }
            return this.ImportFromObject(context, reader);
        }

        protected virtual object ImportFromArray(ImportContext context, JsonReader reader)
        {
            return this.ThrowNotSupported(JsonTokenClass.Array);
        }

        protected virtual object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return this.ThrowNotSupported(JsonTokenClass.Boolean);
        }

        protected virtual object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return this.ThrowNotSupported(JsonTokenClass.Number);
        }

        protected virtual object ImportFromObject(ImportContext context, JsonReader reader)
        {
            return this.ThrowNotSupported(JsonTokenClass.Object);
        }

        protected virtual object ImportFromString(ImportContext context, JsonReader reader)
        {
            return this.ThrowNotSupported(JsonTokenClass.String);
        }

        protected virtual object ImportNull(ImportContext context, JsonReader reader)
        {
            reader.Read();
            return null;
        }

        internal static object ReadReturning(JsonReader reader, object result)
        {
            reader.Read();
            return result;
        }

        private object ThrowNotSupported(JsonTokenClass clazz)
        {
            throw this.GetImportException(clazz.Name);
        }

        public Type OutputType
        {
            get
            {
                return this._outputType;
            }
        }
    }
}

