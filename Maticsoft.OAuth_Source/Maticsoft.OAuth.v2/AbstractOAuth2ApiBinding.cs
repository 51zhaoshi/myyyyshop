namespace Maticsoft.OAuth.v2
{
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public abstract class AbstractOAuth2ApiBinding : IApiBinding
    {
        private string accessToken;
        private Maticsoft.OAuth.Rest.Client.RestTemplate restTemplate;

        protected AbstractOAuth2ApiBinding()
        {
            this.accessToken = null;
            this.restTemplate = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            ((WebClientHttpRequestFactory) this.restTemplate.RequestFactory).Expect100Continue = false;
            this.restTemplate.MessageConverters = this.GetMessageConverters();
            this.ConfigureRestTemplate(this.restTemplate);
        }

        protected AbstractOAuth2ApiBinding(AccessGrant accessGrant)
        {
            this.accessToken = accessGrant.AccessToken;
            this.restTemplate = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            ((WebClientHttpRequestFactory) this.restTemplate.RequestFactory).Expect100Continue = false;
            this.restTemplate.RequestInterceptors.Add(new OAuth2RequestInterceptor(accessGrant.AccessToken, this.GetOAuth2Version()));
            this.restTemplate.MessageConverters = this.GetMessageConverters();
            this.ConfigureRestTemplate(this.restTemplate);
        }

        protected virtual void ConfigureRestTemplate(Maticsoft.OAuth.Rest.Client.RestTemplate restTemplate)
        {
        }

        protected virtual IList<IHttpMessageConverter> GetMessageConverters()
        {
            return new List<IHttpMessageConverter> { new StringHttpMessageConverter(), new FormHttpMessageConverter() };
        }

        protected virtual OAuth2Version GetOAuth2Version()
        {
            return OAuth2Version.Bearer;
        }

        public bool IsAuthorized
        {
            get
            {
                return (this.accessToken != null);
            }
        }

        public Maticsoft.OAuth.Rest.Client.RestTemplate RestTemplate
        {
            get
            {
                return this.restTemplate;
            }
        }
    }
}

