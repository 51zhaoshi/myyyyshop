namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal class Pop3Response
    {
        private string _hostMessage;
        private byte[] _responseContents;
        private bool _statusIndicator;

        public Pop3Response(byte[] responseContents, string hostMessage, bool statusIndicator)
        {
            if (responseContents == null)
            {
                throw new ArgumentNullException("responseBuffer");
            }
            if (string.IsNullOrEmpty(hostMessage))
            {
                throw new ArgumentNullException("hostMessage");
            }
            this._responseContents = responseContents;
            this._hostMessage = hostMessage;
            this._statusIndicator = statusIndicator;
        }

        public static Pop3Response CreateResponse(byte[] responseContents)
        {
            MemoryStream stream = new MemoryStream(responseContents);
            using (StreamReader reader = new StreamReader(stream))
            {
                string hostMessage = reader.ReadLine();
                if (hostMessage == null)
                {
                    return null;
                }
                return new Pop3Response(responseContents, hostMessage, hostMessage.StartsWith("+OK"));
            }
        }

        public string HostMessage
        {
            get
            {
                return this._hostMessage;
            }
        }

        internal byte[] ResponseContents
        {
            get
            {
                return this._responseContents;
            }
        }

        public bool StatusIndicator
        {
            get
            {
                return this._statusIndicator;
            }
        }
    }
}

