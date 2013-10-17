namespace Maticsoft.Common.Mail
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    internal abstract class Pop3Command<T> : IDisposable where T: Pop3Response
    {
        private byte[] _buffer;
        private bool _isMultiline;
        private ManualResetEvent _manualResetEvent;
        private Stream _networkStream;
        private MemoryStream _responseContents;
        private Pop3State _validExecuteState;
        private const int BufferSize = 0x400;
        private const string MessageTerminator = ".";
        private const string MultilineMessageTerminator = "\r\n.\r\n";
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

        public Pop3Command(Stream stream, bool isMultiline, Pop3State validExecuteState)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            this._manualResetEvent = new ManualResetEvent(false);
            this._buffer = new byte[0x400];
            this._responseContents = new MemoryStream();
            this._networkStream = stream;
            this._isMultiline = isMultiline;
            this._validExecuteState = validExecuteState;
        }

        protected abstract byte[] CreateRequestMessage();
        protected virtual T CreateResponse(byte[] buffer)
        {
            return (Pop3Response.CreateResponse(buffer) as T);
        }

        public void Dispose()
        {
            if (this._responseContents != null)
            {
                this._responseContents.Dispose();
            }
        }

        protected void EnsurePop3State(Pop3State currentState)
        {
            if ((currentState & this.ValidExecuteState) != currentState)
            {
                throw new Pop3Exception(string.Format("This command is being executedin an invalid execution state.  Current:{0}, Valid:{1}", currentState, this.ValidExecuteState));
            }
        }

        internal virtual T Execute(Pop3State currentState)
        {
            this.EnsurePop3State(currentState);
            byte[] message = this.CreateRequestMessage();
            if (message != null)
            {
                this.Send(message);
            }
            T local = this.CreateResponse(this.GetResponse());
            if (local == null)
            {
                return default(T);
            }
            this.OnTrace(local.HostMessage);
            return local;
        }

        private void GetMultiLineResponseCallback(IAsyncResult ar)
        {
            int bytesReceived = this._networkStream.EndRead(ar);
            if (this.WriteReceivedBytesToBuffer(bytesReceived).EndsWith("\r\n.\r\n") || (bytesReceived == 0))
            {
                this._manualResetEvent.Set();
            }
            else
            {
                this.Receive(new AsyncCallback(this.GetMultiLineResponseCallback));
            }
        }

        protected byte[] GetRequestMessage(params string[] args)
        {
            string message = string.Join(string.Empty, args);
            this.OnTrace(message);
            return Encoding.ASCII.GetBytes(message);
        }

        private byte[] GetResponse()
        {
            AsyncCallback callback;
            byte[] buffer;
            if (this._isMultiline)
            {
                callback = new AsyncCallback(this.GetMultiLineResponseCallback);
            }
            else
            {
                callback = new AsyncCallback(this.GetSingleLineResponseCallback);
            }
            try
            {
                this.Receive(callback);
                this._manualResetEvent.WaitOne();
                buffer = this._responseContents.ToArray();
            }
            catch (SocketException exception)
            {
                throw new Pop3Exception("Unable to get response.", exception);
            }
            return buffer;
        }

        protected string[] GetResponseLines(MemoryStream stream)
        {
            string[] strArray;
            List<string> list = new List<string>();
            using (StreamReader reader = new StreamReader(stream))
            {
                try
                {
                    while (true)
                    {
                        string item = reader.ReadLine();
                        if (item.StartsWith("."))
                        {
                            if (item == ".")
                            {
                                goto Label_004E;
                            }
                            item = item.Substring(1);
                        }
                        list.Add(item);
                    }
                }
                catch (IOException exception)
                {
                    throw new Pop3Exception("Unable to get response lines.", exception);
                }
            Label_004E:
                strArray = list.ToArray();
            }
            return strArray;
        }

        private void GetSingleLineResponseCallback(IAsyncResult ar)
        {
            int bytesReceived = this._networkStream.EndRead(ar);
            if (this.WriteReceivedBytesToBuffer(bytesReceived).EndsWith("\r\n"))
            {
                this._manualResetEvent.Set();
            }
            else
            {
                this.Receive(new AsyncCallback(this.GetSingleLineResponseCallback));
            }
        }

        protected void OnTrace(string message)
        {
            if (this.Trace != null)
            {
                this.Trace(message);
            }
        }

        private IAsyncResult Receive(AsyncCallback callback)
        {
            return this._networkStream.BeginRead(this._buffer, 0, this._buffer.Length, callback, null);
        }

        private void Send(byte[] message)
        {
            try
            {
                this._networkStream.Write(message, 0, message.Length);
            }
            catch (SocketException exception)
            {
                throw new Pop3Exception("Unable to send the request message: " + Encoding.ASCII.GetString(message), exception);
            }
        }

        protected MemoryStream StripPop3HostMessage(byte[] bytes, string header)
        {
            int index = header.Length + 2;
            return new MemoryStream(bytes, index, bytes.Length - index);
        }

        private string WriteReceivedBytesToBuffer(int bytesReceived)
        {
            this._responseContents.Write(this._buffer, 0, bytesReceived);
            byte[] bytes = this._responseContents.ToArray();
            return Encoding.ASCII.GetString(bytes, (bytes.Length > 5) ? (bytes.Length - 5) : 0, 5);
        }

        protected bool IsMultiline
        {
            get
            {
                return this._isMultiline;
            }
            set
            {
                this._isMultiline = value;
            }
        }

        public Stream NetworkStream
        {
            get
            {
                return this._networkStream;
            }
            set
            {
                this._networkStream = value;
            }
        }

        public Pop3State ValidExecuteState
        {
            get
            {
                return this._validExecuteState;
            }
        }
    }
}

