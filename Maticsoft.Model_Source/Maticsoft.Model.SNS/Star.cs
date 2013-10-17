namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Star
    {
        private string _applyreason;
        private DateTime _createddate;
        private DateTime? _expireddate;
        private int _id;
        private string _nickname;
        private int? _status;
        private int _typeid;
        private string _usergravatar;
        private int _userid;

        public string ApplyReason
        {
            get
            {
                return this._applyreason;
            }
            set
            {
                this._applyreason = value;
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

        public DateTime? ExpiredDate
        {
            get
            {
                return this._expireddate;
            }
            set
            {
                this._expireddate = value;
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

        public string NickName
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
            }
        }

        public int? Status
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

        public int TypeID
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

        public string UserGravatar
        {
            get
            {
                return this._usergravatar;
            }
            set
            {
                this._usergravatar = value;
            }
        }

        public int UserID
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
    }
}

