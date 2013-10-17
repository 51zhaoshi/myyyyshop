namespace Maticsoft.Model.SysManage
{
    using System;

    [Serializable]
    public class VerifyMail
    {
        private DateTime _createddate;
        private string _keyvalue;
        private int _status;
        private string _username;
        private int? _validitytype;

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

        public string KeyValue
        {
            get
            {
                return this._keyvalue;
            }
            set
            {
                this._keyvalue = value;
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

        public int? ValidityType
        {
            get
            {
                return this._validitytype;
            }
            set
            {
                this._validitytype = value;
            }
        }
    }
}

