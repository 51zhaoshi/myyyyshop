namespace Maticsoft.OAuth.Http.Converters
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.IO;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Text;

    public class FormHttpMessageConverter : IHttpMessageConverter
    {
        private IList<IHttpMessageConverter> _partConverters;
        private IList<MediaType> _supportedMediaTypes = new List<MediaType>(2);
        private static char[] BOUNDARY_CHARS = new char[] { 
            '-', '_', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 
            'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 
            'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 
            'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
         };
        protected static readonly Encoding DEFAULT_CHARSET = new UTF8Encoding(false);
        private Random random = new Random();

        public FormHttpMessageConverter()
        {
            this._supportedMediaTypes.Add(MediaType.APPLICATION_FORM_URLENCODED);
            this._supportedMediaTypes.Add(MediaType.MULTIPART_FORM_DATA);
            this._partConverters = new List<IHttpMessageConverter>(3);
            this._partConverters.Add(new ByteArrayHttpMessageConverter());
            this._partConverters.Add(new StringHttpMessageConverter());
            this._partConverters.Add(new FileInfoHttpMessageConverter());
            this._partConverters.Add(new ResourceHttpMessageConverter());
        }

        public bool CanRead(Type type, MediaType mediaType)
        {
            if (typeof(NameValueCollection).IsAssignableFrom(type))
            {
                if (mediaType == null)
                {
                    return true;
                }
                foreach (MediaType type2 in this._supportedMediaTypes)
                {
                    if ((type2 != MediaType.MULTIPART_FORM_DATA) && type2.Includes(mediaType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanWrite(Type type, MediaType mediaType)
        {
            if (typeof(NameValueCollection).IsAssignableFrom(type) || typeof(IDictionary<string, object>).IsAssignableFrom(type))
            {
                if (mediaType == null)
                {
                    return true;
                }
                foreach (MediaType type2 in this._supportedMediaTypes)
                {
                    if (type2.IsCompatibleWith(mediaType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        protected virtual string GenerateMultipartBoundary()
        {
            char[] chArray = new char[this.random.Next(11) + 30];
            for (int i = 0; i < chArray.Length; i++)
            {
                chArray[i] = BOUNDARY_CHARS[this.random.Next(BOUNDARY_CHARS.Length)];
            }
            return new string(chArray);
        }

        private string GetContentDispositionFormData(string name, string filename)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("form-data; name=\"{0}\"", name);
            if (filename != null)
            {
                builder.AppendFormat("; filename=\"{0}\"", filename);
            }
            return builder.ToString();
        }

        private HttpEntity GetEntity(object part)
        {
            if (part is HttpEntity)
            {
                return (HttpEntity) part;
            }
            return new HttpEntity(part);
        }

        protected virtual string GetMultipartFilename(object part)
        {
            if (part is FileInfo)
            {
                return ((FileInfo) part).Name;
            }
            if (part is IResource)
            {
                Uri uri = ((IResource) part).Uri;
                if (uri != null)
                {
                    return Path.GetFileName(uri.ToString());
                }
            }
            return null;
        }

        public T Read<T>(IHttpInputMessage message) where T: class
        {
            string str;
            MediaType contentType = message.Headers.ContentType;
            Encoding encoding = ((contentType != null) && (contentType.CharSet != null)) ? contentType.CharSet : DEFAULT_CHARSET;
            using (StreamReader reader = new StreamReader(message.Body, encoding))
            {
                str = reader.ReadToEnd();
            }
            string[] strArray = str.Split(new char[] { '&' });
            NameValueCollection values = new NameValueCollection(strArray.Length);
            foreach (string str2 in strArray)
            {
                int index = str2.IndexOf('=');
                if (index == -1)
                {
                    values.Add(HttpUtils.FormDecode(str2), null);
                }
                else
                {
                    string name = HttpUtils.FormDecode(str2.Substring(0, index));
                    string str4 = HttpUtils.FormDecode(str2.Substring(index + 1));
                    values.Add(name, str4);
                }
            }
            return (values as T);
        }

        public void Write(object content, MediaType contentType, IHttpOutputMessage message)
        {
            if (content is NameValueCollection)
            {
                this.WriteForm((NameValueCollection) content, message);
            }
            else if (content is IDictionary<string, object>)
            {
                this.WriteMultipart((IDictionary<string, object>) content, message);
            }
        }

        private void WriteBoundary(string boundary, StreamWriter streamWriter)
        {
            streamWriter.Write("--");
            streamWriter.Write(boundary);
            streamWriter.WriteLine();
        }

        private void WriteEnd(string boundary, StreamWriter streamWriter)
        {
            streamWriter.Write("--");
            streamWriter.Write(boundary);
            streamWriter.Write("--");
            streamWriter.WriteLine();
        }

        private void WriteForm(NameValueCollection form, IHttpOutputMessage message)
        {
            message.Headers.ContentType = MediaType.APPLICATION_FORM_URLENCODED;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < form.AllKeys.Length; i++)
            {
                string name = form.AllKeys[i];
                string[] values = form.GetValues(name);
                if (values == null)
                {
                    builder.Append(HttpUtils.FormEncode(name));
                }
                else
                {
                    for (int j = 0; j < values.Length; j++)
                    {
                        string s = values[j];
                        builder.Append(HttpUtils.FormEncode(name));
                        builder.Append('=');
                        builder.Append(HttpUtils.FormEncode(s));
                        if (j != (values.Length - 1))
                        {
                            builder.Append('&');
                        }
                    }
                }
                if (i != (form.AllKeys.Length - 1))
                {
                    builder.Append('&');
                }
            }
            byte[] byteData = DEFAULT_CHARSET.GetBytes(builder.ToString());
            message.Headers.ContentLength = byteData.Length;
            message.Body = delegate (Stream stream) {
                stream.Write(byteData, 0, byteData.Length);
            };
        }

        private void WriteMultipart(IDictionary<string, object> parts, IHttpOutputMessage message)
        {
            string boundary = this.GenerateMultipartBoundary();
            IDictionary<string, string> parameters = new Dictionary<string, string>(1);
            parameters.Add("boundary", boundary);
            MediaType type = new MediaType(MediaType.MULTIPART_FORM_DATA, parameters);
            message.Headers.ContentType = type;
            message.Body = delegate (Stream stream) {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.NewLine = "\r\n";
                    this.WriteParts(boundary, parts, writer);
                    this.WriteEnd(boundary, writer);
                }
            };
        }

        private void WritePart(string name, HttpEntity partEntity, StreamWriter streamWriter)
        {
            object body = partEntity.Body;
            Type type = body.GetType();
            HttpHeaders headers = partEntity.Headers;
            MediaType contentType = headers.ContentType;
            foreach (IHttpMessageConverter converter in this._partConverters)
            {
                if (converter.CanWrite(type, contentType))
                {
                    IHttpOutputMessage message = new MultipartHttpOutputMessage(streamWriter);
                    message.Headers["Content-Disposition"] = this.GetContentDispositionFormData(name, this.GetMultipartFilename(body));
                    foreach (string str in headers)
                    {
                        message.Headers[str] = headers[str];
                    }
                    converter.Write(body, contentType, message);
                    return;
                }
            }
            throw new HttpMessageNotWritableException(string.Format("Could not write request: no suitable HttpMessageConverter found for part type [{0}]", type));
        }

        private void WriteParts(string boundary, IDictionary<string, object> parts, StreamWriter streamWriter)
        {
            foreach (KeyValuePair<string, object> pair in parts)
            {
                this.WriteBoundary(boundary, streamWriter);
                HttpEntity partEntity = this.GetEntity(pair.Value);
                this.WritePart(pair.Key, partEntity, streamWriter);
                streamWriter.WriteLine();
            }
        }

        public IList<IHttpMessageConverter> PartConverters
        {
            get
            {
                return this._partConverters;
            }
            set
            {
                this._partConverters = value;
            }
        }

        public IList<MediaType> SupportedMediaTypes
        {
            get
            {
                return this._supportedMediaTypes;
            }
        }

        private sealed class MultipartHttpOutputMessage : IHttpOutputMessage
        {
            private StreamWriter bodyWriter;
            private HttpHeaders headers = new HttpHeaders();

            public MultipartHttpOutputMessage(StreamWriter bodyWriter)
            {
                this.bodyWriter = bodyWriter;
            }

            private void WritePartBody(Action<Stream> body)
            {
                foreach (string str in this.headers)
                {
                    this.bodyWriter.Write(str);
                    this.bodyWriter.Write(": ");
                    this.bodyWriter.Write(this.headers[str]);
                    this.bodyWriter.WriteLine();
                }
                this.bodyWriter.WriteLine();
                this.bodyWriter.Flush();
                Stream baseStream = this.bodyWriter.BaseStream;
                baseStream.Flush();
                body(baseStream);
                baseStream.Flush();
            }

            public Action<Stream> Body
            {
                get
                {
                    throw new InvalidOperationException();
                }
                set
                {
                    this.WritePartBody(value);
                }
            }

            public HttpHeaders Headers
            {
                get
                {
                    return this.headers;
                }
            }
        }
    }
}

