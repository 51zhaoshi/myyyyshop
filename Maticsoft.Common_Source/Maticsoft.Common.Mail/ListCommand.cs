namespace Maticsoft.Common.Mail
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal sealed class ListCommand : Pop3Command<ListResponse>
    {
        private int _messageId;

        public ListCommand(Stream stream) : base(stream, true, Pop3State.Transaction)
        {
        }

        public ListCommand(Stream stream, int messageId) : this(stream)
        {
            if (messageId < 0)
            {
                throw new ArgumentOutOfRangeException("messageId");
            }
            this._messageId = messageId;
            base.IsMultiline = false;
        }

        protected override byte[] CreateRequestMessage()
        {
            string str = "LIST ";
            if (!base.IsMultiline)
            {
                str = str + this._messageId.ToString();
            }
            return base.GetRequestMessage(new string[] { str, "\r\n" });
        }

        protected override ListResponse CreateResponse(byte[] buffer)
        {
            List<Pop3ListItem> list;
            Pop3Response response = Pop3Response.CreateResponse(buffer);
            if (base.IsMultiline)
            {
                list = new List<Pop3ListItem>();
                foreach (string str in base.GetResponseLines(base.StripPop3HostMessage(buffer, response.HostMessage)))
                {
                    string[] strArray = str.Split(new char[] { ' ' });
                    if (strArray.Length < 2)
                    {
                        throw new Pop3Exception("Invalid line in multiline response:  " + str);
                    }
                    list.Add(new Pop3ListItem(Convert.ToInt32(strArray[0]), Convert.ToInt64(strArray[1])));
                }
            }
            else
            {
                list = new List<Pop3ListItem>(1);
                string[] strArray3 = response.HostMessage.Split(new char[] { ' ' });
                if (strArray3.Length < 3)
                {
                    throw new Pop3Exception("Invalid response message: " + response.HostMessage);
                }
                list.Add(new Pop3ListItem(Convert.ToInt32(strArray3[1]), Convert.ToInt64(strArray3[2])));
            }
            return new ListResponse(response, list);
        }
    }
}

