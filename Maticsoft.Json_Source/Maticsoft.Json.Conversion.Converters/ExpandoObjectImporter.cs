namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;

    public class ExpandoObjectImporter : ImporterBase
    {
        public ExpandoObjectImporter() : base(typeof(ExpandoObject))
        {
        }

        private object ImportArray(ImportContext context, JsonReader reader)
        {
            reader.ReadToken(JsonTokenClass.Array);
            List<object> list = new List<object>();
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                list.Add(this.ImportValue(context, reader));
            }
            reader.Read();
            return new ReadOnlyCollection<object>(list);
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            reader.ReadToken(JsonTokenClass.Object);
            IDictionary<string, object> dictionary = new ExpandoObject();
            while (reader.TokenClass != JsonTokenClass.EndObject)
            {
                dictionary[reader.ReadMember()] = this.ImportValue(context, reader);
            }
            reader.Read();
            return dictionary;
        }

        private object ImportValue(ImportContext context, JsonReader reader)
        {
            if (reader.TokenClass == JsonTokenClass.Object)
            {
                return this.ImportFromObject(context, reader);
            }
            if (reader.TokenClass != JsonTokenClass.Array)
            {
                return context.Import(AnyType.Value, reader);
            }
            return this.ImportArray(context, reader);
        }
    }
}

