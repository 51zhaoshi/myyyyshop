namespace Maticsoft.OAuth.Http.Client
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Util;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Threading;

    public class WebClientHttpRequest : IClientHttpRequest, IHttpOutputMessage
    {
        private Action<Stream> body;
        private HttpHeaders headers;
        private System.Net.HttpWebRequest httpWebRequest;
        private bool isCancelled;
        private bool isExecuted;

        public WebClientHttpRequest(System.Net.HttpWebRequest request)
        {
            ArgumentUtils.AssertNotNull(request, "request");
            this.httpWebRequest = request;
            this.headers = new HttpHeaders();
        }

        public void CancelAsync()
        {
            this.isCancelled = true;
            try
            {
                if (this.httpWebRequest != null)
                {
                    this.httpWebRequest.Abort();
                }
            }
            catch (Exception exception)
            {
                if (((exception is OutOfMemoryException) || (exception is StackOverflowException)) || (exception is ThreadAbortException))
                {
                    throw;
                }
            }
        }

        protected virtual IClientHttpResponse CreateClientHttpResponse(HttpWebResponse response)
        {
            return new WebClientHttpResponse(response);
        }

        protected void EnsureNotExecuted()
        {
            if (this.isExecuted)
            {
                throw new InvalidOperationException("Client HTTP request already executed or is currently executing.");
            }
        }

        public IClientHttpResponse Execute()
        {
            this.EnsureNotExecuted();
            try
            {
                this.PrepareForExecution();
                if (this.body != null)
                {
                    using (Stream stream = this.httpWebRequest.GetRequestStream())
                    {
                        this.body(stream);
                    }
                }
                HttpWebResponse response = this.httpWebRequest.GetResponse() as HttpWebResponse;
                if (this.httpWebRequest.HaveResponse && (response != null))
                {
                    return this.CreateClientHttpResponse(response);
                }
            }
            catch (WebException exception)
            {
                HttpWebResponse response2 = exception.Response as HttpWebResponse;
                if (response2 == null)
                {
                    throw;
                }
                return this.CreateClientHttpResponse(response2);
            }
            finally
            {
                this.isExecuted = true;
            }
            return null;
        }

        public void ExecuteAsync(object state, Action<ClientHttpRequestCompletedEventArgs> executeCompleted)
        {
            this.EnsureNotExecuted();
            AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(state);
            ExecuteState state2 = new ExecuteState(executeCompleted, asyncOperation);
            try
            {
                this.PrepareForExecution();
                if (this.body != null)
                {
                    this.httpWebRequest.BeginGetRequestStream(new AsyncCallback(this.ExecuteRequestCallback), state2);
                }
                else
                {
                    this.HttpWebRequest.BeginGetResponse(new AsyncCallback(this.ExecuteResponseCallback), state2);
                }
            }
            catch (Exception exception)
            {
                if (((exception is ThreadAbortException) || (exception is StackOverflowException)) || (exception is OutOfMemoryException))
                {
                    throw;
                }
                this.ExecuteAsyncCallback(state2, null, exception);
            }
            finally
            {
                this.isExecuted = true;
            }
        }

        private void ExecuteAsyncCallback(ExecuteState state, IClientHttpResponse response, Exception exception)
        {
            ClientHttpRequestCompletedEventArgs eventArgs = new ClientHttpRequestCompletedEventArgs(response, exception, this.isCancelled, state.AsyncOperation.UserSuppliedState);
            ExecuteCallbackArgs<ClientHttpRequestCompletedEventArgs> arg = new ExecuteCallbackArgs<ClientHttpRequestCompletedEventArgs>(eventArgs, state.ExecuteCompleted);
            state.AsyncOperation.PostOperationCompleted(new SendOrPostCallback(WebClientHttpRequest.ExecuteResponseReceived), arg);
        }

        private void ExecuteRequestCallback(IAsyncResult result)
        {
            ExecuteState asyncState = (ExecuteState) result.AsyncState;
            try
            {
                using (Stream stream = this.httpWebRequest.EndGetRequestStream(result))
                {
                    this.body(stream);
                    stream.Flush();
                    stream.Close();
                }
                this.httpWebRequest.BeginGetResponse(new AsyncCallback(this.ExecuteResponseCallback), asyncState);
            }
            catch (Exception exception)
            {
                if (((exception is ThreadAbortException) || (exception is StackOverflowException)) || (exception is OutOfMemoryException))
                {
                    throw;
                }
                this.ExecuteAsyncCallback(asyncState, null, exception);
            }
        }

        private void ExecuteResponseCallback(IAsyncResult result)
        {
            ExecuteState asyncState = (ExecuteState) result.AsyncState;
            IClientHttpResponse response = null;
            Exception exception = null;
            try
            {
                HttpWebResponse response2 = this.httpWebRequest.EndGetResponse(result) as HttpWebResponse;
                if (this.httpWebRequest.HaveResponse && (response2 != null))
                {
                    response = this.CreateClientHttpResponse(response2);
                }
            }
            catch (Exception exception2)
            {
                if (((exception2 is ThreadAbortException) || (exception2 is StackOverflowException)) || (exception2 is OutOfMemoryException))
                {
                    throw;
                }
                exception = exception2;
                if (exception2 is WebException)
                {
                    HttpWebResponse response3 = ((WebException) exception2).Response as HttpWebResponse;
                    if (response3 != null)
                    {
                        exception = null;
                        response = this.CreateClientHttpResponse(response3);
                    }
                }
            }
            this.ExecuteAsyncCallback(asyncState, response, exception);
        }

        private static void ExecuteResponseReceived(object arg)
        {
            ExecuteCallbackArgs<ClientHttpRequestCompletedEventArgs> args = (ExecuteCallbackArgs<ClientHttpRequestCompletedEventArgs>) arg;
            if (args.Callback != null)
            {
                args.Callback(args.EventArgs);
            }
        }

        protected virtual void PrepareForExecution()
        {
            foreach (string str in this.headers)
            {
                string str2;
                switch (str.ToUpper(CultureInfo.InvariantCulture))
                {
                    case "ACCEPT":
                    {
                        this.httpWebRequest.Accept = this.headers[str];
                        continue;
                    }
                    case "CONTENT-LENGTH":
                    {
                        this.httpWebRequest.ContentLength = this.headers.ContentLength;
                        continue;
                    }
                    case "CONTENT-TYPE":
                    {
                        this.httpWebRequest.ContentType = this.headers[str];
                        continue;
                    }
                    case "DATE":
                    {
                        DateTime? date = this.headers.Date;
                        if (!date.HasValue)
                        {
                            break;
                        }
                        this.httpWebRequest.Date = date.Value;
                        continue;
                    }
                    case "HOST":
                    {
                        this.httpWebRequest.Host = this.headers[str];
                        continue;
                    }
                    case "CONNECTION":
                    {
                        str2 = this.headers[str];
                        if (!str2.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase))
                        {
                            goto Label_020C;
                        }
                        this.httpWebRequest.KeepAlive = true;
                        continue;
                    }
                    case "EXPECT":
                    {
                        this.httpWebRequest.Expect = this.headers[str];
                        continue;
                    }
                    case "IF-MODIFIED-SINCE":
                    {
                        DateTime? ifModifiedSince = this.headers.IfModifiedSince;
                        if (!ifModifiedSince.HasValue)
                        {
                            goto Label_0276;
                        }
                        this.httpWebRequest.IfModifiedSince = ifModifiedSince.Value;
                        continue;
                    }
                    case "RANGE":
                    {
                        string str3 = this.headers[str];
                        try
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            string rangeSpecifier = strArray[0];
                            string str5 = strArray[1];
                            int index = str5.IndexOf('-');
                            long from = long.Parse(str5.Substring(0, index));
                            long to = long.Parse(str5.Substring(index + 1));
                            this.httpWebRequest.AddRange(rangeSpecifier, from, to);
                        }
                        catch
                        {
                            this.httpWebRequest.Headers[str] = this.headers[str];
                        }
                        continue;
                    }
                    case "REFERER":
                    {
                        this.httpWebRequest.Referer = this.headers[str];
                        continue;
                    }
                    case "TRANSFER-ENCODING":
                    {
                        this.httpWebRequest.SendChunked = true;
                        string str6 = this.headers[str];
                        if (!str6.Equals("Chunked", StringComparison.OrdinalIgnoreCase))
                        {
                            this.httpWebRequest.TransferEncoding = str6;
                        }
                        continue;
                    }
                    case "USER-AGENT":
                    {
                        this.httpWebRequest.UserAgent = this.headers[str];
                        continue;
                    }
                    default:
                        goto Label_038F;
                }
                this.httpWebRequest.Date = DateTime.MinValue;
                continue;
            Label_020C:
                if (!str2.Equals("Close", StringComparison.OrdinalIgnoreCase))
                {
                    this.httpWebRequest.Connection = str2;
                }
                continue;
            Label_0276:
                this.httpWebRequest.IfModifiedSince = DateTime.MinValue;
                continue;
            Label_038F:
                this.httpWebRequest.Headers[str] = this.headers[str];
            }
        }

        public Action<Stream> Body
        {
            set
            {
                this.body = value;
            }
        }

        public HttpHeaders Headers
        {
            get
            {
                return this.headers;
            }
        }

        public System.Net.HttpWebRequest HttpWebRequest
        {
            get
            {
                return this.httpWebRequest;
            }
        }

        public HttpMethod Method
        {
            get
            {
                return new HttpMethod(this.httpWebRequest.Method);
            }
        }

        public System.Uri Uri
        {
            get
            {
                return this.httpWebRequest.RequestUri;
            }
        }

        private class ExecuteCallbackArgs<T> where T: class
        {
            public Action<T> Callback;
            public T EventArgs;

            public ExecuteCallbackArgs(T eventArgs, Action<T> callback)
            {
                this.EventArgs = eventArgs;
                this.Callback = callback;
            }
        }

        private class ExecuteState
        {
            public System.ComponentModel.AsyncOperation AsyncOperation;
            public Action<ClientHttpRequestCompletedEventArgs> ExecuteCompleted;

            public ExecuteState(Action<ClientHttpRequestCompletedEventArgs> executeCompleted, System.ComponentModel.AsyncOperation asyncOperation)
            {
                this.ExecuteCompleted = executeCompleted;
                this.AsyncOperation = asyncOperation;
            }
        }
    }
}

