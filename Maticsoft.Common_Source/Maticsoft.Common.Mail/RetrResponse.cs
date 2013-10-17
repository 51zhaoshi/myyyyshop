namespace Maticsoft.Common.Mail
{
    using System;

    internal sealed class RetrResponse : Pop3Response
    {
        private string[] _messageLines;
        private long _octects;

        public RetrResponse(Pop3Response response, string[] messageLines) : base(response.ResponseContents, response.HostMessage, response.StatusIndicator)
        {
            if (messageLines == null)
            {
                throw new ArgumentNullException("messageLines");
            }
            string[] strArray = response.HostMessage.Split(new char[] { ' ' });
            if (strArray.Length == 2)
            {
                this._octects = Convert.ToInt64(strArray[1]);
            }
            this._messageLines = messageLines;
        }

        public string[] MessageLines
        {
            get
            {
                return this._messageLines;
            }
        }

        public long Octets
        {
            get
            {
                return this._octects;
            }
        }
    }
}

