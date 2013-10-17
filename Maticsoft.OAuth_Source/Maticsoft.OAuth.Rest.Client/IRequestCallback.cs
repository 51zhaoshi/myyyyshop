namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http.Client;
    using System;

    public interface IRequestCallback
    {
        void DoWithRequest(IClientHttpRequest request);
    }
}

