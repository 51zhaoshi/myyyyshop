namespace Maticsoft.Common.Mail
{
    using Maticsoft.Common.Mime;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    public sealed class Pop3Client : IDisposable
    {
        private TcpClient _client;
        private Stream _clientStream;
        private Pop3State _currentState;
        private string _hostname;
        private string _password;
        private int _port;
        private string _username;
        private bool _useSsl;
        private static readonly int DefaultPort = 110;
        private Action<string> Trace;

        public event Action<string> Trace
        {
            add
            {
                Action<string> action2;
                Action<string> trace = this.Trace;
                do
                {
                    action2 = trace;
                    Action<string> action3 = (Action<string>) Delegate.Combine(action2, value);
                    trace = Interlocked.CompareExchange<Action<string>>(ref this.Trace, action3, action2);
                }
                while (trace != action2);
            }
            remove
            {
                Action<string> action2;
                Action<string> trace = this.Trace;
                do
                {
                    action2 = trace;
                    Action<string> action3 = (Action<string>) Delegate.Remove(action2, value);
                    trace = Interlocked.CompareExchange<Action<string>>(ref this.Trace, action3, action2);
                }
                while (trace != action2);
            }
        }

        private Pop3Client()
        {
            this._client = new TcpClient();
            this._currentState = Pop3State.Unknown;
        }

        public Pop3Client(string hostname, string username, string password) : this(hostname, DefaultPort, false, username, password)
        {
        }

        public Pop3Client(string hostname, bool useSsl, string username, string password) : this(hostname, DefaultPort, useSsl, username, password)
        {
        }

        public Pop3Client(string hostname, int port, bool useSsl, string username, string password) : this()
        {
            if (string.IsNullOrEmpty(hostname))
            {
                throw new ArgumentNullException("hostname");
            }
            if (port < 0)
            {
                throw new ArgumentOutOfRangeException("port");
            }
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }
            this._hostname = hostname;
            this._port = port;
            this._useSsl = useSsl;
            this._username = username;
            this._password = password;
        }

        public void Authenticate()
        {
            this.Connect();
            using (UserCommand command = new UserCommand(this._clientStream, this._username))
            {
                this.ExecuteCommand<Pop3Response, UserCommand>(command);
            }
            using (PassCommand command2 = new PassCommand(this._clientStream, this._password))
            {
                this.ExecuteCommand<Pop3Response, PassCommand>(command2);
            }
            this._currentState = Pop3State.Transaction;
        }

        private void Connect()
        {
            if (this._client == null)
            {
                this._client = new TcpClient();
            }
            if (!this._client.Connected)
            {
                ConnectResponse response;
                this.SetState(Pop3State.Unknown);
                using (ConnectCommand command = new ConnectCommand(this._client, this._hostname, this._port, this._useSsl))
                {
                    this.TraceCommand<ConnectCommand, ConnectResponse>(command);
                    response = command.Execute(this.CurrentState);
                    this.EnsureResponse(response);
                }
                this.SetClientStream(response.NetworkStream);
                this.SetState(Pop3State.Authorization);
            }
        }

        public void Dele(Pop3ListItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            using (DeleCommand command = new DeleCommand(this._clientStream, item.MessageId))
            {
                this.ExecuteCommand<Pop3Response, DeleCommand>(command);
            }
        }

        private void Disconnect()
        {
            if (this._clientStream != null)
            {
                this._clientStream.Close();
            }
            if (this._client != null)
            {
                this._client.Close();
                this._client = null;
            }
        }

        public void Dispose()
        {
            this.Disconnect();
        }

        private void EnsureConnection()
        {
            if (!this._client.Connected)
            {
                throw new Pop3Exception("Pop3 client is not connected.");
            }
        }

        private void EnsureResponse(Pop3Response response)
        {
            this.EnsureResponse(response, string.Empty);
        }

        private void EnsureResponse(Pop3Response response, string error)
        {
            if (response == null)
            {
                throw new Pop3Exception("Unable to get Response.  Response object null.");
            }
            if (!response.StatusIndicator)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(error))
                {
                    message = response.HostMessage;
                }
                else
                {
                    message = error + ": " + error;
                }
                throw new Pop3Exception(message);
            }
        }

        private TResponse ExecuteCommand<TResponse, TCommand>(TCommand command) where TResponse: Pop3Response where TCommand: Pop3Command<TResponse>
        {
            this.EnsureConnection();
            this.TraceCommand<TCommand, TResponse>(command);
            TResponse response = command.Execute(this.CurrentState);
            this.EnsureResponse(response);
            return response;
        }

        public List<Pop3ListItem> List()
        {
            ListResponse response;
            using (ListCommand command = new ListCommand(this._clientStream))
            {
                response = this.ExecuteCommand<ListResponse, ListCommand>(command);
            }
            return response.Items;
        }

        public Pop3ListItem List(int messageId)
        {
            ListResponse response;
            using (ListCommand command = new ListCommand(this._clientStream, messageId))
            {
                response = this.ExecuteCommand<ListResponse, ListCommand>(command);
            }
            return new Pop3ListItem(response.MessageNumber, response.Octets);
        }

        public void Noop()
        {
            using (NoopCommand command = new NoopCommand(this._clientStream))
            {
                this.ExecuteCommand<Pop3Response, NoopCommand>(command);
            }
        }

        private void OnTrace(string message)
        {
            if (this.Trace != null)
            {
                this.Trace(message);
            }
        }

        public void Quit()
        {
            using (QuitCommand command = new QuitCommand(this._clientStream))
            {
                this.ExecuteCommand<Pop3Response, QuitCommand>(command);
                if (this.CurrentState.Equals(Pop3State.Transaction))
                {
                    this.SetState(Pop3State.Update);
                }
                this.Disconnect();
                this.SetState(Pop3State.Unknown);
            }
        }

        public MailMessageEx RetrMailMessageEx(Pop3ListItem item)
        {
            MailMessageEx ex = this.RetrMimeEntity(item).ToMailMessageEx();
            if (ex != null)
            {
                ex.MessageNumber = item.MessageId;
            }
            return ex;
        }

        public MimeEntity RetrMimeEntity(Pop3ListItem item)
        {
            RetrResponse response;
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.MessageId < 1)
            {
                throw new ArgumentOutOfRangeException("item.MessageId");
            }
            using (RetrCommand command = new RetrCommand(this._clientStream, item.MessageId))
            {
                response = this.ExecuteCommand<RetrResponse, RetrCommand>(command);
            }
            MimeReader reader = new MimeReader(response.MessageLines);
            return reader.CreateMimeEntity();
        }

        public void Rset()
        {
            using (RsetCommand command = new RsetCommand(this._clientStream))
            {
                this.ExecuteCommand<Pop3Response, RsetCommand>(command);
            }
        }

        private void SetClientStream(Stream networkStream)
        {
            if (this._clientStream != null)
            {
                this._clientStream.Dispose();
            }
            this._clientStream = networkStream;
        }

        private void SetState(Pop3State state)
        {
            this._currentState = state;
        }

        public Maticsoft.Common.Mail.Stat Stat()
        {
            StatResponse response;
            using (StatCommand command = new StatCommand(this._clientStream))
            {
                response = this.ExecuteCommand<StatResponse, StatCommand>(command);
            }
            return new Maticsoft.Common.Mail.Stat(response.MessageCount, response.Octets);
        }

        public MailMessageEx Top(int messageId, int lineCount)
        {
            RetrResponse response;
            if (messageId < 1)
            {
                throw new ArgumentOutOfRangeException("messageId");
            }
            if (lineCount < 0)
            {
                throw new ArgumentOutOfRangeException("lineCount");
            }
            using (TopCommand command = new TopCommand(this._clientStream, messageId, lineCount))
            {
                response = this.ExecuteCommand<RetrResponse, TopCommand>(command);
            }
            MimeEntity entity = new MimeReader(response.MessageLines).CreateMimeEntity();
            MailMessageEx ex = entity.ToMailMessageEx();
            ex.Octets = response.Octets;
            ex.MessageNumber = messageId;
            return entity.ToMailMessageEx();
        }

        private void TraceCommand<TCommand, TResponse>(TCommand command) where TCommand: Pop3Command<TResponse> where TResponse: Pop3Response
        {
            Action<string> action = null;
            if (this.Trace != null)
            {
                if (action == null)
                {
                    action = delegate (string message) {
                        this.OnTrace(message);
                    };
                }
                command.Trace += action;
            }
        }

        public Pop3State CurrentState
        {
            get
            {
                return this._currentState;
            }
        }

        public string Hostname
        {
            get
            {
                return this._hostname;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public int Port
        {
            get
            {
                return this._port;
            }
        }

        public string Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public bool UseSsl
        {
            get
            {
                return this._useSsl;
            }
        }
    }
}

