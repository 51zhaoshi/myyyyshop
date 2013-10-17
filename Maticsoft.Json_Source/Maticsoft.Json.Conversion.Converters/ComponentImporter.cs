namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.ComponentModel;

    public sealed class ComponentImporter : ImporterBase
    {
        private readonly IObjectMemberImporter[] _importers;
        private readonly PropertyDescriptorCollection _properties;

        public ComponentImporter(Type type) : this(type, null)
        {
        }

        public ComponentImporter(Type type, ICustomTypeDescriptor typeDescriptor) : base(type)
        {
            if (typeDescriptor == null)
            {
                typeDescriptor = new Maticsoft.Json.Conversion.CustomTypeDescriptor(type);
            }
            int num = 0;
            PropertyDescriptorCollection properties = typeDescriptor.GetProperties();
            IObjectMemberImporter[] importerArray = new IObjectMemberImporter[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                IServiceProvider provider = properties[i] as IServiceProvider;
                if (provider != null)
                {
                    IObjectMemberImporter service = (IObjectMemberImporter) provider.GetService(typeof(IObjectMemberImporter));
                    if (service != null)
                    {
                        importerArray[i++] = service;
                        num++;
                    }
                }
            }
            this._properties = properties;
            if (num > 0)
            {
                this._importers = importerArray;
            }
        }

        protected override object ImportFromObject(ImportContext context, JsonReader reader)
        {
            reader.Read();
            object target = Activator.CreateInstance(base.OutputType);
            INonObjectMemberImporter importer = target as INonObjectMemberImporter;
            while (reader.TokenClass != JsonTokenClass.EndObject)
            {
                string name = reader.ReadMember();
                PropertyDescriptor descriptor = this._properties.Find(name, true);
                if ((descriptor == null) || descriptor.IsReadOnly)
                {
                    if ((importer == null) || !importer.Import(context, name, reader))
                    {
                        reader.Skip();
                    }
                }
                else
                {
                    if (this._importers != null)
                    {
                        int index = this._properties.IndexOf(descriptor);
                        IObjectMemberImporter importer2 = this._importers[index];
                        if (importer2 != null)
                        {
                            importer2.Import(context, reader, target);
                            continue;
                        }
                    }
                    descriptor.SetValue(target, context.Import(descriptor.PropertyType, reader));
                }
            }
            return ImporterBase.ReadReturning(reader, target);
        }
    }
}

