namespace Maticsoft.OAuth.Http.Converters.Json
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Json;
    using System;
    using System.IO;
    using System.Text;

    public class TextJsonHttpMessageConverter : AbstractHttpMessageConverter
    {
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);
        private Maticsoft.OAuth.Json.JsonMapper mapper;

        public TextJsonHttpMessageConverter() : this(new Maticsoft.OAuth.Json.JsonMapper())
        {
        }

        public TextJsonHttpMessageConverter(Maticsoft.OAuth.Json.JsonMapper mapper) : base(new MediaType[] { MediaType.ALL })
        {
            this.mapper = mapper;
        }

        public override bool CanRead(Type type, MediaType mediaType)
        {
            if (!base.CanRead(mediaType))
            {
                return false;
            }
            if (!typeof(JsonValue).IsAssignableFrom(type))
            {
                return this.mapper.CanDeserialize(type);
            }
            return true;
        }

        public override bool CanWrite(Type type, MediaType mediaType)
        {
            if (!base.CanWrite(mediaType))
            {
                return false;
            }
            if (!typeof(JsonValue).IsAssignableFrom(type))
            {
                return this.mapper.CanSerialize(type);
            }
            return true;
        }

        protected virtual string ConvertToJson(string result)
        {
            StringBuilder builder = new StringBuilder("{");
            foreach (string str in result.Split(new char[] { '&' }))
            {
                string[] strArray = str.Split(new char[] { '=' });
                if (strArray.Length == 2)
                {
                    builder.Append("\"");
                    builder.Append(strArray[0]);
                    builder.Append("\"");
                    builder.Append(":");
                    builder.Append("\"");
                    builder.Append(strArray[1]);
                    builder.Append("\"");
                    builder.Append(",");
                }
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append("}");
            return builder.ToString();
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            Encoding contentTypeCharset = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET);
            using (StreamReader reader = new StreamReader(message.Body, contentTypeCharset))
            {
                string result = reader.ReadToEnd();
                JsonValue value2 = JsonValue.Parse(this.ConvertToJson(result));
                if (typeof(T) == typeof(JsonValue))
                {
                    return (value2 as T);
                }
                return this.mapper.Deserialize<T>(value2);
            }
        }

        protected override bool Supports(Type type)
        {
            throw new InvalidOperationException();
        }

        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            byte[] byteData = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET).GetBytes(((content is JsonValue) ? ((JsonValue) content) : this.mapper.Serialize(content)).ToString());
            message.Body = delegate (Stream stream) {
                stream.Write(byteData, 0, byteData.Length);
            };
        }

        public Maticsoft.OAuth.Json.JsonMapper JsonMapper
        {
            get
            {
                return this.mapper;
            }
        }
    }
}

