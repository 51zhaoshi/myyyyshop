namespace Maticsoft.Json.Conversion.Converters
{
    using Maticsoft.Json;
    using Maticsoft.Json.Conversion;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    internal class CollectionImporter<TCollection, TOutput, TItem> : ImporterBase where TCollection: ICollection<TItem>, new() where TOutput: IEnumerable
    {
        private readonly bool _isOutputReadOnly;

        public CollectionImporter() : this(false)
        {
        }

        public CollectionImporter(bool isOutputReadOnly) : base(typeof(TOutput))
        {
            this._isOutputReadOnly = isOutputReadOnly;
        }

        protected override object ImportFromArray(ImportContext context, JsonReader reader)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            reader.Read();
            TCollection local = (default(TCollection) == null) ? Activator.CreateInstance<TCollection>() : default(TCollection);
            while (reader.TokenClass != JsonTokenClass.EndArray)
            {
                local.Add(context.Import<TItem>(reader));
            }
            object result = this.IsOutputReadOnly ? ((object) new ReadOnlyCollection<TItem>((IList<TItem>) local)) : ((object) local);
            return ImporterBase.ReadReturning(reader, result);
        }

        public bool IsOutputReadOnly
        {
            get
            {
                return this._isOutputReadOnly;
            }
        }
    }
}

