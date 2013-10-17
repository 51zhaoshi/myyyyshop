namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using System;

    public interface IResponseErrorHandler
    {
        void HandleError(Uri requestUri, HttpMethod requestMethod, IClientHttpResponse response);
        bool HasError(Uri requestUri, HttpMethod requestMethod, IClientHttpResponse response);
    }
}

