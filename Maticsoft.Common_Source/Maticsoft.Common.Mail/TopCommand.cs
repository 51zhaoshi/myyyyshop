namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal sealed class TopCommand : Pop3Command<RetrResponse>
    {
        private int _lineCount;
        private int _messageNumber;

        internal TopCommand(Stream stream, int messageNumber, int lineCount) : base(stream, true, Pop3State.Transaction)
        {
            if (messageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("messageNumber");
            }
            if (lineCount < 0)
            {
                throw new ArgumentOutOfRangeException("lineCount");
            }
            this._messageNumber = messageNumber;
            this._lineCount = lineCount;
        }

        protected override byte[] CreateRequestMessage()
        {
            return base.GetRequestMessage(new string[] { "TOP ", this._messageNumber.ToString(), " ", this._lineCount.ToString(), "\r\n" });
        }

        protected override RetrResponse CreateResponse(byte[] buffer)
        {
            Pop3Response response = Pop3Response.CreateResponse(buffer);
            if (response == null)
            {
                return null;
            }
            return new RetrResponse(response, base.GetResponseLines(base.StripPop3HostMessage(buffer, response.HostMessage)));
        }
    }
}

