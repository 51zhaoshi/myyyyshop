namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using System;

    public class LocationHeaderResponseExtractor : IResponseExtractor<Uri>
    {
        public Uri ExtractData(IClientHttpResponse response)
        {
            return response.Headers.Location;
        }
    }
}

