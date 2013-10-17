namespace Maticsoft.OAuth.Rest.Client
{
    using System;
    using System.ComponentModel;

    public class RestOperationCompletedEventArgs<T> : AsyncCompletedEventArgs
    {
        private T response;

        public RestOperationCompletedEventArgs(T response, Exception exception, bool cancelled, object userState) : base(exception, cancelled, userState)
        {
            this.response = response;
        }

        public T Response
        {
            get
            {
                base.RaiseExceptionIfNecessary();
                return this.response;
            }
        }
    }
}

