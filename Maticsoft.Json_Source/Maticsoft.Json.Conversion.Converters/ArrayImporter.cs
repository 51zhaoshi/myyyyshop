namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections;

    public sealed class ArrayImporter : ImporterBase
    {
        public ArrayImporter() : this(null)
        {
        }

        public ArrayImporter(Type arrayType) : base(AssertArrayType(arrayType))
        {
        }

        private static Type AssertArrayType(Type type)
        {
            if (type == null)
            {
                return typeof(object[]);
            }
            if (!type.IsArray)
            {
                throw new ArgumentException(string.Format("{0} is not an array.", type.FullName), "type");
            }
            if (type.GetArrayRank() != 1)
            {
                throw new ArgumentException(string.Format("{0} is not one-dimension array. Multi-dimensional arrays are not supported.", type.FullName), "arrayType");
            }
            return type;
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            reader.Read();
            ArrayList list = new ArrayList();
            Type elementType = base.OutputType.GetElementType();
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                list.Add(context.Import(elementType, reader));
            }
            return ImporterBase.ReadReturning(reader, list.ToArray(elementType));
        }

        protected override object ImportFromBoolean(ImportContext context, JsonReader reader)
        {
            return this.ImportScalarAsArray(context, reader);
        }

        protected override object ImportFromNumber(ImportContext context, JsonReader reader)
        {
            return this.ImportScalarAsArray(context, reader);
        }

        protected override object ImportFromString(ImportContext context, JsonReader reader)
        {
            return this.ImportScalarAsArray(context, reader);
        }

        private object ImportScalarAsArray(ImportContext context, JsonReader reader)
        {
            Type elementType = base.OutputType.GetElementType();
            Array array = Array.CreateInstance(elementType, 1);
            array.SetValue(context.Import(elementType, reader), 0);
            return array;
        }
    }
}

