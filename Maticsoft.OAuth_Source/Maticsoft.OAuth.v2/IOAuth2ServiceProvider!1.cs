namespace Maticsoft.OAuth.v2
{
    using Maticsoft.OAuth;

    public interface IOAuth2ServiceProvider<T> : IServiceProvider<T> where T: IApiBinding
    {
        T GetApi(AccessGrant accessGrant);

        IOAuth2Operations OAuthOperations { get; }
    }
}

