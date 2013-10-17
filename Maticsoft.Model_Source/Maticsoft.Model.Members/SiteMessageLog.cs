namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class SiteMessageLog
    {
        private string _ext1;
        private string _ext2;
        private int _id;
        private int? _messageid;
        private string _messagestate;
        private string _messagetype;
        private int? _receiverid;
        private string _receiverusername;

        public string Ext1
        {
            get
            {
                return this._ext1;
            }
            set
            {
                this._ext1 = value;
            }
        }

        public string Ext2
        {
            get
            {
                return this._ext2;
            }
            set
            {
                this._ext2 = value;
            }
        }

        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int? MessageID
        {
            get
            {
                return this._messageid;
            }
            set
            {
                this._messageid = value;
            }
        }

        public string MessageState
        {
            get
            {
                return this._messagestate;
            }
            set
            {
                this._messagestate = value;
            }
        }

        public string MessageType
        {
            get
            {
                return this._messagetype;
            }
            set
            {
                this._messagetype = value;
            }
        }

        public int? ReceiverID
        {
            get
            {
                return this._receiverid;
            }
            set
            {
                this._receiverid = value;
            }
        }

        public string ReceiverUserName
        {
            get
            {
                return this._receiverusername;
            }
            set
            {
                this._receiverusername = value;
            }
        }
    }
}

