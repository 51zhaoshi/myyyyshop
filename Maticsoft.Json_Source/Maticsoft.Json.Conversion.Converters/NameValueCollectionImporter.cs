namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Specialized;

    public class NameValueCollectionImporter : ImporterBase
    {
        public NameValueCollectionImporter() : base(typeof(NameValueCollection))
        {
        }

        protected virtual NameValueCollection CreateCollection()
        {
            return new NameValueCollection();
        }

        protected virtual string GetValueAsString(JsonReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (((reader.TokenClass == JsonTokenClass.String) || (reader.TokenClass == JsonTokenClass.Boolean)) || (reader.TokenClass == JsonTokenClass.Number))
            {
                return reader.Text;
            }
            if (reader.TokenClass != JsonTokenClass.Null)
            {
                throw new JsonException(string.Format("Cannot put a JSON {0} value in a NameValueCollection instance.", reader.TokenClass));
            }
            return null;
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
            if (reader.TokenClass != JsonTokenClass.Object)
            {
                throw new JsonException("Expecting object.");
            }
            reader.Read();
            NameValueCollection result = this.CreateCollection();
            while (reader.TokenClass != JsonTokenClass.EndObject)
            {
                string name = reader.ReadMember();
                if (reader.TokenClass == JsonTokenClass.Array)
                {
                    reader.Read();
                    while (reader.TokenClass != JsonTokenClass.EndArray)
                    {
                        result.Add(name, this.GetValueAsString(reader));
                        reader.Read();
                    }
                }
                else
                {
                    result.Add(name, this.GetValueAsString(reader));
                }
                reader.Read();
            }
            return ImporterBase.ReadReturning(reader, result);
        }
    }
}

