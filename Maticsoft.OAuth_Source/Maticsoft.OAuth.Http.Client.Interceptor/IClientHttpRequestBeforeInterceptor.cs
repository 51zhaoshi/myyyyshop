namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using System;

    public interface IClientHttpRequestBeforeInterceptor : IClientHttpRequestInterceptor
    {
        void BeforeExecute(IClientHttpRequestContext request);
    }
}

