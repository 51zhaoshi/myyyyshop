namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal sealed class NoopCommand : Pop3Command<Pop3Response>
    {
        public NoopCommand(Stream stream) : base(stream, false, Pop3State.Transaction)
        {
        }

        protected override byte[] CreateRequestMessage()
        {
            return base.GetRequestMessage(new string[] { "NOOP\r\n" });
        }
    }
}

