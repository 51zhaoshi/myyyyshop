namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class ReferUsers
    {
        private DateTime _createddate;
        private int _id;
        private bool _isread;
        private string _refernickname;
        private int _referuserid;
        private int _tagetid;
        private int _type;

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

        public string ReferNickName
        {
            get
            {
                return this._refernickname;
            }
            set
            {
                this._refernickname = value;
            }
        }

        public int ReferUserID
        {
            get
            {
                return this._referuserid;
            }
            set
            {
                this._referuserid = value;
            }
        }

        public int TagetID
        {
            get
            {
                return this._tagetid;
            }
            set
            {
                this._tagetid = value;
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

