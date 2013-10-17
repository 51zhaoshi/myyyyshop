namespace Maticsoft.Model.SNS
{
    using Maticsoft.Model.Members;
    using System;

    [Serializable]
    public class GroupUsers
    {
        private string _applyreason;
        private int _groupid;
        private bool _isrecommend;
        private DateTime _jointime;
        private string _nickname;
        private int _role;
        private int _sequence;
        private int _status;
        private int _userid;
        public UsersExpModel User;

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

        public int GroupID
        {
            get
            {
                return this._groupid;
            }
            set
            {
                this._groupid = value;
            }
        }

        public bool IsRecommend
        {
            get
            {
                return this._isrecommend;
            }
            set
            {
                this._isrecommend = value;
            }
        }

        public DateTime JoinTime
        {
            get
            {
                return this._jointime;
            }
            set
            {
                this._jointime = value;
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

        public int Role
        {
            get
            {
                return this._role;
            }
            set
            {
                this._role = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
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

