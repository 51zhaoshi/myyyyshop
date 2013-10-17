namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http.Client;

    public interface IClientHttpRequestFactoryInterceptor : IClientHttpRequestInterceptor
    {
        IClientHttpRequest Create(IClientHttpRequestFactoryCreation creation);
    }
}

