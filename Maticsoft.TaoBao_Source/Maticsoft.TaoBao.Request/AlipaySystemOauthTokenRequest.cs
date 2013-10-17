namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AlipaySystemOauthTokenRequest : ITopRequest<AlipaySystemOauthTokenResponse>
    {
        private IDictionary<string, string> otherParameters;

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }

        public string GetApiName()
        {
            return "alipay.system.oauth.token";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("code", this.Code);
            dictionary.Add("grant_type", this.GrantType);
            dictionary.Add("refresh_token", this.RefreshToken);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateMaxLength("code", this.Code, 40);
            RequestValidator.ValidateRequired("grant_type", this.GrantType);
            RequestValidator.ValidateMaxLength("grant_type", this.GrantType, 20);
            RequestValidator.ValidateMaxLength("refresh_token", this.RefreshToken, 40);
        }

        public string Code { get; set; }

        public string GrantType { get; set; }

        public string RefreshToken { get; set; }
    }
}

