namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public class HttpEntityRequestCallback : AcceptHeaderRequestCallback
    {
        protected HttpEntity requestEntity;

        public HttpEntityRequestCallback(object requestBody, IList<IHttpMessageConverter> messageConverters) : this(requestBody, null, messageConverters)
        {
        }

        public HttpEntityRequestCallback(object requestBody, Type responseType, IList<IHttpMessageConverter> messageConverters) : base(responseType, messageConverters)
        {
            if (requestBody is HttpEntity)
            {
                this.requestEntity = (HttpEntity) requestBody;
            }
            else
            {
                this.requestEntity = new HttpEntity(requestBody);
            }
        }

        public override void DoWithRequest(IClientHttpRequest request)
        {
            base.DoWithRequest(request);
            foreach (string str in this.requestEntity.Headers)
            {
                request.Headers[str] = this.requestEntity.Headers[str];
            }
            if (this.requestEntity.HasBody)
            {
                object body = this.requestEntity.Body;
                MediaType contentType = this.requestEntity.Headers.ContentType;
                foreach (IHttpMessageConverter converter in base.messageConverters)
                {
                    if (converter.CanWrite(body.GetType(), contentType))
                    {
                        converter.Write(body, contentType, request);
                        return;
                    }
                }
                string str2 = string.Format("Could not write request: no suitable IHttpMessageConverter found for request type [{0}]", body.GetType().FullName);
                if (contentType != null)
                {
                    str2 = string.Format("{0} and content type [{1}]", str2, contentType);
                }
                throw new RestClientException(str2);
            }
            if (request.Headers.ContentLength == -1L)
            {
                request.Headers.ContentLength = 0L;
            }
        }
    }
}

