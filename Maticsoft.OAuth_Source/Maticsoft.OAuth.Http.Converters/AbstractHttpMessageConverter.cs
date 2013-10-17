namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class AbstractHttpMessageConverter : IHttpMessageConverter
    {
        private IList<MediaType> _supportedMediaTypes;

        protected AbstractHttpMessageConverter()
        {
            this._supportedMediaTypes = new List<MediaType>();
        }

        protected AbstractHttpMessageConverter(params MediaType[] supportedMediaTypes)
        {
            this._supportedMediaTypes = new List<MediaType>();
            this._supportedMediaTypes = new List<MediaType>(supportedMediaTypes);
        }

        protected virtual bool CanRead(MediaType mediaType)
        {
            if (mediaType == null)
            {
                return true;
            }
            foreach (MediaType type in this._supportedMediaTypes)
            {
                if (type.Includes(mediaType))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool CanRead(Type type, MediaType mediaType)
        {
            return (this.Supports(type) && this.CanRead(mediaType));
        }

        protected virtual bool CanWrite(MediaType mediaType)
        {
            if ((mediaType == null) || (mediaType == MediaType.ALL))
            {
                return true;
            }
            foreach (MediaType type in this._supportedMediaTypes)
            {
                if (type.IsCompatibleWith(mediaType))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual bool CanWrite(Type type, MediaType mediaType)
        {
            return (this.Supports(type) && this.CanWrite(mediaType));
        }

        protected virtual Encoding GetContentTypeCharset(MediaType contentType, Encoding defaultEncoding)
        {
            if ((contentType != null) && (contentType.CharSet != null))
            {
                return contentType.CharSet;
            }
            return defaultEncoding;
        }

        protected virtual MediaType GetDefaultContentType(object content)
        {
            if (this._supportedMediaTypes.Count <= 0)
            {
                return null;
            }
            return this._supportedMediaTypes[0];
        }

        public virtual T Read<T>(IHttpInputMessage message) where T: class
        {
            return this.ReadInternal<T>(message);
        }

        protected abstract T ReadInternal<T>(IHttpInputMessage message) where T: class;
        protected abstract bool Supports(Type type);
        public virtual void Write(object content, MediaType contentType, IHttpOutputMessage message)
        {
            HttpHeaders headers = message.Headers;
            if (headers.ContentType == null)
            {
                if (((contentType == null) || contentType.IsWildcardType) || contentType.IsWildcardSubtype)
                {
                    contentType = this.GetDefaultContentType(content);
                }
                if (contentType != null)
                {
                    headers.ContentType = contentType;
                }
            }
            this.WriteInternal(content, message);
        }

        protected abstract void WriteInternal(object content, IHttpOutputMessage message);

        public virtual IList<MediaType> SupportedMediaTypes
        {
            get
            {
                return this._supportedMediaTypes;
            }
            set
            {
                this._supportedMediaTypes = value;
            }
        }
    }
}

