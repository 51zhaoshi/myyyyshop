namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http.Client;
    using System;

    public interface IClientHttpResponseAsyncContext
    {
        bool Cancelled { get; set; }

        Exception Error { get; set; }

        IClientHttpResponse Response { get; set; }

        object UserState { get; set; }
    }
}

