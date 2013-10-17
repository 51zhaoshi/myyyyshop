namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class MessageConverterResponseExtractor<T> : IResponseExtractor<T> where T: class
    {
        protected IList<IHttpMessageConverter> messageConverters;

        public MessageConverterResponseExtractor(IList<IHttpMessageConverter> messageConverters)
        {
            this.messageConverters = messageConverters;
        }

        public virtual T ExtractData(IClientHttpResponse response)
        {
            if (!this.HasMessageBody(response))
            {
                return default(T);
            }
            MediaType contentType = response.Headers.ContentType;
            if (contentType == null)
            {
                contentType = MediaType.APPLICATION_OCTET_STREAM;
            }
            foreach (IHttpMessageConverter converter in this.messageConverters)
            {
                if (converter.CanRead(typeof(T), contentType))
                {
                    return converter.Read<T>(response);
                }
            }
            throw new RestClientException(string.Format("Could not extract response: no suitable HttpMessageConverter found for response type [{0}] and content type [{1}]", typeof(T).FullName, contentType));
        }

        protected virtual bool HasMessageBody(IClientHttpResponse response)
        {
            return (((response.StatusCode != HttpStatusCode.NoContent) && (response.StatusCode != HttpStatusCode.NotModified)) && (response.Headers.ContentLength != 0L));
        }
    }
}

