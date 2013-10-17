namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using System;

    public class HttpMessageResponseExtractor : IResponseExtractor<HttpResponseMessage>
    {
        public HttpResponseMessage ExtractData(IClientHttpResponse response)
        {
            return new HttpResponseMessage(response.Headers, response.StatusCode, response.StatusDescription);
        }
    }
}

