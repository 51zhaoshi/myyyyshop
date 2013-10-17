namespace Maticsoft.Json.Conversion
{
    using Maticsoft.Json.Collections;
    using System;
    using System.Reflection;

    [Serializable]
    internal sealed class ImporterCollection : KeyedCollection
    {
        public void Add(IImporter importer)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }
            base.Add(importer);
        }

        protected override object KeyFromValue(object value)
        {
            return ((IImporter) value).OutputType;
        }

        public void Put(IImporter importer)
        {
            if (importer == null)
            {
                throw new ArgumentNullException("importer");
            }
            base.Remove(importer.OutputType);
            this.Add(importer);
        }

        public IImporter this[Type type]
        {
            get
            {
                return (IImporter) base.GetByKey(type);
            }
        }
    }
}

