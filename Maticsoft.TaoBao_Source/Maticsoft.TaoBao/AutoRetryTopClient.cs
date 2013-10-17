namespace Maticsoft.TaoBao
{
    using Maticsoft.TaoBao.Request;
    using System;
    using System.Threading;

    public class AutoRetryTopClient : DefaultTopClient
    {
        private int maxRetryCount;
        private static readonly TopException RETRY_FAIL = new TopException("sdk.retry-call-fail", "API调用重试失败");
        [ThreadStatic]
        private static int retryCounter = -1;
        private int retryWaitTime;
        private bool throwIfOverMaxRetry;

        public AutoRetryTopClient(string serverUrl, string appKey, string appSecret) : base(serverUrl, appKey, appSecret)
        {
            this.maxRetryCount = 3;
            this.retryWaitTime = 500;
        }

        public AutoRetryTopClient(string serverUrl, string appKey, string appSecret, string format) : base(serverUrl, appKey, appSecret, format)
        {
            this.maxRetryCount = 3;
            this.retryWaitTime = 500;
        }

        public T Execute<T>(ITopRequest<T> request) where T: TopResponse
        {
            return this.Execute<T>(request, null);
        }

        public T Execute<T>(ITopRequest<T> request, string session) where T: TopResponse
        {
            return this.Execute<T>(request, session, DateTime.Now);
        }

        public T Execute<T>(ITopRequest<T> request, string session, DateTime timestamp) where T: TopResponse
        {
            T local = default(T);
            try
            {
                retryCounter++;
                local = base.Execute<T>(request, session, timestamp);
                if (local.IsError)
                {
                    if (retryCounter < this.maxRetryCount)
                    {
                        if ((local.SubErrCode == null) || !local.SubErrCode.StartsWith("isp."))
                        {
                            return local;
                        }
                        Thread.Sleep(this.retryWaitTime);
                        return this.Execute<T>(request, session, timestamp);
                    }
                    if (this.throwIfOverMaxRetry)
                    {
                        throw RETRY_FAIL;
                    }
                }
                return local;
            }
            catch (Exception exception)
            {
                if ((exception != RETRY_FAIL) && (retryCounter < this.maxRetryCount))
                {
                    Thread.Sleep(this.retryWaitTime);
                    return this.Execute<T>(request, session, timestamp);
                }
            }
            finally
            {
                this.retryWaitTime = -1;
            }
            return local;
        }

        public void SetMaxRetryCount(int maxRetryCount)
        {
            this.maxRetryCount = maxRetryCount;
        }

        public void SetRetryWaitTime(int retryWaitTime)
        {
            this.retryWaitTime = retryWaitTime;
        }

        public void SetThrowIfOverMaxRetry(bool throwIfOverMaxRetry)
        {
            this.throwIfOverMaxRetry = throwIfOverMaxRetry;
        }
    }
}

