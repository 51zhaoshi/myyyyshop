namespace Maticsoft.OAuth.v1
{
    using System;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    public interface IOAuth1Operations
    {
        string BuildAuthenticateUrl(string requestToken, OAuth1Parameters parameters);
        string BuildAuthorizeUrl(string requestToken, OAuth1Parameters parameters);
        Task<OAuthToken> ExchangeForAccessTokenAsync(AuthorizedRequestToken requestToken, NameValueCollection additionalParameters);
        Task<OAuthToken> FetchRequestTokenAsync(string callbackUrl, NameValueCollection additionalParameters);

        OAuth1Version Version { get; }
    }
}

