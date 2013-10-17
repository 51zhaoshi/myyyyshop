namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections;
    using System.ComponentModel;

    public sealed class ComponentExporter : ExporterBase
    {
        private readonly IObjectMemberExporter[] _exporters;
        private readonly PropertyDescriptorCollection _properties;

        public ComponentExporter(Type inputType) : this(inputType, (ICustomTypeDescriptor) null)
        {
        }

        public ComponentExporter(Type inputType, ICustomTypeDescriptor typeDescriptor) : this(inputType, (typeDescriptor != null) ? typeDescriptor.GetProperties() : new Maticsoft.Json.Conversion.CustomTypeDescriptor(inputType).GetProperties())
        {
        }

        private ComponentExporter(Type inputType, PropertyDescriptorCollection properties) : base(inputType)
        {
            this._properties = properties;
            int num = 0;
            IObjectMemberExporter[] exporterArray = new IObjectMemberExporter[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                IServiceProvider provider = properties[i] as IServiceProvider;
                if (provider != null)
                {
                    IObjectMemberExporter service = (IObjectMemberExporter) provider.GetService(typeof(IObjectMemberExporter));
                    if (service != null)
                    {
                        exporterArray[i] = service;
                        num++;
                    }
                }
            }
            if (num > 0)
            {
                this._exporters = exporterArray;
            }
        }

        protected override void ExportValue(ExportContext context, object value, JsonWriter writer)
        {
            if (this._properties.Count == 0)
            {
                writer.WriteString(value.ToString());
            }
            else
            {
                ObjectReferenceTracker tracker = null;
                try
                {
                    writer.WriteStartObject();
                    int num = 0;
                    foreach (PropertyDescriptor descriptor in this._properties)
                    {
                        IObjectMemberExporter exporter = ((this._exporters != null) && (num < this._exporters.Length)) ? this._exporters[num++] : null;
                        if (exporter != null)
                        {
                            exporter.Export(context, writer, value);
                        }
                        else
                        {
                            object o = descriptor.GetValue(value);
                            if (!JsonNull.LogicallyEquals(o))
                            {
                                writer.WriteMember(descriptor.Name);
                                if (value.GetType().IsClass && (tracker == null))
                                {
                                    tracker = TrackObject(context, value);
                                }
                                context.Export(o, writer);
                            }
                        }
                    }
                    writer.WriteEndObject();
                }
                finally
                {
                    if (tracker != null)
                    {
                        tracker.Pop(value);
                    }
                }
            }
        }

        private static ObjectReferenceTracker TrackObject(ExportContext context, object value)
        {
            Type key = typeof(ComponentExporter);
            ObjectReferenceTracker tracker = (ObjectReferenceTracker) context.Items[key];
            if (tracker == null)
            {
                tracker = new ObjectReferenceTracker();
                context.Items.Add(key, tracker);
            }
            tracker.PushNew(value);
            return tracker;
        }

        private sealed class ObjectReferenceTracker
        {
            private readonly ArrayList _stack = new ArrayList(6);

            public void Pop(object value)
            {
                int index = this._stack.Count - 1;
                this._stack.RemoveAt(index);
            }

            public void PushNew(object value)
            {
                for (int i = this._stack.Count - 1; i >= 0; i--)
                {
                    if (object.ReferenceEquals(this._stack[i], value))
                    {
                        throw new JsonException(string.Format("{0} does not support export of an object graph containing circular references. A value of type {1} has already been exported.", typeof(ComponentExporter).FullName, value.GetType().FullName));
                    }
                }
                this._stack.Add(value);
            }
        }
    }
}

