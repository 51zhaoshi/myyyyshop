namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Rest.Client;
    using System;

    public class RestAsyncOperationState<T> where T: class
    {
        private HttpMethod _method;
        private Action<RestOperationCompletedEventArgs<T>> _methodCompleted;
        private IResponseErrorHandler _responseErrorHandler;
        private IResponseExtractor<T> _responseExtractor;
        private System.Uri _uri;

        public RestAsyncOperationState(System.Uri uri, HttpMethod method, IResponseExtractor<T> responseExtractor, IResponseErrorHandler responseErrorHandler, Action<RestOperationCompletedEventArgs<T>> methodCompleted)
        {
            this._uri = uri;
            this._method = method;
            this._responseExtractor = responseExtractor;
            this._responseErrorHandler = responseErrorHandler;
            this._methodCompleted = methodCompleted;
        }

        public HttpMethod Method
        {
            get
            {
                return this._method;
            }
            set
            {
                this._method = value;
            }
        }

        public Action<RestOperationCompletedEventArgs<T>> MethodCompleted
        {
            get
            {
                return this._methodCompleted;
            }
            set
            {
                this._methodCompleted = value;
            }
        }

        public IResponseErrorHandler ResponseErrorHandler
        {
            get
            {
                return this._responseErrorHandler;
            }
            set
            {
                this._responseErrorHandler = value;
            }
        }

        public IResponseExtractor<T> ResponseExtractor
        {
            get
            {
                return this._responseExtractor;
            }
            set
            {
                this._responseExtractor = value;
            }
        }

        public System.Uri Uri
        {
            get
            {
                return this._uri;
            }
            set
            {
                this._uri = value;
            }
        }
    }
}

