namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class Distributor
    {
        private int _distributorid;
        private string _distributorname;

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

        public string DistributorName
        {
            get
            {
                return this._distributorname;
            }
            set
            {
                this._distributorname = value;
            }
        }
    }
}

