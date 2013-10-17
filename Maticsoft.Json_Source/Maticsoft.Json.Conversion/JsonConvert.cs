namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using System;
    using System.IO;
    using System.Text;

    public sealed class JsonConvert
    {
        private static ExportContextFactoryHandler _currentExportContextFactoryHandler = (_defaultExportContextFactoryHandler = new ExportContextFactoryHandler(JsonConvert.CreateDefaultExportContext));
        private static ImportContextFactoryHandler _currentImportContextFactoryHandler = (_defaultImportContextFactoryHandler = new ImportContextFactoryHandler(JsonConvert.CreateDefaultImportContext));
        private static readonly ExportContextFactoryHandler _defaultExportContextFactoryHandler;
        private static readonly ImportContextFactoryHandler _defaultImportContextFactoryHandler;

        private JsonConvert()
        {
            throw new NotSupportedException();
        }

        private static ExportContext CreateDefaultExportContext()
        {
            return new ExportContext();
        }

        private static ImportContext CreateDefaultImportContext()
        {
            return new ImportContext();
        }

        public static ExportContext CreateExportContext()
        {
            return CurrentExportContextFactory();
        }

        public static ImportContext CreateImportContext()
        {
            return CurrentImportContextFactory();
        }

        public static void Export(object value, JsonWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            CreateExportContext().Export(value, writer);
        }

        public static void Export(object value, TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }
            Export(value, JsonText.CreateWriter(writer));
        }

        public static void Export(object value, StringBuilder sb)
        {
            if (sb == null)
            {
                throw new ArgumentNullException("sb");
            }
            Export(value, new StringWriter(sb));
        }

        public static string ExportToString(object value)
        {
            StringBuilder sb = new StringBuilder();
            Export(value, sb);
            return sb.ToString();
        }

        public static object Import(JsonReader reader)
        {
            return Import(AnyType.Value, reader);
        }

        public static T Import<T>(JsonReader reader)
        {
            return (T) Import(typeof(T), reader);
        }

        public static object Import(TextReader reader)
        {
            return Import(JsonText.CreateReader(reader));
        }

        public static T Import<T>(TextReader reader)
        {
            return (T) Import(typeof(T), reader);
        }

        public static object Import(string text)
        {
            return Import(new StringReader(text));
        }

        public static T Import<T>(string text)
        {
            return (T) Import(typeof(T), text);
        }

        public static object Import(Type type, JsonReader reader)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            return CreateImportContext().Import(type, reader);
        }

        public static object Import(Type type, TextReader reader)
        {
            return Import(type, JsonText.CreateReader(reader));
        }

        public static object Import(Type type, string text)
        {
            return Import(type, new StringReader(text));
        }

        public static ExportContextFactoryHandler CurrentExportContextFactory
        {
            get
            {
                return _currentExportContextFactoryHandler;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _currentExportContextFactoryHandler = value;
            }
        }

        public static ImportContextFactoryHandler CurrentImportContextFactory
        {
            get
            {
                return _currentImportContextFactoryHandler;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _currentImportContextFactoryHandler = value;
            }
        }

        public static ExportContextFactoryHandler DefaultExportContextFactory
        {
            get
            {
                return _defaultExportContextFactoryHandler;
            }
        }

        public static ImportContextFactoryHandler DefaultImportContextFactory
        {
            get
            {
                return _defaultImportContextFactoryHandler;
            }
        }
    }
}

