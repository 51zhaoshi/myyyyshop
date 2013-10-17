namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal sealed class ConnectResponse : Pop3Response
    {
        private Stream _networkStream;

        public ConnectResponse(Pop3Response response, Stream networkStream) : base(response.ResponseContents, response.HostMessage, response.StatusIndicator)
        {
            if (networkStream == null)
            {
                throw new ArgumentNullException("networkStream");
            }
            this._networkStream = networkStream;
        }

        public Stream NetworkStream
        {
            get
            {
                return this._networkStream;
            }
        }
    }
}

