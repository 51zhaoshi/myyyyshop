namespace Maticsoft.Json
{
    using System;
    using System.Collections;

    public sealed class DictionaryHelper
    {
        private static readonly DictionaryEntry[] _zeroEntries = new DictionaryEntry[0];

        private DictionaryHelper()
        {
            throw new NotSupportedException();
        }

        public static DictionaryEntry[] GetEntries(IDictionary dictionary)
        {
            if ((dictionary == null) || (dictionary.Count == 0))
            {
                return _zeroEntries;
            }
            DictionaryEntry[] entryArray = new DictionaryEntry[dictionary.Count];
            using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
            {
                int num = 0;
                while (enumerator.MoveNext())
                {
                    entryArray[num++] = enumerator.Entry;
                }
                return entryArray;
            }
        }
    }
}

