namespace Maticsoft.Common.Mail
{
    using System;
    using System.IO;

    internal sealed class StatCommand : Pop3Command<StatResponse>
    {
        public StatCommand(Stream stream) : base(stream, false, Pop3State.Transaction)
        {
        }

        protected override byte[] CreateRequestMessage()
        {
            return base.GetRequestMessage(new string[] { "STAT\r\n" });
        }

        protected override StatResponse CreateResponse(byte[] buffer)
        {
            Pop3Response response = Pop3Response.CreateResponse(buffer);
            string[] strArray = response.HostMessage.Split(new char[] { ' ' });
            if (strArray.Length < 3)
            {
                throw new Pop3Exception("Invalid response message: " + response.HostMessage);
            }
            int messageCount = Convert.ToInt32(strArray[1]);
            return new StatResponse(response, messageCount, Convert.ToInt64(strArray[2]));
        }
    }
}

