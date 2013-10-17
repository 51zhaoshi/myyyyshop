namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class UserShip
    {
        private int _activeuserid;
        private int _categoryid;
        private DateTime _createddate = DateTime.Now;
        private bool _isfellew;
        private bool _isread;
        private int _passiveuserid;
        private int _state;
        private int _type;
        private int fanscount;
        private bool isBothway;
        private string nickname;
        private string singature;

        public int ActiveUserID
        {
            get
            {
                return this._activeuserid;
            }
            set
            {
                this._activeuserid = value;
            }
        }

        public int CategoryID
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
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

        public int FansCount
        {
            get
            {
                return this.fanscount;
            }
            set
            {
                this.fanscount = value;
            }
        }

        public bool IsBothway
        {
            get
            {
                return this.isBothway;
            }
            set
            {
                this.isBothway = value;
            }
        }

        public bool IsFellow
        {
            get
            {
                return this._isfellew;
            }
            set
            {
                this._isfellew = value;
            }
        }

        public bool IsRead
        {
            get
            {
                return this._isread;
            }
            set
            {
                this._isread = value;
            }
        }

        public string NickName
        {
            get
            {
                return this.nickname;
            }
            set
            {
                this.nickname = value;
            }
        }

        public int PassiveUserID
        {
            get
            {
                return this._passiveuserid;
            }
            set
            {
                this._passiveuserid = value;
            }
        }

        public string Singature
        {
            get
            {
                return this.singature;
            }
            set
            {
                this.singature = value;
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

        public int Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

