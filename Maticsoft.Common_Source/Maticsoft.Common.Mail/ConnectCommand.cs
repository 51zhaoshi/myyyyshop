namespace Maticsoft.Common.Mail
{
    using System;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Authentication;

    internal sealed class ConnectCommand : Pop3Command<ConnectResponse>
    {
        private TcpClient _client;
        private string _hostname;
        private int _port;
        private bool _useSsl;

        public ConnectCommand(TcpClient client, string hostname, int port, bool useSsl) : base(new MemoryStream(), false, Pop3State.Unknown)
        {
            if (client == null)
            {
                throw new ArgumentNullException("client");
            }
            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException("hostname");
            }
            if (port < 1)
            {
                throw new ArgumentOutOfRangeException("port");
            }
            this._client = client;
            this._hostname = hostname;
            this._port = port;
            this._useSsl = useSsl;
        }

        protected override byte[] CreateRequestMessage()
        {
            return null;
        }

        protected override ConnectResponse CreateResponse(byte[] buffer)
        {
            return new ConnectResponse(Pop3Response.CreateResponse(buffer), base.NetworkStream);
        }

        internal override ConnectResponse Execute(Pop3State currentState)
        {
            base.EnsurePop3State(currentState);
            try
            {
                this._client.Connect(this._hostname, this._port);
                this.SetClientStream();
            }
            catch (SocketException exception)
            {
                throw new Pop3Exception(string.Format("Unable to connect to {0}:{1}.", this._hostname, this._port), exception);
            }
            return base.Execute(currentState);
        }

        private void SetClientStream()
        {
            if (this._useSsl)
            {
                try
                {
                    base.NetworkStream = new SslStream(this._client.GetStream(), true);
                    ((SslStream) base.NetworkStream).AuthenticateAsClient(this._hostname);
                    return;
                }
                catch (ArgumentException exception)
                {
                    throw new Pop3Exception("Unable to create Ssl Stream for hostname: " + this._hostname, exception);
                }
                catch (AuthenticationException exception2)
                {
                    throw new Pop3Exception("Unable to authenticate ssl stream for hostname: " + this._hostname, exception2);
                }
                catch (InvalidOperationException exception3)
                {
                    throw new Pop3Exception("There was a problem  attempting to authenticate this SSL stream for hostname: " + this._hostname, exception3);
                }
            }
            base.NetworkStream = this._client.GetStream();
        }
    }
}

