namespace Maticsoft.TaoBao.Parser
{
    using Jayrock.Json.Conversion;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml.Serialization;

    public class TopJsonParser : ITopParser
    {
        private static readonly Dictionary<string, Dictionary<string, TopAttribute>> attrs = new Dictionary<string, Dictionary<string, TopAttribute>>();

        public object FromJson(ITopReader reader, Type type)
        {
            object obj2 = null;
            Dictionary<string, TopAttribute>.Enumerator enumerator = this.GetTopAttributes(type).GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, TopAttribute> current = enumerator.Current;
                TopAttribute attribute = current.Value;
                string itemName = attribute.ItemName;
                string listName = attribute.ListName;
                if (reader.HasReturnField(itemName) || (!string.IsNullOrEmpty(listName) && reader.HasReturnField(listName)))
                {
                    object obj3 = null;
                    if (attribute.ListType != null)
                    {
                        obj3 = reader.GetListObjects(listName, itemName, attribute.ListType, new DTopConvert(this.FromJson));
                    }
                    else if (typeof(string) == attribute.ItemType)
                    {
                        object primitiveObject = reader.GetPrimitiveObject(itemName);
                        if (primitiveObject != null)
                        {
                            obj3 = primitiveObject.ToString();
                        }
                    }
                    else if (typeof(long) == attribute.ItemType)
                    {
                        object obj5 = reader.GetPrimitiveObject(itemName);
                        if (obj5 != null)
                        {
                            obj3 = ((IConvertible) obj5).ToInt64(null);
                        }
                    }
                    else if (typeof(bool) == attribute.ItemType)
                    {
                        obj3 = reader.GetPrimitiveObject(itemName);
                    }
                    else
                    {
                        obj3 = reader.GetReferenceObject(itemName, attribute.ItemType, new DTopConvert(this.FromJson));
                    }
                    if (obj3 != null)
                    {
                        if (obj2 == null)
                        {
                            obj2 = Activator.CreateInstance(type);
                        }
                        attribute.Method.Invoke(obj2, new object[] { obj3 });
                    }
                }
            }
            return obj2;
        }

        private Dictionary<string, TopAttribute> GetTopAttributes(Type type)
        {
            Dictionary<string, TopAttribute> dictionary = null;
            if (!attrs.TryGetValue(type.FullName, out dictionary) || (dictionary == null))
            {
                dictionary = new Dictionary<string, TopAttribute>();
                foreach (PropertyInfo info in type.GetProperties())
                {
                    TopAttribute attribute = new TopAttribute {
                        Method = info.GetSetMethod()
                    };
                    XmlElementAttribute[] customAttributes = info.GetCustomAttributes(typeof(XmlElementAttribute), true) as XmlElementAttribute[];
                    if ((customAttributes != null) && (customAttributes.Length > 0))
                    {
                        attribute.ItemName = customAttributes[0].ElementName;
                    }
                    if (attribute.ItemName == null)
                    {
                        XmlArrayItemAttribute[] attributeArray2 = info.GetCustomAttributes(typeof(XmlArrayItemAttribute), true) as XmlArrayItemAttribute[];
                        if ((attributeArray2 != null) && (attributeArray2.Length > 0))
                        {
                            attribute.ItemName = attributeArray2[0].ElementName;
                        }
                        XmlArrayAttribute[] attributeArray3 = info.GetCustomAttributes(typeof(XmlArrayAttribute), true) as XmlArrayAttribute[];
                        if ((attributeArray3 != null) && (attributeArray3.Length > 0))
                        {
                            attribute.ListName = attributeArray3[0].ElementName;
                        }
                        if (attribute.ListName == null)
                        {
                            goto Label_013C;
                        }
                    }
                    if (info.PropertyType.IsGenericType)
                    {
                        Type[] genericArguments = info.PropertyType.GetGenericArguments();
                        attribute.ListType = genericArguments[0];
                    }
                    else
                    {
                        attribute.ItemType = info.PropertyType;
                    }
                    dictionary.Add(info.Name, attribute);
                Label_013C:;
                }
                attrs[type.FullName] = dictionary;
            }
            return dictionary;
        }

        public T Parse<T>(string body) where T: TopResponse
        {
            T local = default(T);
            IDictionary dictionary = JsonConvert.Import(body) as IDictionary;
            if (dictionary != null)
            {
                IDictionary json = null;
                foreach (object obj2 in dictionary.Keys)
                {
                    json = dictionary[obj2] as IDictionary;
                    break;
                }
                if (json != null)
                {
                    ITopReader reader = new TopJsonReader(json);
                    local = (T) this.FromJson(reader, typeof(T));
                }
            }
            if (local == null)
            {
                local = Activator.CreateInstance<T>();
            }
            if (local != null)
            {
                local.Body = body;
            }
            return local;
        }
    }
}

