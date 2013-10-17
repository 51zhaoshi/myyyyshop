namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion.Converters;
    using Maticsoft.Json.Reflection;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    [Serializable]
    public class ImportContext
    {
        private ImporterCollection _importers;
        private IDictionary _items;
        private static ImporterCollection _stockImporters;

        private static IImporter FindCompatibleImporter(Type type)
        {
            if (typeof(IJsonImportable).IsAssignableFrom(type))
            {
                return new ImportAwareImporter(type);
            }
            if (type.IsArray && (type.GetArrayRank() == 1))
            {
                return new ArrayImporter(type);
            }
            if (type.IsEnum)
            {
                return new EnumImporter(type);
            }
            if (Reflector.IsConstructionOfNullable(type))
            {
                return new NullableImporter(type);
            }
            bool flag = Reflector.IsConstructionOfGenericTypeDefinition(type, typeof(IList<>));
            bool flag2 = !flag && Reflector.IsConstructionOfGenericTypeDefinition(type, typeof(ICollection<>));
            bool flag3 = !flag2 && ((type == typeof(IEnumerable)) || Reflector.IsConstructionOfGenericTypeDefinition(type, typeof(IEnumerable<>)));
            if ((flag || flag2) || flag3)
            {
                Type type2 = type.IsGenericType ? type.GetGenericArguments()[0] : typeof(object);
                return (IImporter) Activator.CreateInstance(typeof(CollectionImporter<,>).MakeGenericType(new Type[] { type, type2 }), new object[] { flag3 });
            }
            if (Reflector.IsConstructionOfGenericTypeDefinition(type, typeof(IDictionary<,>)))
            {
                return (IImporter) Activator.CreateInstance(typeof(DictionaryImporter<,>).MakeGenericType(type.GetGenericArguments()));
            }
            if (Reflector.IsConstructionOfGenericTypeDefinition(type, typeof(ISet<>)))
            {
                Type[] genericArguments = type.GetGenericArguments();
                Type type4 = typeof(HashSet<>).MakeGenericType(genericArguments);
                return (IImporter) Activator.CreateInstance(typeof(CollectionImporter<,,>).MakeGenericType(new Type[] { type4, type, genericArguments[0] }));
            }
            if (Reflector.IsTupleFamily(type))
            {
                return new TupleImporter(type);
            }
            if ((type.IsPublic || type.IsNestedPublic) && (!type.IsPrimitive && (type.IsValueType || (type.GetConstructor(Type.EmptyTypes) != null))))
            {
                if (!type.IsValueType)
                {
                    return new ComponentImporter(type);
                }
                CustomTypeDescriptor typeDescriptor = new CustomTypeDescriptor(type);
                if (typeDescriptor.GetProperties().Count > 0)
                {
                    return new ComponentImporter(type, typeDescriptor);
                }
            }
            return null;
        }

        public virtual IImporter FindImporter(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            IImporter importer = this.Importers[type];
            if (importer != null)
            {
                return importer;
            }
            importer = StockImporters[type];
            if (importer == null)
            {
                importer = FindCompatibleImporter(type);
            }
            if (importer != null)
            {
                this.Register(importer);
                return importer;
            }
            return null;
        }

        public virtual T Import<T>(JsonReader reader)
        {
            return (T) this.Import(typeof(T), reader);
        }

        public virtual object Import(JsonReader reader)
        {
            return this.Import(AnyType.Value, reader);
        }

        public virtual object Import(Type type, JsonReader reader)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            IImporter importer = this.FindImporter(type);
            if (importer == null)
            {
                throw new JsonException(string.Format("Don't know how to import {0} from JSON.", type.FullName));
            }
            reader.MoveToContent();
            return importer.Import(this, reader);
        }

        public virtual void Register(IImporter importer)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }
            this.Importers.Put(importer);
        }

        private ImporterCollection Importers
        {
            get
            {
                if (this._importers == null)
                {
                    this._importers = new ImporterCollection();
                }
                return this._importers;
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

        private static ImporterCollection StockImporters
        {
            get
            {
                if (_stockImporters == null)
                {
                    ImporterCollection importers = new ImporterCollection();
                    importers.Add(new ByteImporter());
                    importers.Add(new Int16Importer());
                    importers.Add(new Int32Importer());
                    importers.Add(new Int64Importer());
                    importers.Add(new SingleImporter());
                    importers.Add(new DoubleImporter());
                    importers.Add(new DecimalImporter());
                    importers.Add(new StringImporter());
                    importers.Add(new BooleanImporter());
                    importers.Add(new DateTimeImporter());
                    importers.Add(new GuidImporter());
                    importers.Add(new UriImporter());
                    importers.Add(new ByteArrayImporter());
                    importers.Add(new AnyImporter());
                    importers.Add(new DictionaryImporter());
                    importers.Add(new ListImporter());
                    importers.Add(new NameValueCollectionImporter());
                    importers.Add(new BigIntegerImporter());
                    importers.Add(new ExpandoObjectImporter());
                    IList config = (IList) ConfigurationSettings.GetConfig("maticsoft/json.conversion.importers");
                    if ((config != null) && (config.Count > 0))
                    {
                        foreach (Type type in config)
                        {
                            importers.Put((IImporter) Activator.CreateInstance(type));
                        }
                    }
                    _stockImporters = importers;
                }
                return _stockImporters;
            }
        }
    }
}

