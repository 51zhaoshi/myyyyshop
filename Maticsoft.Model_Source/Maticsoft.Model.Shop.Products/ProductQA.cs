namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductQA
    {
        private DateTime? _createddate;
        private int? _parentid;
        private int _productid;
        private int _qaid;
        private string _question;
        private string _replycontent;
        private DateTime? _replydate;
        private int? _replyuserid;
        private string _replyusername;
        private int _state;
        private int _userid;
        private string _username;

        public DateTime? CreatedDate
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

        public int? ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public int ProductId
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

        public int QAId
        {
            get
            {
                return this._qaid;
            }
            set
            {
                this._qaid = value;
            }
        }

        public string Question
        {
            get
            {
                return this._question;
            }
            set
            {
                this._question = value;
            }
        }

        public string ReplyContent
        {
            get
            {
                return this._replycontent;
            }
            set
            {
                this._replycontent = value;
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

        public int? ReplyUserId
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

        public int State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
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

