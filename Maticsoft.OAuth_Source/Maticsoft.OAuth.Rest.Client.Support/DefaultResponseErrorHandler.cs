namespace Maticsoft.OAuth.Rest.Client.Support
{
    using Maticsoft.OAuth.Http;
    using Maticsoft.OAuth.Http.Client;
    using Maticsoft.OAuth.Rest.Client;
    using Maticsoft.OAuth.Util;
    using System;
    using System.IO;
    using System.Net;

    public class DefaultResponseErrorHandler : IResponseErrorHandler
    {
        public virtual void HandleError(Uri requestUri, HttpMethod requestMethod, IClientHttpResponse response)
        {
            byte[] body = null;
            Stream source = response.Body;
            if (source != null)
            {
                using (MemoryStream stream2 = new MemoryStream())
                {
                    IoUtils.CopyStream(source, stream2);
                    body = stream2.ToArray();
                }
            }
            this.HandleError(requestUri, requestMethod, new HttpResponseMessage<byte[]>(body, response.Headers, response.StatusCode, response.StatusDescription));
        }

        public virtual void HandleError(Uri requestUri, HttpMethod requestMethod, HttpResponseMessage<byte[]> response)
        {
            switch (((int) (response.StatusCode / HttpStatusCode.Continue)))
            {
                case 4:
                    throw new HttpClientErrorException(requestUri, requestMethod, response);

                case 5:
                    throw new HttpServerErrorException(requestUri, requestMethod, response);
            }
            throw new HttpResponseException(requestUri, requestMethod, response);
        }

        protected virtual bool HasError(HttpStatusCode statusCode)
        {
            int num = (int) (statusCode / HttpStatusCode.Continue);
            if (num != 4)
            {
                return (num == 5);
            }
            return true;
        }

        public virtual bool HasError(Uri requestUri, HttpMethod requestMethod, IClientHttpResponse response)
        {
            return this.HasError(response.StatusCode);
        }
    }
}

