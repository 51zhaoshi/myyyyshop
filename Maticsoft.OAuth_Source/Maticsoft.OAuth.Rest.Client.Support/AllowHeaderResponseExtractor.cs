namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public class AllowHeaderResponseExtractor : IResponseExtractor<IList<HttpMethod>>
    {
        public IList<HttpMethod> ExtractData(IClientHttpResponse response)
        {
            return new List<HttpMethod>(response.Headers.Allow);
        }
    }
}

