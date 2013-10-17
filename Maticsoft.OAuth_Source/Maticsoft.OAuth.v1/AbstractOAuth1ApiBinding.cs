namespace Maticsoft.OAuth.v1
{
    using Maticsoft.OAuth;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Rest.Client;
    using System;
    using System.Collections.Generic;

    public abstract class AbstractOAuth1ApiBinding : IApiBinding
    {
        private bool isAuthorized;
        private Maticsoft.OAuth.Rest.Client.RestTemplate restTemplate;

        protected AbstractOAuth1ApiBinding()
        {
            this.isAuthorized = false;
            this.restTemplate = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            ((WebClientHttpRequestFactory) this.restTemplate.RequestFactory).Expect100Continue = false;
            this.restTemplate.MessageConverters = this.GetMessageConverters();
            this.ConfigureRestTemplate(this.restTemplate);
        }

        protected AbstractOAuth1ApiBinding(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            this.isAuthorized = true;
            this.restTemplate = new Maticsoft.OAuth.Rest.Client.RestTemplate();
            ((WebClientHttpRequestFactory) this.restTemplate.RequestFactory).Expect100Continue = false;
            this.restTemplate.RequestInterceptors.Add(new OAuth1RequestInterceptor(consumerKey, consumerSecret, accessToken, accessTokenSecret));
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

        public bool IsAuthorized
        {
            get
            {
                return this.isAuthorized;
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

