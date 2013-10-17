namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using System;

    public interface IClientHttpRequestAsyncExecution : IClientHttpRequestContext
    {
        void ExecuteAsync();
        void ExecuteAsync(Action<IClientHttpResponseAsyncContext> executeCompleted);

        object AsyncState { get; set; }
    }
}

