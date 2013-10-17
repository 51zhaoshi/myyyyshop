namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.IO;
    using System.Text;

    public class StringHttpMessageConverter : AbstractHttpMessageConverter
    {
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);

        public StringHttpMessageConverter() : base(new MediaType[] { new MediaType("text", "plain", "utf-8"), MediaType.ALL })
        {
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            Encoding contentTypeCharset = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET);
            using (StreamReader reader = new StreamReader(message.Body, contentTypeCharset))
            {
                return (reader.ReadToEnd() as T);
            }
        }

        protected override bool Supports(Type type)
        {
            return type.Equals(typeof(string));
        }

        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            byte[] byteData = this.GetContentTypeCharset(message.Headers.ContentType, DEFAULT_CHARSET).GetBytes(content as string);
            message.Body = delegate (Stream stream) {
                stream.Write(byteData, 0, byteData.Length);
            };
        }
    }
}

