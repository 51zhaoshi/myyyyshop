namespace Maticsoft.Json.Conversion.Converters
{
    using System;

    internal class CollectionImporter<TOutput, TItem> : CollectionImporter<List<TItem>, TOutput, TItem> where TOutput: IEnumerable
    {
        public CollectionImporter()
        {
        }

        public CollectionImporter(bool isOututReadOnly) : base(isOututReadOnly)
        {
        }
    }
}

