namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Util;
    using System;
    using System.IO;

    public class ByteArrayHttpMessageConverter : AbstractHttpMessageConverter
    {
        public ByteArrayHttpMessageConverter() : base(new MediaType[] { new MediaType("application", "octet-stream"), MediaType.ALL })
        {
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            using (MemoryStream stream = new MemoryStream())
            {
                IoUtils.CopyStream(message.Body, stream);
                return (stream.ToArray() as T);
            }
        }

        protected override bool Supports(Type type)
        {
            return type.Equals(typeof(byte[]));
        }

        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            byte[] byteData = content as byte[];
            message.Body = delegate (Stream stream) {
                stream.Write(byteData, 0, byteData.Length);
            };
        }
    }
}

