namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class LineDistributor
    {
        private int _distributorid;
        private int _lineid;

        public int DistributorId
        {
            get
            {
                return this._distributorid;
            }
            set
            {
                this._distributorid = value;
            }
        }

        public int LineId
        {
            get
            {
                return this._lineid;
            }
            set
            {
                this._lineid = value;
            }
        }
    }
}

