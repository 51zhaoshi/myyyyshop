namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json.Collections;
    using System;
    using System.Reflection;

    [Serializable]
    internal sealed class ExporterCollection : KeyedCollection
    {
        public void Add(IExporter exporter)
        {
            if (exporter == null)
            {
                throw new ArgumentNullException("exporter");
            }
            base.Add(exporter);
        }

        protected override object KeyFromValue(object value)
        {
            return ((IExporter) value).InputType;
        }

        public void Put(IExporter exporter)
        {
            if (exporter == null)
            {
                throw new ArgumentNullException("exporter");
            }
            base.Remove(exporter.InputType);
            this.Add(exporter);
        }

        public IExporter this[Type type]
        {
            get
            {
                return (IExporter) base.GetByKey(type);
            }
        }
    }
}

