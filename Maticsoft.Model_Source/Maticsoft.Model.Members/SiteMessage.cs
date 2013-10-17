namespace Maticsoft.Model.Members
{
    using System;

    [Serializable]
    public class SiteMessage
    {
        private string _content;
        private string _ext1;
        private string _ext2;
        private int _id;
        private string _msgtype;
        private bool _readerisdel;
        private DateTime? _readtime;
        private int? _receiverid;
        private bool _receiverisread;
        private string _receiverusername;
        private int? _senderid;
        private bool _senderisdel;
        private string _senderusername;
        private DateTime? _sendtime;
        private string _title;

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

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

        public string MsgType
        {
            get
            {
                return this._msgtype;
            }
            set
            {
                this._msgtype = value;
            }
        }

        public bool ReaderIsDel
        {
            get
            {
                return this._readerisdel;
            }
            set
            {
                this._readerisdel = value;
            }
        }

        public DateTime? ReadTime
        {
            get
            {
                return this._readtime;
            }
            set
            {
                this._readtime = value;
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

        public bool ReceiverIsRead
        {
            get
            {
                return this._receiverisread;
            }
            set
            {
                this._receiverisread = value;
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

        public int? SenderID
        {
            get
            {
                return this._senderid;
            }
            set
            {
                this._senderid = value;
            }
        }

        public bool SenderIsDel
        {
            get
            {
                return this._senderisdel;
            }
            set
            {
                this._senderisdel = value;
            }
        }

        public string SenderUserName
        {
            get
            {
                return this._senderusername;
            }
            set
            {
                this._senderusername = value;
            }
        }

        public DateTime? SendTime
        {
            get
            {
                return this._sendtime;
            }
            set
            {
                this._sendtime = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
    }
}

