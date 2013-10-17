namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductConsults
    {
        private int _consultationid;
        private string _consultationtext;
        private DateTime _createddate;
        private bool _isreply;
        private long _productid;
        private int _recomend;
        private DateTime? _replydate;
        private string _replytext;
        private int _replyuserid;
        private string _replyusername;
        private int _status;
        private int _typeid = -1;
        private string _useremail;
        private int _userid;
        private string _username;

        public int ConsultationId
        {
            get
            {
                return this._consultationid;
            }
            set
            {
                this._consultationid = value;
            }
        }

        public string ConsultationText
        {
            get
            {
                return this._consultationtext;
            }
            set
            {
                this._consultationtext = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public bool IsReply
        {
            get
            {
                return this._isreply;
            }
            set
            {
                this._isreply = value;
            }
        }

        public long ProductId
        {
            get
            {
                return this._productid;
            }
            set
            {
                this._productid = value;
            }
        }

        public int Recomend
        {
            get
            {
                return this._recomend;
            }
            set
            {
                this._recomend = value;
            }
        }

        public DateTime? ReplyDate
        {
            get
            {
                return this._replydate;
            }
            set
            {
                this._replydate = value;
            }
        }

        public string ReplyText
        {
            get
            {
                return this._replytext;
            }
            set
            {
                this._replytext = value;
            }
        }

        public int ReplyUserId
        {
            get
            {
                return this._replyuserid;
            }
            set
            {
                this._replyuserid = value;
            }
        }

        public string ReplyUserName
        {
            get
            {
                return this._replyusername;
            }
            set
            {
                this._replyusername = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return this._useremail;
            }
            set
            {
                this._useremail = value;
            }
        }

        public int UserId
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string UserName
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
    }
}

