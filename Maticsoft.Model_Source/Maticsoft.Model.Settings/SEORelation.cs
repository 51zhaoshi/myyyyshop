namespace Maticsoft.Model.Settings
{
    using System;

    [Serializable]
    public class SEORelation
    {
        private DateTime? _createddate = new DateTime?(DateTime.Now);
        private bool _isactive;
        private bool _iscms;
        private bool _iscomment;
        private bool _isshop;
        private bool _issns;
        private string _keyname;
        private string _linkurl;
        private int _relationid;

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

        public bool IsActive
        {
            get
            {
                return this._isactive;
            }
            set
            {
                this._isactive = value;
            }
        }

        public bool IsCMS
        {
            get
            {
                return this._iscms;
            }
            set
            {
                this._iscms = value;
            }
        }

        public bool IsComment
        {
            get
            {
                return this._iscomment;
            }
            set
            {
                this._iscomment = value;
            }
        }

        public bool IsShop
        {
            get
            {
                return this._isshop;
            }
            set
            {
                this._isshop = value;
            }
        }

        public bool IsSNS
        {
            get
            {
                return this._issns;
            }
            set
            {
                this._issns = value;
            }
        }

        public string KeyName
        {
            get
            {
                return this._keyname;
            }
            set
            {
                this._keyname = value;
            }
        }

        public string LinkURL
        {
            get
            {
                return this._linkurl;
            }
            set
            {
                this._linkurl = value;
            }
        }

        public int RelationID
        {
            get
            {
                return this._relationid;
            }
            set
            {
                this._relationid = value;
            }
        }
    }
}

