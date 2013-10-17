namespace Maticsoft.OAuth.Http.Converters.Json
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Converters;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Xml;

    public class DataContractJsonHttpMessageConverter : AbstractHttpMessageConverter
    {
        private IEnumerable<Type> _knownTypes;
        private bool _requiresAttribute;
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);

        public DataContractJsonHttpMessageConverter() : base(new MediaType[] { new MediaType("application", "json") })
        {
        }

        public DataContractJsonHttpMessageConverter(bool requiresAttribute) : base(new MediaType[] { new MediaType("application", "json") })
        {
            this._requiresAttribute = requiresAttribute;
        }

        protected virtual DataContractJsonSerializer GetSerializer(Type type)
        {
            if (this._knownTypes == null)
            {
                return new DataContractJsonSerializer(type);
            }
            return new DataContractJsonSerializer(type, this._knownTypes);
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            return (T) this.GetSerializer(typeof(T)).ReadObject(message.Body);
        }

        protected override bool Supports(Type type)
        {
            if (this._requiresAttribute && (System.Attribute.GetCustomAttributes(type, typeof(DataContractAttribute), true).Length <= 0))
            {
                return (System.Attribute.GetCustomAttributes(type, typeof(CollectionDataContractAttribute), true).Length > 0);
            }
            return true;
        }

        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            Encoding encoding = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET);
            DataContractJsonSerializer serializer = this.GetSerializer(content.GetType());
            message.Body = delegate (Stream stream) {
                using (XmlDictionaryWriter writer = JsonReaderWriterFactory.CreateJsonWriter(stream, encoding, false))
                {
                    serializer.WriteObject(writer, content);
                }
            };
        }

        public IEnumerable<Type> KnownTypes
        {
            get
            {
                return this._knownTypes;
            }
            set
            {
                this._knownTypes = value;
            }
        }

        public bool RequiresAttribute
        {
            get
            {
                return this._requiresAttribute;
            }
        }
    }
}

