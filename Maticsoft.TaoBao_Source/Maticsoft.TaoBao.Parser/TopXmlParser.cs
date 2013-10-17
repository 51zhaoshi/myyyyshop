namespace Maticsoft.TaoBao.Parser
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml.Serialization;

    public class TopXmlParser : ITopParser
    {
        private static Dictionary<string, XmlSerializer> parsers = new Dictionary<string, XmlSerializer>();
        private static Regex regex = new Regex(@"<(\w+?)[ >]", RegexOptions.Compiled);

        private string GetRootElement(string body)
        {
            Match match = regex.Match(body);
            if (!match.Success)
            {
                throw new TopException("Invalid XML response format!");
            }
            return match.Groups[1].ToString();
        }

        public T Parse<T>(string body) where T: TopResponse
        {
            Type type = typeof(T);
            string rootElement = this.GetRootElement(body);
            string fullName = type.FullName;
            if ("error_response".Equals(rootElement))
            {
                fullName = fullName + "_error_response";
            }
            XmlSerializer serializer = null;
            if (!parsers.TryGetValue(fullName, out serializer) || (serializer == null))
            {
                XmlAttributes attributes = new XmlAttributes {
                    XmlRoot = new XmlRootAttribute(rootElement)
                };
                XmlAttributeOverrides overrides = new XmlAttributeOverrides();
                overrides.Add(type, attributes);
                serializer = new XmlSerializer(type, overrides);
                parsers[fullName] = serializer;
            }
            object obj2 = null;
            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(body)))
            {
                obj2 = serializer.Deserialize(stream);
            }
            T local = (T) obj2;
            if (local != null)
            {
                local.Body = body;
            }
            return local;
        }
    }
}

