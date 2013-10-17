namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using System;
    using System.Net;

    public interface IClientHttpResponse : IHttpInputMessage, IDisposable
    {
        void Close();

        HttpStatusCode StatusCode { get; }

        string StatusDescription { get; }
    }
}

