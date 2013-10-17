namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion.Converters;
    using Maticsoft.Json.Reflection;
    using System;
    using System.Collections;
    using System.Configuration;

    [Serializable]
    public class ExportContext
    {
        private ExporterCollection _exporters;
        private IDictionary _items;
        private static ExporterCollection _stockExporters;

        public virtual void Export(object value, JsonWriter writer)
        {
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
                IExporter exporter = this.FindExporter(value.GetType());
                if (exporter != null)
                {
                    exporter.Export(this, value, writer);
                }
                else
                {
                    writer.WriteString(value.ToString());
                }
            }
        }

        private IExporter FindBaseExporter(Type baseType, Type actualType)
        {
            if (baseType == typeof(object))
            {
                return null;
            }
            IExporter exporter = this.Exporters[baseType];
            if (exporter == null)
            {
                exporter = StockExporters[baseType];
                if (exporter == null)
                {
                    return this.FindBaseExporter(baseType.BaseType, actualType);
                }
            }
            return (IExporter) Activator.CreateInstance(exporter.GetType(), new object[] { actualType });
        }

        private IExporter FindCompatibleExporter(Type type)
        {
            if (typeof(IJsonExportable).IsAssignableFrom(type))
            {
                return new ExportAwareExporter(type);
            }
            if (Reflector.IsConstructionOfNullable(type))
            {
                return new NullableExporter(type);
            }
            if (Reflector.IsTupleFamily(type))
            {
                return new TupleExporter(type);
            }
            if (type.IsClass && (type != typeof(object)))
            {
                IExporter exporter = this.FindBaseExporter(type.BaseType, type);
                if (exporter != null)
                {
                    return exporter;
                }
            }
            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                return new DictionaryExporter(type);
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return new EnumerableExporter(type);
            }
            if ((type.IsPublic || type.IsNestedPublic) && ((!type.IsPrimitive && !type.IsEnum) && (type.IsValueType || (type.GetConstructor(Type.EmptyTypes) != null))))
            {
                if (!type.IsValueType)
                {
                    return new ComponentExporter(type);
                }
                CustomTypeDescriptor descriptor = new CustomTypeDescriptor(type);
                if (descriptor.GetProperties().Count > 0)
                {
                    return new ComponentExporter(type, descriptor);
                }
            }
            CustomTypeDescriptor typeDescriptor = CustomTypeDescriptor.TryCreateForAnonymousClass(type);
            if (typeDescriptor != null)
            {
                return new ComponentExporter(type, typeDescriptor);
            }
            return new StringExporter(type);
        }

        public virtual IExporter FindExporter(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            IExporter exporter = this.Exporters[type];
            if (exporter != null)
            {
                return exporter;
            }
            exporter = StockExporters[type];
            if (exporter == null)
            {
                exporter = this.FindCompatibleExporter(type);
            }
            if (exporter != null)
            {
                this.Register(exporter);
                return exporter;
            }
            return null;
        }

        public virtual void Register(IExporter exporter)
        {
            if (exporter == null)
            {
                throw new ArgumentNullException("exporter");
            }
            this.Exporters.Put(exporter);
        }

        private ExporterCollection Exporters
        {
            get
            {
                if (this._exporters == null)
                {
                    this._exporters = new ExporterCollection();
                }
                return this._exporters;
            }
        }

        public IDictionary Items
        {
            get
            {
                if (this._items == null)
                {
                    this._items = new Hashtable();
                }
                return this._items;
            }
        }

        private static ExporterCollection StockExporters
        {
            get
            {
                if (_stockExporters == null)
                {
                    ExporterCollection exporters = new ExporterCollection();
                    exporters.Add(new ByteExporter());
                    exporters.Add(new Int16Exporter());
                    exporters.Add(new Int32Exporter());
                    exporters.Add(new Int64Exporter());
                    exporters.Add(new SingleExporter());
                    exporters.Add(new DoubleExporter());
                    exporters.Add(new DecimalExporter());
                    exporters.Add(new StringExporter());
                    exporters.Add(new BooleanExporter());
                    exporters.Add(new DateTimeExporter());
                    exporters.Add(new JsonNumberExporter());
                    exporters.Add(new JsonBufferExporter());
                    exporters.Add(new ByteArrayExporter());
                    exporters.Add(new DataRowViewExporter());
                    exporters.Add(new NameValueCollectionExporter());
                    exporters.Add(new DataSetExporter());
                    exporters.Add(new DataTableExporter());
                    exporters.Add(new DataRowExporter());
                    exporters.Add(new DbDataRecordExporter());
                    exporters.Add(new ControlExporter());
                    exporters.Add(new BigIntegerExporter());
                    exporters.Add(new ExpandoObjectExporter());
                    IList config = (IList) ConfigurationSettings.GetConfig("maticsoft/json.conversion.exporters");
                    if ((config != null) && (config.Count > 0))
                    {
                        foreach (Type type in config)
                        {
                            exporters.Put((IExporter) Activator.CreateInstance(type));
                        }
                    }
                    _stockExporters = exporters;
                }
                return _stockExporters;
            }
        }
    }
}

