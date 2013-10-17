namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class OnLineShopPhoto
    {
        private DateTime _createddate;
        private string _creatednickname;
        private int _createduserid;
        private int _photoid;
        private int _productid;
        private int _status;

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

        public int CreatedUserId
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

        public int PhotoID
        {
            get
            {
                return this._photoid;
            }
            set
            {
                this._photoid = value;
            }
        }

        public int ProductID
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
    }
}

