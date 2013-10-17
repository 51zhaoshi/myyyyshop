namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;

    public class DictionaryImporter<TKey, TValue> : ImporterBase
    {
        public DictionaryImporter() : base(typeof(IDictionary<TKey, TValue>))
        {
        }

        protected virtual Dictionary<TKey, TValue> CreateDictionary()
        {
            return new Dictionary<TKey, TValue>(DictionaryImporter<TKey, TValue>.IsKeyOfString ? ((IEqualityComparer<TKey>) StringComparer.Ordinal) : null);
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
            IDictionary<TKey, TValue> result = this.CreateDictionary();
            bool isKeyOfString = DictionaryImporter<TKey, TValue>.IsKeyOfString;
            reader.ReadToken(JsonTokenClass.Object);
            while (reader.TokenClass != JsonTokenClass.EndObject)
            {
                TKey local;
                string text = reader.ReadMember();
                if (isKeyOfString)
                {
                    local = (TKey) text;
                }
                else
                {
                    local = context.Import<TKey>(JsonBuffer.From(JsonToken.String(text)).CreateReader());
                }
                result.Add(local, context.Import<TValue>(reader));
            }
            return ImporterBase.ReadReturning(reader, result);
        }

        private static bool IsKeyOfString
        {
            get
            {
                return (Type.GetTypeCode(typeof(TKey)) == TypeCode.String);
            }
        }
    }
}

