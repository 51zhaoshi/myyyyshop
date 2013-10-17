namespace Maticsoft.Model.Shop.Coupon
{
    using System;

    [Serializable]
    public class CouponClass
    {
        private int _classid;
        private string _name;
        private int _sequence;
        private int _status = 1;

        public int ClassId
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
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
    }
}

