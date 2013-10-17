namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductLine
    {
        private int _lineid;
        private string _linename;

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

        public string LineName
        {
            get
            {
                return this._linename;
            }
            set
            {
                this._linename = value;
            }
        }
    }
}

