namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal sealed class QuitCommand : Pop3Command<Pop3Response>
    {
        public QuitCommand(Stream stream) : base(stream, false, Pop3State.Transaction | Pop3State.Authorization)
        {
        }

        protected override byte[] CreateRequestMessage()
        {
            return base.GetRequestMessage(new string[] { "QUIT\r\n" });
        }
    }
}

