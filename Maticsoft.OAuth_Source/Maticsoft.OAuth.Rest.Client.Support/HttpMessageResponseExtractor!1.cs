namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public class HttpMessageResponseExtractor<T> : IResponseExtractor<HttpResponseMessage<T>> where T: class
    {
        private MessageConverterResponseExtractor<T> httpMessageConverterExtractor;

        public HttpMessageResponseExtractor(IList<IHttpMessageConverter> messageConverters)
        {
            this.httpMessageConverterExtractor = new MessageConverterResponseExtractor<T>(messageConverters);
        }

        public HttpResponseMessage<T> ExtractData(IClientHttpResponse response)
        {
            return new HttpResponseMessage<T>(this.httpMessageConverterExtractor.ExtractData(response), response.Headers, response.StatusCode, response.StatusDescription);
        }
    }
}

