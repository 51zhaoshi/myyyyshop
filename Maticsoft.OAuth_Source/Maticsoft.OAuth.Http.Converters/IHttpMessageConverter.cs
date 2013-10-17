namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Collections.Generic;

    public interface IHttpMessageConverter
    {
        bool CanRead(Type type, MediaType mediaType);
        bool CanWrite(Type type, MediaType mediaType);
        T Read<T>(IHttpInputMessage message) where T: class;
        void Write(object content, MediaType contentType, IHttpOutputMessage message);

        IList<MediaType> SupportedMediaTypes { get; }
    }
}

