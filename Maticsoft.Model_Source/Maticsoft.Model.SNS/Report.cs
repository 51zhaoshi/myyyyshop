namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Report
    {
        private DateTime _createddate;
        private string _creatednickname;
        private int _createduserid;
        private string _description;
        private int _id;
        private int _reporttypeid;
        private int? _status;
        private int _targetid;
        private int _targettype;

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

        public string CreatedNickName
        {
            get
            {
                return this._creatednickname;
            }
            set
            {
                this._creatednickname = value;
            }
        }

        public int CreatedUserID
        {
            get
            {
                return this._createduserid;
            }
            set
            {
                this._createduserid = value;
            }
        }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
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

        public int ReportTypeID
        {
            get
            {
                return this._reporttypeid;
            }
            set
            {
                this._reporttypeid = value;
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

        public int TargetID
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
            }
        }

        public int TargetType
        {
            get
            {
                return this._targettype;
            }
            set
            {
                this._targettype = value;
            }
        }
    }
}

