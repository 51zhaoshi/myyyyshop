namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http.Client;

    public interface IClientHttpRequestSyncInterceptor : IClientHttpRequestInterceptor
    {
        IClientHttpResponse Execute(IClientHttpRequestSyncExecution execution);
    }
}

