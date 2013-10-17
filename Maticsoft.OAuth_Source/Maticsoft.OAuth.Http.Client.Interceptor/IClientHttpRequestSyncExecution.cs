namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http.Client;

    public interface IClientHttpRequestSyncExecution : IClientHttpRequestContext
    {
        IClientHttpResponse Execute();
    }
}

