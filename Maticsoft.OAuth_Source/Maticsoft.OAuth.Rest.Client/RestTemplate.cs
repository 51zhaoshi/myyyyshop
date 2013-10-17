namespace Maticsoft.OAuth.Rest.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Http.Client.Interceptor;
    using Maticsoft.OAuth.Http.Converters;
    using Maticsoft.OAuth.Http.Converters.Feed;
    using Maticsoft.OAuth.Http.Converters.Xml;
    using Maticsoft.OAuth.Rest.Client.Support;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class RestTemplate : IRestOperations
    {
        private Uri _baseAddress;
        private IResponseErrorHandler _errorHandler;
        private IList<IHttpMessageConverter> _messageConverters;
        private IClientHttpRequestFactory _requestFactory;
        private IList<IClientHttpRequestInterceptor> _requestInterceptors;

        public RestTemplate()
        {
            this._requestFactory = new WebClientHttpRequestFactory();
            this._errorHandler = new DefaultResponseErrorHandler();
            this._requestInterceptors = new List<IClientHttpRequestInterceptor>();
            this._messageConverters = new List<IHttpMessageConverter>();
            this._messageConverters.Add(new ByteArrayHttpMessageConverter());
            this._messageConverters.Add(new StringHttpMessageConverter());
            this._messageConverters.Add(new FormHttpMessageConverter());
            this._messageConverters.Add(new XmlDocumentHttpMessageConverter());
            this._messageConverters.Add(new XElementHttpMessageConverter());
            this._messageConverters.Add(new Rss20FeedHttpMessageConverter());
            this._messageConverters.Add(new Atom10FeedHttpMessageConverter());
        }

        public RestTemplate(string baseAddress) : this()
        {
            this.BaseAddress = new Uri(baseAddress, UriKind.Absolute);
        }

        public RestTemplate(Uri baseAddress) : this()
        {
            this.BaseAddress = baseAddress;
        }

        protected virtual Uri BuildUri(Uri baseAddress, Uri uri)
        {
            if (uri.IsAbsoluteUri)
            {
                return uri;
            }
            if (baseAddress == null)
            {
                throw new ArgumentException(string.Format("'{0}' is not an absolute URI", uri), "uri");
            }
            return new Uri(baseAddress, uri);
        }

        protected virtual Uri BuildUri(Uri baseAddress, string url, IDictionary<string, object> uriVariables)
        {
            Uri uri = new Maticsoft.OAuth.Util.UriTemplate(url).Expand(uriVariables);
            return this.BuildUri(baseAddress, uri);
        }

        protected virtual Uri BuildUri(Uri baseAddress, string url, params object[] uriVariables)
        {
            Uri uri = new Maticsoft.OAuth.Util.UriTemplate(url).Expand(uriVariables);
            return this.BuildUri(baseAddress, uri);
        }

        public void Delete(Uri url)
        {
            this.Execute<object>(url, HttpMethod.DELETE, null, null);
        }

        public void Delete(string url, IDictionary<string, object> uriVariables)
        {
            this.Execute<object>(url, HttpMethod.DELETE, null, null, uriVariables);
        }

        public void Delete(string url, params object[] uriVariables)
        {
            this.Execute<object>(url, HttpMethod.DELETE, null, null, uriVariables);
        }

        public Task DeleteAsync(Uri url)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, CancellationToken.None);
        }

        public Task DeleteAsync(string url, IDictionary<string, object> uriVariables)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, CancellationToken.None, uriVariables);
        }

        public Task DeleteAsync(string url, params object[] uriVariables)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler DeleteAsync(Uri url, Action<RestOperationCompletedEventArgs<object>> deleteCompleted)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, deleteCompleted);
        }

        public RestOperationCanceler DeleteAsync(string url, Action<RestOperationCompletedEventArgs<object>> deleteCompleted, params object[] uriVariables)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, deleteCompleted, uriVariables);
        }

        public RestOperationCanceler DeleteAsync(string url, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<object>> deleteCompleted)
        {
            return this.ExecuteAsync<object>(url, HttpMethod.DELETE, null, null, uriVariables, deleteCompleted);
        }

        protected virtual T DoExecute<T>(Uri uri, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor) where T: class
        {
            IClientHttpRequest request = this.GetClientHttpRequestFactory().CreateRequest(uri, method);
            if (requestCallback != null)
            {
                requestCallback.DoWithRequest(request);
            }
            using (IClientHttpResponse response = request.Execute())
            {
                if (response != null)
                {
                    if ((this._errorHandler != null) && this._errorHandler.HasError(uri, method, response))
                    {
                        HandleResponseError(uri, method, response, this._errorHandler);
                    }
                    else
                    {
                        LogResponseStatus(uri, method, response);
                    }
                    if (responseExtractor != null)
                    {
                        return responseExtractor.ExtractData(response);
                    }
                }
            }
            return default(T);
        }

        protected virtual RestOperationCanceler DoExecuteAsync<T>(Uri uri, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, Action<RestOperationCompletedEventArgs<T>> methodCompleted) where T: class
        {
            try
            {
                IClientHttpRequest request = this.GetClientHttpRequestFactory().CreateRequest(uri, method);
                RestAsyncOperationState<T> state = new RestAsyncOperationState<T>(uri, method, responseExtractor, this._errorHandler, methodCompleted);
                if (requestCallback != null)
                {
                    requestCallback.DoWithRequest(request);
                }
                request.ExecuteAsync(state, new Action<ClientHttpRequestCompletedEventArgs>(RestTemplate.ResponseReceivedCallback<T>));
                return new RestOperationCanceler(request);
            }
            catch (Exception exception)
            {
                if (methodCompleted != null)
                {
                    methodCompleted(new RestOperationCompletedEventArgs<T>(default(T), exception, false, null));
                }
                return new RestOperationCanceler(uri, method);
            }
        }

        protected virtual Task<T> DoExecuteAsync<T>(Uri uri, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, CancellationToken cancellationToken) where T: class
        {
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
            Task.Factory.StartNew(delegate {
                RestOperationCanceler canceler = this.DoExecuteAsync<T>(uri, method, requestCallback, responseExtractor, delegate (RestOperationCompletedEventArgs<T> args) {
                    if (args.Cancelled)
                    {
                        taskCompletionSource.TrySetCanceled();
                    }
                    else if (args.Error != null)
                    {
                        taskCompletionSource.TrySetException(args.Error);
                    }
                    else
                    {
                        taskCompletionSource.TrySetResult(args.Response);
                    }
                });
                cancellationToken.Register(delegate {
                    canceler.Cancel();
                });
            });
            return taskCompletionSource.Task;
        }

        public HttpResponseMessage Exchange(Uri url, HttpMethod method, HttpEntity requestEntity)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, method, requestCallback, responseExtractor);
        }

        public HttpResponseMessage<T> Exchange<T>(Uri url, HttpMethod method, HttpEntity requestEntity) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor);
        }

        public HttpResponseMessage Exchange(string url, HttpMethod method, HttpEntity requestEntity, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, method, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage<T> Exchange<T>(string url, HttpMethod method, HttpEntity requestEntity, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage<T> Exchange<T>(string url, HttpMethod method, HttpEntity requestEntity, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage Exchange(string url, HttpMethod method, HttpEntity requestEntity, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, method, requestCallback, responseExtractor, uriVariables);
        }

        public RestOperationCanceler ExchangeAsync<T>(Uri url, HttpMethod method, HttpEntity requestEntity, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> methodCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, methodCompleted);
        }

        public RestOperationCanceler ExchangeAsync(Uri url, HttpMethod method, HttpEntity requestEntity, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> methodCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, methodCompleted);
        }

        public Task<HttpResponseMessage> ExchangeAsync(Uri url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, cancellationToken);
        }

        public Task<HttpResponseMessage<T>> ExchangeAsync<T>(Uri url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, cancellationToken);
        }

        public RestOperationCanceler ExchangeAsync(string url, HttpMethod method, HttpEntity requestEntity, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> methodCompleted, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, methodCompleted, uriVariables);
        }

        public RestOperationCanceler ExchangeAsync<T>(string url, HttpMethod method, HttpEntity requestEntity, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> methodCompleted, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, methodCompleted, uriVariables);
        }

        public RestOperationCanceler ExchangeAsync<T>(string url, HttpMethod method, HttpEntity requestEntity, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> methodCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, uriVariables, methodCompleted);
        }

        public RestOperationCanceler ExchangeAsync(string url, HttpMethod method, HttpEntity requestEntity, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> methodCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, uriVariables, methodCompleted);
        }

        public Task<HttpResponseMessage<T>> ExchangeAsync<T>(string url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, cancellationToken, uriVariables);
        }

        public Task<HttpResponseMessage<T>> ExchangeAsync<T>(string url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, method, requestCallback, responseExtractor, cancellationToken, uriVariables);
        }

        public Task<HttpResponseMessage> ExchangeAsync(string url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, cancellationToken, uriVariables);
        }

        public Task<HttpResponseMessage> ExchangeAsync(string url, HttpMethod method, HttpEntity requestEntity, CancellationToken cancellationToken, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(requestEntity, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, method, requestCallback, responseExtractor, cancellationToken, uriVariables);
        }

        public T Execute<T>(Uri url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor) where T: class
        {
            return this.DoExecute<T>(this.BuildUri(this._baseAddress, url), method, requestCallback, responseExtractor);
        }

        public T Execute<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, IDictionary<string, object> uriVariables) where T: class
        {
            return this.DoExecute<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor);
        }

        public T Execute<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, params object[] uriVariables) where T: class
        {
            return this.DoExecute<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor);
        }

        public RestOperationCanceler ExecuteAsync<T>(Uri url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, Action<RestOperationCompletedEventArgs<T>> methodCompleted) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url), method, requestCallback, responseExtractor, methodCompleted);
        }

        public Task<T> ExecuteAsync<T>(Uri url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, CancellationToken cancellationToken) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url), method, requestCallback, responseExtractor, cancellationToken);
        }

        public RestOperationCanceler ExecuteAsync<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, Action<RestOperationCompletedEventArgs<T>> methodCompleted, params object[] uriVariables) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor, methodCompleted);
        }

        public RestOperationCanceler ExecuteAsync<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<T>> methodCompleted) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor, methodCompleted);
        }

        public Task<T> ExecuteAsync<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, CancellationToken cancellationToken, params object[] uriVariables) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor, cancellationToken);
        }

        public Task<T> ExecuteAsync<T>(string url, HttpMethod method, IRequestCallback requestCallback, IResponseExtractor<T> responseExtractor, CancellationToken cancellationToken, IDictionary<string, object> uriVariables) where T: class
        {
            return this.DoExecuteAsync<T>(this.BuildUri(this._baseAddress, url, uriVariables), method, requestCallback, responseExtractor, cancellationToken);
        }

        private IClientHttpRequestFactory GetClientHttpRequestFactory()
        {
            if ((this._requestInterceptors != null) && (this._requestInterceptors.Count > 0))
            {
                return new InterceptingClientHttpRequestFactory(this._requestFactory, this._requestInterceptors);
            }
            return this._requestFactory;
        }

        public HttpResponseMessage<T> GetForMessage<T>(Uri url) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor);
        }

        public HttpResponseMessage<T> GetForMessage<T>(string url, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage<T> GetForMessage<T>(string url, IDictionary<string, object> uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables);
        }

        public Task<HttpResponseMessage<T>> GetForMessageAsync<T>(Uri url) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<HttpResponseMessage<T>> GetForMessageAsync<T>(string url, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<HttpResponseMessage<T>> GetForMessageAsync<T>(string url, IDictionary<string, object> uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler GetForMessageAsync<T>(Uri url, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> getCompleted) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, getCompleted);
        }

        public RestOperationCanceler GetForMessageAsync<T>(string url, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> getCompleted, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, getCompleted, uriVariables);
        }

        public RestOperationCanceler GetForMessageAsync<T>(string url, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> getCompleted) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables, getCompleted);
        }

        public T GetForObject<T>(Uri url) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.GET, requestCallback, responseExtractor);
        }

        public T GetForObject<T>(string url, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables);
        }

        public T GetForObject<T>(string url, IDictionary<string, object> uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables);
        }

        public Task<T> GetForObjectAsync<T>(Uri url) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<T> GetForObjectAsync<T>(string url, IDictionary<string, object> uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<T> GetForObjectAsync<T>(string url, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler GetForObjectAsync<T>(Uri url, Action<RestOperationCompletedEventArgs<T>> getCompleted) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, getCompleted);
        }

        public RestOperationCanceler GetForObjectAsync<T>(string url, Action<RestOperationCompletedEventArgs<T>> getCompleted, params object[] uriVariables) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, getCompleted, uriVariables);
        }

        public RestOperationCanceler GetForObjectAsync<T>(string url, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<T>> getCompleted) where T: class
        {
            AcceptHeaderRequestCallback requestCallback = new AcceptHeaderRequestCallback(typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.GET, requestCallback, responseExtractor, uriVariables, getCompleted);
        }

        private static void HandleResponseError(Uri uri, HttpMethod method, IClientHttpResponse response, IResponseErrorHandler errorHandler)
        {
            errorHandler.HandleError(uri, method, response);
        }

        public HttpHeaders HeadForHeaders(Uri url)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.Execute<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor);
        }

        public HttpHeaders HeadForHeaders(string url, params object[] uriVariables)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.Execute<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, uriVariables);
        }

        public HttpHeaders HeadForHeaders(string url, IDictionary<string, object> uriVariables)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.Execute<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, uriVariables);
        }

        public Task<HttpHeaders> HeadForHeadersAsync(Uri url)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, CancellationToken.None);
        }

        public Task<HttpHeaders> HeadForHeadersAsync(string url, params object[] uriVariables)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<HttpHeaders> HeadForHeadersAsync(string url, IDictionary<string, object> uriVariables)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler HeadForHeadersAsync(Uri url, Action<RestOperationCompletedEventArgs<HttpHeaders>> headCompleted)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, headCompleted);
        }

        public RestOperationCanceler HeadForHeadersAsync(string url, Action<RestOperationCompletedEventArgs<HttpHeaders>> headCompleted, params object[] uriVariables)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, headCompleted, uriVariables);
        }

        public RestOperationCanceler HeadForHeadersAsync(string url, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpHeaders>> headCompleted)
        {
            HeadersResponseExtractor responseExtractor = new HeadersResponseExtractor();
            return this.ExecuteAsync<HttpHeaders>(url, HttpMethod.HEAD, null, responseExtractor, uriVariables, headCompleted);
        }

        private static void LogResponseStatus(Uri uri, HttpMethod method, IClientHttpResponse response)
        {
        }

        public IList<HttpMethod> OptionsForAllow(Uri url)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.Execute<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor);
        }

        public IList<HttpMethod> OptionsForAllow(string url, IDictionary<string, object> uriVariables)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.Execute<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, uriVariables);
        }

        public IList<HttpMethod> OptionsForAllow(string url, params object[] uriVariables)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.Execute<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, uriVariables);
        }

        public Task<IList<HttpMethod>> OptionsForAllowAsync(Uri url)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, CancellationToken.None);
        }

        public Task<IList<HttpMethod>> OptionsForAllowAsync(string url, IDictionary<string, object> uriVariables)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<IList<HttpMethod>> OptionsForAllowAsync(string url, params object[] uriVariables)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler OptionsForAllowAsync(Uri url, Action<RestOperationCompletedEventArgs<IList<HttpMethod>>> optionsCompleted)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, optionsCompleted);
        }

        public RestOperationCanceler OptionsForAllowAsync(string url, Action<RestOperationCompletedEventArgs<IList<HttpMethod>>> optionsCompleted, params object[] uriVariables)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, optionsCompleted, uriVariables);
        }

        public RestOperationCanceler OptionsForAllowAsync(string url, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<IList<HttpMethod>>> optionsCompleted)
        {
            AllowHeaderResponseExtractor responseExtractor = new AllowHeaderResponseExtractor();
            return this.ExecuteAsync<IList<HttpMethod>>(url, HttpMethod.OPTIONS, null, responseExtractor, uriVariables, optionsCompleted);
        }

        public Uri PostForLocation(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.Execute<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor);
        }

        public Uri PostForLocation(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.Execute<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public Uri PostForLocation(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.Execute<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public Task<Uri> PostForLocationAsync(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<Uri> PostForLocationAsync(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<Uri> PostForLocationAsync(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler PostForLocationAsync(Uri url, object request, Action<RestOperationCompletedEventArgs<Uri>> postCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted);
        }

        public RestOperationCanceler PostForLocationAsync(string url, object request, Action<RestOperationCompletedEventArgs<Uri>> postCompleted, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted, uriVariables);
        }

        public RestOperationCanceler PostForLocationAsync(string url, object request, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<Uri>> postCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            LocationHeaderResponseExtractor responseExtractor = new LocationHeaderResponseExtractor();
            return this.ExecuteAsync<Uri>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables, postCompleted);
        }

        public HttpResponseMessage PostForMessage(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor);
        }

        public HttpResponseMessage<T> PostForMessage<T>(Uri url, object request) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor);
        }

        public HttpResponseMessage PostForMessage(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage<T> PostForMessage<T>(string url, object request, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage PostForMessage(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.Execute<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public HttpResponseMessage<T> PostForMessage<T>(string url, object request, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.Execute<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public Task<HttpResponseMessage<T>> PostForMessageAsync<T>(Uri url, object request) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<HttpResponseMessage> PostForMessageAsync(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<HttpResponseMessage> PostForMessageAsync(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<HttpResponseMessage> PostForMessageAsync(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<HttpResponseMessage<T>> PostForMessageAsync<T>(string url, object request, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<HttpResponseMessage<T>> PostForMessageAsync<T>(string url, object request, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler PostForMessageAsync(Uri url, object request, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> postCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted);
        }

        public RestOperationCanceler PostForMessageAsync<T>(Uri url, object request, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> postCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted);
        }

        public RestOperationCanceler PostForMessageAsync(string url, object request, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> postCompleted, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted, uriVariables);
        }

        public RestOperationCanceler PostForMessageAsync<T>(string url, object request, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> postCompleted, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted, uriVariables);
        }

        public RestOperationCanceler PostForMessageAsync(string url, object request, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpResponseMessage>> postCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            HttpMessageResponseExtractor responseExtractor = new HttpMessageResponseExtractor();
            return this.ExecuteAsync<HttpResponseMessage>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables, postCompleted);
        }

        public RestOperationCanceler PostForMessageAsync<T>(string url, object request, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<HttpResponseMessage<T>>> postCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            HttpMessageResponseExtractor<T> responseExtractor = new HttpMessageResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<HttpResponseMessage<T>>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables, postCompleted);
        }

        public T PostForObject<T>(Uri url, object request) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.POST, requestCallback, responseExtractor);
        }

        public T PostForObject<T>(string url, object request, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public T PostForObject<T>(string url, object request, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.Execute<T>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables);
        }

        public Task<T> PostForObjectAsync<T>(Uri url, object request) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None);
        }

        public Task<T> PostForObjectAsync<T>(string url, object request, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public Task<T> PostForObjectAsync<T>(string url, object request, IDictionary<string, object> uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler PostForObjectAsync<T>(Uri url, object request, Action<RestOperationCompletedEventArgs<T>> postCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted);
        }

        public RestOperationCanceler PostForObjectAsync<T>(string url, object request, Action<RestOperationCompletedEventArgs<T>> postCompleted, params object[] uriVariables) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, postCompleted, uriVariables);
        }

        public RestOperationCanceler PostForObjectAsync<T>(string url, object request, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<T>> postCompleted) where T: class
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, typeof(T), this._messageConverters);
            MessageConverterResponseExtractor<T> responseExtractor = new MessageConverterResponseExtractor<T>(this._messageConverters);
            return this.ExecuteAsync<T>(url, HttpMethod.POST, requestCallback, responseExtractor, uriVariables, postCompleted);
        }

        public void Put(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            this.Execute<object>(url, HttpMethod.PUT, requestCallback, null);
        }

        public void Put(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            this.Execute<object>(url, HttpMethod.PUT, requestCallback, null, uriVariables);
        }

        public void Put(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            this.Execute<object>(url, HttpMethod.PUT, requestCallback, null, uriVariables);
        }

        public Task PutAsync(Uri url, object request)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, CancellationToken.None);
        }

        public Task PutAsync(string url, object request, IDictionary<string, object> uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, CancellationToken.None, uriVariables);
        }

        public Task PutAsync(string url, object request, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, CancellationToken.None, uriVariables);
        }

        public RestOperationCanceler PutAsync(Uri url, object request, Action<RestOperationCompletedEventArgs<object>> putCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, putCompleted);
        }

        public RestOperationCanceler PutAsync(string url, object request, Action<RestOperationCompletedEventArgs<object>> putCompleted, params object[] uriVariables)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, putCompleted, uriVariables);
        }

        public RestOperationCanceler PutAsync(string url, object request, IDictionary<string, object> uriVariables, Action<RestOperationCompletedEventArgs<object>> putCompleted)
        {
            HttpEntityRequestCallback requestCallback = new HttpEntityRequestCallback(request, this._messageConverters);
            return this.ExecuteAsync<object>(url, HttpMethod.PUT, requestCallback, null, uriVariables, putCompleted);
        }

        private static void ResponseReceivedCallback<T>(ClientHttpRequestCompletedEventArgs responseReceived) where T: class
        {
            RestAsyncOperationState<T> userState = (RestAsyncOperationState<T>) responseReceived.UserState;
            T local = default(T);
            Exception error = responseReceived.Error;
            bool cancelled = responseReceived.Cancelled;
            if ((error == null) && !cancelled)
            {
                using (IClientHttpResponse response = responseReceived.Response)
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((userState.ResponseErrorHandler != null) && userState.ResponseErrorHandler.HasError(userState.Uri, userState.Method, response))
                            {
                                HandleResponseError(userState.Uri, userState.Method, response, userState.ResponseErrorHandler);
                            }
                            else
                            {
                                LogResponseStatus(userState.Uri, userState.Method, response);
                            }
                            if (userState.ResponseExtractor != null)
                            {
                                local = userState.ResponseExtractor.ExtractData(response);
                            }
                        }
                        catch (Exception exception2)
                        {
                            error = exception2;
                        }
                    }
                }
            }
            if (userState.MethodCompleted != null)
            {
                userState.MethodCompleted(new RestOperationCompletedEventArgs<T>(local, error, cancelled, null));
            }
        }

        public Uri BaseAddress
        {
            get
            {
                return this._baseAddress;
            }
            set
            {
                ArgumentUtils.AssertNotNull(value, "BaseAddress");
                if (!value.IsAbsoluteUri)
                {
                    throw new ArgumentException(string.Format("'{0}' is not an absolute URI", value), "BaseAddress");
                }
                this._baseAddress = value;
            }
        }

        public IResponseErrorHandler ErrorHandler
        {
            get
            {
                return this._errorHandler;
            }
            set
            {
                this._errorHandler = value;
            }
        }

        public IList<IHttpMessageConverter> MessageConverters
        {
            get
            {
                return this._messageConverters;
            }
            set
            {
                this._messageConverters = value;
            }
        }

        public IClientHttpRequestFactory RequestFactory
        {
            get
            {
                return this._requestFactory;
            }
            set
            {
                this._requestFactory = value;
            }
        }

        public IList<IClientHttpRequestInterceptor> RequestInterceptors
        {
            get
            {
                return this._requestInterceptors;
            }
            set
            {
                this._requestInterceptors = value;
            }
        }
    }
}

