namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.IO;
    using System;
    using System.IO;

    [Obsolete("This class is obsolete; use ResourceHttpMessageConverter instead.")]
    public class FileInfoHttpMessageConverter : ResourceHttpMessageConverter
    {
        public override bool CanRead(Type type, MediaType mediaType)
        {
            return false;
        }

        public override bool CanWrite(Type type, MediaType mediaType)
        {
            return type.Equals(typeof(FileInfo));
        }

        public override T Read<T>(IHttpInputMessage message) where T: class
        {
            throw new NotSupportedException();
        }

        public override void Write(object content, MediaType contentType, IHttpOutputMessage message)
        {
            base.Write(new FileResource(((FileInfo) content).FullName), contentType, message);
        }
    }
}

