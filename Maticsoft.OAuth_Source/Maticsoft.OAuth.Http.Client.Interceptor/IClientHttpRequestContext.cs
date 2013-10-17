namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http;
    using System;

    public interface IClientHttpRequestContext
    {
        Action<Stream> Body { get; set; }

        HttpHeaders Headers { get; }

        HttpMethod Method { get; }

        System.Uri Uri { get; }
    }
}

