namespace Maticsoft.OAuth.Http.Client
{
    using System;
    using System.ComponentModel;

    public class ClientHttpRequestCompletedEventArgs : AsyncCompletedEventArgs
    {
        private IClientHttpResponse response;

        public ClientHttpRequestCompletedEventArgs(IClientHttpResponse response, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.response = response;
        }

        public IClientHttpResponse Response
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return this.response;
            }
        }
    }
}

