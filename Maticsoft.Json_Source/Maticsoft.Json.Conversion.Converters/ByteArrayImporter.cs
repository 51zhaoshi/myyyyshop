namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.IO;

    public sealed class ByteArrayImporter : ImporterBase
    {
        public ByteArrayImporter() : base(typeof(byte[]))
        {
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            reader.Read();
            MemoryStream stream = new MemoryStream();
            Type type = typeof(byte);
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                stream.WriteByte((byte) context.Import(type, reader));
            }
            return ImporterBase.ReadReturning(reader, stream.ToArray());
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return new byte[] { (reader.ReadBoolean() ? ((byte) 1) : ((byte) 0)) };
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return new byte[] { reader.ReadNumber().ToByte() };
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            object obj2;
            try
            {
                obj2 = Convert.FromBase64String(reader.ReadString());
            }
            catch (FormatException exception)
            {
                throw new JsonException("Error converting JSON String containing base64-encode data to " + base.OutputType.FullName + ".", exception);
            }
            return obj2;
        }
    }
}

