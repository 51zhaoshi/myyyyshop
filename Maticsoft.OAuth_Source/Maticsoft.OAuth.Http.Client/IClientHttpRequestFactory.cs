namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using System;

    public interface IClientHttpRequestFactory
    {
        IClientHttpRequest CreateRequest(Uri uri, HttpMethod method);
    }
}

