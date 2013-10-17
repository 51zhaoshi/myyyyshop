namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth;
    using System;

    public interface IOAuth1ServiceProvider<T> : IServiceProvider<T> where T: IApiBinding
    {
        T GetApi(string accessToken, string secret);

        IOAuth1Operations OAuthOperations { get; }
    }
}

