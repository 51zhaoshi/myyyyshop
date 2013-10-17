namespace Maticsoft.Json
{
    using System;
    using System.IO;
    using System.Text;

    public sealed class JsonText
    {
        private static JsonTextReaderFactoryHandler _currentReaderFactory = (_defaultReaderFactory = new JsonTextReaderFactoryHandler(JsonText.DefaultReaderFactoryImpl));
        private static JsonTextWriterFactoryHandler _currentWriterFactory = (_defaultWriterFactory = new JsonTextWriterFactoryHandler(JsonText.DefaultWriterFactoryImpl));
        private static readonly JsonTextReaderFactoryHandler _defaultReaderFactory;
        private static readonly JsonTextWriterFactoryHandler _defaultWriterFactory;

        private JsonText()
        {
            throw new NotSupportedException();
        }

        public static JsonReader CreateReader(TextReader reader)
        {
            return CurrentReaderFactory(reader, null);
        }

        public static JsonReader CreateReader(string source)
        {
            return CreateReader(new StringReader(source));
        }

        public static JsonWriter CreateWriter(TextWriter writer)
        {
            return CurrentWriterFactory(writer, null);
        }

        public static JsonWriter CreateWriter(StringBuilder sb)
        {
            return CreateWriter(new StringWriter(sb));
        }

        private static JsonReader DefaultReaderFactoryImpl(TextReader reader, object options)
        {
            return new JsonTextReader(reader);
        }

        private static JsonWriter DefaultWriterFactoryImpl(TextWriter writer, object options)
        {
            return new JsonTextWriter(writer);
        }

        public static JsonTextReaderFactoryHandler CurrentReaderFactory
        {
            get
            {
                return _currentReaderFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _currentReaderFactory = value;
            }
        }

        public static JsonTextWriterFactoryHandler CurrentWriterFactory
        {
            get
            {
                return _currentWriterFactory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _currentWriterFactory = value;
            }
        }

        public static JsonTextReaderFactoryHandler DefaultReaderFactory
        {
            get
            {
                return _defaultReaderFactory;
            }
        }

        public static JsonTextWriterFactoryHandler DefaultWriterFactory
        {
            get
            {
                return _defaultWriterFactory;
            }
        }
    }
}

