namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using System;

    public class HeadersResponseExtractor : IResponseExtractor<HttpHeaders>
    {
        public HttpHeaders ExtractData(IClientHttpResponse response)
        {
            return response.Headers;
        }
    }
}

