namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;

    public class InterceptingClientHttpRequestFactory : IClientHttpRequestFactory
    {
        private IEnumerable<IClientHttpRequestInterceptor> interceptors;
        private IClientHttpRequestFactory targetRequestFactory;

        public InterceptingClientHttpRequestFactory(IClientHttpRequestFactory requestFactory, IEnumerable<IClientHttpRequestInterceptor> interceptors)
        {
            ArgumentUtils.AssertNotNull(requestFactory, "requestFactory");
            this.targetRequestFactory = requestFactory;
            this.interceptors = (interceptors != null) ? interceptors : ((IEnumerable<IClientHttpRequestInterceptor>) new IClientHttpRequestInterceptor[0]);
        }

        public IClientHttpRequest CreateRequest(Uri uri, HttpMethod method)
        {
            RequestCreation creation = new RequestCreation(uri, method, this.targetRequestFactory, this.interceptors);
            return new InterceptingClientHttpRequest(creation.Create(), this.interceptors);
        }

        public IClientHttpRequestFactory TargetRequestFactory
        {
            get
            {
                return this.targetRequestFactory;
            }
        }

        private sealed class RequestCreation : IClientHttpRequestFactoryCreation
        {
            private IClientHttpRequestFactory delegateRequestFactory;
            private IEnumerator<IClientHttpRequestInterceptor> enumerator;
            private HttpMethod method;
            private System.Uri uri;

            public RequestCreation(System.Uri uri, HttpMethod method, IClientHttpRequestFactory delegateRequestFactory, IEnumerable<IClientHttpRequestInterceptor> interceptors)
            {
                this.uri = uri;
                this.method = method;
                this.delegateRequestFactory = delegateRequestFactory;
                this.enumerator = interceptors.GetEnumerator();
            }

            public IClientHttpRequest Create()
            {
                if (!this.enumerator.MoveNext())
                {
                    return this.delegateRequestFactory.CreateRequest(this.uri, this.method);
                }
                if (this.enumerator.Current is IClientHttpRequestFactoryInterceptor)
                {
                    return ((IClientHttpRequestFactoryInterceptor) this.enumerator.Current).Create(this);
                }
                return this.Create();
            }

            public HttpMethod Method
            {
                get
                {
                    return this.method;
                }
                set
                {
                    this.method = value;
                }
            }

            public System.Uri Uri
            {
                get
                {
                    return this.uri;
                }
                set
                {
                    this.uri = value;
                }
            }
        }
    }
}

