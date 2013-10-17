namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using System;

    public interface IClientHttpRequestFactoryCreation
    {
        IClientHttpRequest Create();

        HttpMethod Method { get; set; }

        System.Uri Uri { get; set; }
    }
}

