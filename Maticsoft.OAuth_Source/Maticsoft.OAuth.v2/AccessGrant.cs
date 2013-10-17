namespace Maticsoft.OAuth.v2
{
    using System;

    [Serializable]
    public class AccessGrant
    {
        private string accessToken;
        private DateTime? expireTime;
        private string[] extraData;
        private string refreshToken;
        private string scope;

        public AccessGrant(string accessToken) : this(accessToken, null, null, null)
        {
        }

        public AccessGrant(AccessGrant accessGrant, string[] extraData) : this(accessGrant.AccessToken, accessGrant.Scope, accessGrant.RefreshToken, extraData, accessGrant.ExpireTime)
        {
        }

        public AccessGrant(string accessToken, string[] extraData) : this(accessToken, null, null, null, extraData)
        {
        }

        public AccessGrant(string accessToken, string scope, string refreshToken, int? expiresIn)
        {
            this.accessToken = accessToken;
            this.scope = scope;
            this.refreshToken = refreshToken;
            this.expireTime = expiresIn.HasValue ? new DateTime?(DateTime.UtcNow.AddSeconds((double) expiresIn.Value)) : null;
        }

        public AccessGrant(string accessToken, string scope, string refreshToken, string[] extraData, DateTime? expireTime)
        {
            this.accessToken = accessToken;
            this.scope = scope;
            this.refreshToken = refreshToken;
            this.expireTime = expireTime;
            this.extraData = extraData;
        }

        public AccessGrant(string accessToken, string scope, string refreshToken, int? expiresIn, string[] extraData)
        {
            this.accessToken = accessToken;
            this.scope = scope;
            this.refreshToken = refreshToken;
            this.expireTime = expiresIn.HasValue ? new DateTime?(DateTime.UtcNow.AddSeconds((double) expiresIn.Value)) : null;
            this.extraData = extraData;
        }

        public string AccessToken
        {
            get
            {
                return this.accessToken;
            }
        }

        public DateTime? ExpireTime
        {
            get
            {
                return this.expireTime;
            }
        }

        public string[] ExtraData
        {
            get
            {
                return this.extraData;
            }
        }

        public string RefreshToken
        {
            get
            {
                return this.refreshToken;
            }
        }

        public string Scope
        {
            get
            {
                return this.scope;
            }
        }
    }
}

