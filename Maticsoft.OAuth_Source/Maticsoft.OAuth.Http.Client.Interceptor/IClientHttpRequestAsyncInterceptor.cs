namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using System;

    public interface IClientHttpRequestAsyncInterceptor : IClientHttpRequestInterceptor
    {
        void ExecuteAsync(IClientHttpRequestAsyncExecution execution);
    }
}

