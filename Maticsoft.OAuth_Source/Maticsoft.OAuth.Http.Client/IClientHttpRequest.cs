namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using System;

    public interface IClientHttpRequest : IHttpOutputMessage
    {
        void CancelAsync();
        IClientHttpResponse Execute();
        void ExecuteAsync(object state, Action<ClientHttpRequestCompletedEventArgs> executeCompleted);

        HttpMethod Method { get; }

        System.Uri Uri { get; }
    }
}

