namespace Maticsoft.OAuth.v2
{
    using System;
    using System.Collections.Specialized;
    using System.Threading.Tasks;

    public interface IOAuth2Operations
    {
        Task<AccessGrant> AuthenticateClientAsync();
        Task<AccessGrant> AuthenticateClientAsync(string scope);
        string BuildAuthenticateUrl(GrantType grantType, OAuth2Parameters parameters);
        string BuildAuthorizeUrl(GrantType grantType, OAuth2Parameters parameters);
        Task<AccessGrant> ExchangeCredentialsForAccessAsync(string username, string password, NameValueCollection additionalParameters);
        Task<AccessGrant> ExchangeForAccessAsync(string authorizationCode, string redirectUri, NameValueCollection additionalParameters);
        Task<AccessGrant> RefreshAccessAsync(string refreshToken, NameValueCollection additionalParameters);
        [Obsolete("Use the other RefreshAccessAsync method instead. Set the scope via additional parameters if needed.")]
        Task<AccessGrant> RefreshAccessAsync(string refreshToken, string scope, NameValueCollection additionalParameters);
    }
}

