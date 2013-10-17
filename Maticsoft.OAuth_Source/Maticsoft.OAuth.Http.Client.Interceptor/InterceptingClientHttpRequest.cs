namespace Maticsoft.OAuth.Http.Client.Interceptor
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Util;
    using System;
    using System.Collections.Generic;

    public class InterceptingClientHttpRequest : IClientHttpRequest, IHttpOutputMessage
    {
        private Action<Stream> body;
        private IEnumerable<IClientHttpRequestInterceptor> interceptors;
        private IClientHttpRequest targetRequest;

        public InterceptingClientHttpRequest(IClientHttpRequest request, IEnumerable<IClientHttpRequestInterceptor> interceptors)
        {
            ArgumentUtils.AssertNotNull(request, "request");
            this.targetRequest = request;
            this.interceptors = (interceptors != null) ? interceptors : ((IEnumerable<IClientHttpRequestInterceptor>) new IClientHttpRequestInterceptor[0]);
        }

        public void CancelAsync()
        {
            this.targetRequest.CancelAsync();
        }

        public IClientHttpResponse Execute()
        {
            RequestSyncExecution execution = new RequestSyncExecution(this.targetRequest, this.body, this.interceptors);
            return execution.Execute();
        }

        public void ExecuteAsync(object state, Action<ClientHttpRequestCompletedEventArgs> executeCompleted)
        {
            new RequestAsyncExecution(this.targetRequest, this.body, this.interceptors, state, executeCompleted).ExecuteAsync();
        }

        public Action<Stream> Body
        {
            get
            {
                return this.body;
            }
            set
            {
                this.body = value;
                this.targetRequest.Body = value;
            }
        }

        public HttpHeaders Headers
        {
            get
            {
                return this.targetRequest.Headers;
            }
        }

        public HttpMethod Method
        {
            get
            {
                return this.targetRequest.Method;
            }
        }

        public IClientHttpRequest TargetRequest
        {
            get
            {
                return this.targetRequest;
            }
        }

        public System.Uri Uri
        {
            get
            {
                return this.targetRequest.Uri;
            }
        }

        private abstract class AbstractRequestContext : IClientHttpRequestContext
        {
            protected Action<Stream> body;
            protected IClientHttpRequest delegateRequest;
            protected IEnumerator<IClientHttpRequestInterceptor> enumerator;

            protected AbstractRequestContext(IClientHttpRequest delegateRequest, Action<Stream> body, IEnumerable<IClientHttpRequestInterceptor> interceptors)
            {
                this.delegateRequest = delegateRequest;
                this.body = body;
                this.enumerator = interceptors.GetEnumerator();
            }

            public Action<Stream> Body
            {
                get
                {
                    return this.body;
                }
                set
                {
                    this.delegateRequest.Body = value;
                }
            }

            public HttpHeaders Headers
            {
                get
                {
                    return this.delegateRequest.Headers;
                }
            }

            public HttpMethod Method
            {
                get
                {
                    return this.delegateRequest.Method;
                }
            }

            public System.Uri Uri
            {
                get
                {
                    return this.delegateRequest.Uri;
                }
            }
        }

        private sealed class RequestAsyncExecution : InterceptingClientHttpRequest.AbstractRequestContext, IClientHttpRequestAsyncExecution, IClientHttpRequestContext
        {
            private object asyncState;
            private IList<Action<IClientHttpResponseAsyncContext>> executeCompletedDelegates;
            private Action<ClientHttpRequestCompletedEventArgs> interceptedExecuteCompleted;

            public RequestAsyncExecution(IClientHttpRequest delegateRequest, Action<Stream> body, IEnumerable<IClientHttpRequestInterceptor> interceptors, object asyncState, Action<ClientHttpRequestCompletedEventArgs> executeCompleted) : base(delegateRequest, body, interceptors)
            {
                this.asyncState = asyncState;
                this.interceptedExecuteCompleted = executeCompleted;
                this.executeCompletedDelegates = new List<Action<IClientHttpResponseAsyncContext>>();
            }

            public void ExecuteAsync()
            {
                this.ExecuteAsync(null);
            }

            public void ExecuteAsync(Action<IClientHttpResponseAsyncContext> executeCompleted)
            {
                Action<ClientHttpRequestCompletedEventArgs> action = null;
                if (executeCompleted != null)
                {
                    this.executeCompletedDelegates.Insert(0, executeCompleted);
                }
                if (base.enumerator.MoveNext())
                {
                    if (base.enumerator.Current is IClientHttpRequestAsyncInterceptor)
                    {
                        ((IClientHttpRequestAsyncInterceptor) base.enumerator.Current).ExecuteAsync(this);
                    }
                    else
                    {
                        if (base.enumerator.Current is IClientHttpRequestBeforeInterceptor)
                        {
                            ((IClientHttpRequestBeforeInterceptor) base.enumerator.Current).BeforeExecute(this);
                        }
                        this.ExecuteAsync(null);
                    }
                }
                else
                {
                    if (action == null)
                    {
                        action = delegate (ClientHttpRequestCompletedEventArgs args) {
                            InterceptingClientHttpRequest.ResponseAsyncContext context = new InterceptingClientHttpRequest.ResponseAsyncContext(args);
                            foreach (Action<IClientHttpResponseAsyncContext> action in this.executeCompletedDelegates)
                            {
                                action(context);
                            }
                            if (this.interceptedExecuteCompleted != null)
                            {
                                this.interceptedExecuteCompleted(context.GetCompletedEventArgs());
                            }
                        };
                    }
                    base.delegateRequest.ExecuteAsync(this.asyncState, action);
                }
            }

            public object AsyncState
            {
                get
                {
                    return this.asyncState;
                }
                set
                {
                    this.asyncState = value;
                }
            }
        }

        private sealed class RequestSyncExecution : InterceptingClientHttpRequest.AbstractRequestContext, IClientHttpRequestSyncExecution, IClientHttpRequestContext
        {
            public RequestSyncExecution(IClientHttpRequest delegateRequest, Action<Stream> body, IEnumerable<IClientHttpRequestInterceptor> interceptors) : base(delegateRequest, body, interceptors)
            {
            }

            public IClientHttpResponse Execute()
            {
                if (!base.enumerator.MoveNext())
                {
                    return base.delegateRequest.Execute();
                }
                if (base.enumerator.Current is IClientHttpRequestSyncInterceptor)
                {
                    return ((IClientHttpRequestSyncInterceptor) base.enumerator.Current).Execute(this);
                }
                if (base.enumerator.Current is IClientHttpRequestBeforeInterceptor)
                {
                    ((IClientHttpRequestBeforeInterceptor) base.enumerator.Current).BeforeExecute(this);
                }
                return this.Execute();
            }
        }

        private sealed class ResponseAsyncContext : IClientHttpResponseAsyncContext
        {
            private bool cancelled;
            private ClientHttpRequestCompletedEventArgs completedEventArgs;
            private Exception error;
            private bool hasChanged;
            private IClientHttpResponse response;
            private object userState;

            public ResponseAsyncContext(ClientHttpRequestCompletedEventArgs completedEventArgs)
            {
                this.cancelled = completedEventArgs.Cancelled;
                this.error = completedEventArgs.Error;
                if (this.error == null)
                {
                    this.response = completedEventArgs.Response;
                }
                this.userState = completedEventArgs.UserState;
                this.completedEventArgs = completedEventArgs;
            }

            public ClientHttpRequestCompletedEventArgs GetCompletedEventArgs()
            {
                if (!this.hasChanged)
                {
                    return this.completedEventArgs;
                }
                return new ClientHttpRequestCompletedEventArgs(this.response, this.error, this.cancelled, this.userState);
            }

            public bool Cancelled
            {
                get
                {
                    return this.cancelled;
                }
                set
                {
                    this.hasChanged = true;
                    this.cancelled = value;
                }
            }

            public Exception Error
            {
                get
                {
                    return this.error;
                }
                set
                {
                    this.hasChanged = true;
                    this.error = value;
                }
            }

            public IClientHttpResponse Response
            {
                get
                {
                    return this.response;
                }
                set
                {
                    this.hasChanged = true;
                    this.response = value;
                }
            }

            public object UserState
            {
                get
                {
                    return this.userState;
                }
                set
                {
                    this.hasChanged = true;
                    this.userState = value;
                }
            }
        }
    }
}

