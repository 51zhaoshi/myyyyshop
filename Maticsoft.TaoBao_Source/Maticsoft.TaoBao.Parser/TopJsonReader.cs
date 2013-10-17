namespace Maticsoft.TaoBao.Parser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class TopJsonReader : ITopReader
    {
        private IDictionary json;

        public TopJsonReader(IDictionary json)
        {
            this.json = json;
        }

        public IList GetListObjects(string listName, string itemName, Type type, DTopConvert convert)
        {
            IList list = null;
            IDictionary dictionary = this.json[listName] as IDictionary;
            if ((dictionary != null) && (dictionary.Count > 0))
            {
                IList list2 = dictionary[itemName] as IList;
                if ((list2 == null) || (list2.Count <= 0))
                {
                    return list;
                }
                list = Activator.CreateInstance(typeof(List<>).MakeGenericType(new Type[] { type })) as IList;
                foreach (object obj2 in list2)
                {
                    if (typeof(IDictionary).IsAssignableFrom(obj2.GetType()))
                    {
                        IDictionary json = obj2 as IDictionary;
                        object obj3 = convert(new TopJsonReader(json), type);
                        if (obj3 != null)
                        {
                            list.Add(obj3);
                        }
                    }
                    else if (!typeof(IList).IsAssignableFrom(obj2.GetType()))
                    {
                        list.Add(obj2);
                    }
                }
            }
            return list;
        }

        public object GetPrimitiveObject(object name)
        {
            return this.json[name];
        }

        public object GetReferenceObject(object name, Type type, DTopConvert convert)
        {
            IDictionary json = this.json[name] as IDictionary;
            if ((json != null) && (json.Count > 0))
            {
                return convert(new TopJsonReader(json), type);
            }
            return null;
        }

        public bool HasReturnField(object name)
        {
            return this.json.Contains(name);
        }
    }
}

