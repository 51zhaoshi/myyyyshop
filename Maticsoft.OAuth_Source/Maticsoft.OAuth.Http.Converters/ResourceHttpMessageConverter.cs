namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.IO;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ResourceHttpMessageConverter : AbstractHttpMessageConverter
    {
        private IDictionary<string, string> _mimeMapping;
        private static IDictionary<string, string> defaultMimeMapping = new Dictionary<string, string>(9, StringComparer.OrdinalIgnoreCase);

        static ResourceHttpMessageConverter()
        {
            defaultMimeMapping.Add(".bmp", "image/bmp");
            defaultMimeMapping.Add(".gif", "image/gif");
            defaultMimeMapping.Add(".jpg", "image/jpeg");
            defaultMimeMapping.Add(".jpeg", "image/jpeg");
            defaultMimeMapping.Add(".pdf", "application/pdf");
            defaultMimeMapping.Add(".png", "image/png");
            defaultMimeMapping.Add(".tif", "image/tiff");
            defaultMimeMapping.Add(".txt", "text/plain");
            defaultMimeMapping.Add(".zip", "application/x-zip-compressed");
        }

        public ResourceHttpMessageConverter() : base(new MediaType[] { MediaType.ALL })
        {
        }

        protected override MediaType GetDefaultContentType(object content)
        {
            Uri uri = ((IResource) content).Uri;
            if (uri != null)
            {
                string str2;
                string extension = Path.GetExtension(uri.ToString());
                IDictionary<string, string> dictionary = (this._mimeMapping == null) ? defaultMimeMapping : this._mimeMapping;
                if (dictionary.TryGetValue(extension, out str2))
                {
                    return MediaType.Parse(str2);
                }
            }
            return MediaType.APPLICATION_OCTET_STREAM;
        }

        protected override T ReadInternal<T>(IHttpInputMessage message) where T: class
        {
            using (MemoryStream stream = new MemoryStream())
            {
                IoUtils.CopyStream(message.Body, stream);
                return (new ByteArrayResource(stream.ToArray()) as T);
            }
        }

        protected override bool Supports(Type type)
        {
            return typeof(IResource).IsAssignableFrom(type);
        }

        protected override void WriteInternal(object content, IHttpOutputMessage message)
        {
            message.Body = delegate (Stream stream) {
                using (Stream stream2 = ((IResource) content).GetStream())
                {
                    IoUtils.CopyStream(stream2, stream);
                }
            };
        }

        public IDictionary<string, string> MimeMapping
        {
            get
            {
                if (this._mimeMapping == null)
                {
                    this._mimeMapping = new Dictionary<string, string>(defaultMimeMapping);
                }
                return this._mimeMapping;
            }
            set
            {
                this._mimeMapping = value;
            }
        }
    }
}

