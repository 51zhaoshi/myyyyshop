namespace Maticsoft.Model.Shop.Products
{
    using System;

    public class ProductCompareServer
    {
        private string _attrName;
        private string _product1;
        private string _product2;
        private string _product3;
        private string _product4;

        public string AttrName
        {
            get
            {
                return this._attrName;
            }
            set
            {
                this._attrName = value;
            }
        }

        public string Product1
        {
            get
            {
                return this._product1;
            }
            set
            {
                this._product1 = value;
            }
        }

        public string Product2
        {
            get
            {
                return this._product2;
            }
            set
            {
                this._product2 = value;
            }
        }

        public string Product3
        {
            get
            {
                return this._product3;
            }
            set
            {
                this._product3 = value;
            }
        }

        public string Product4
        {
            get
            {
                return this._product4;
            }
            set
            {
                this._product4 = value;
            }
        }
    }
}

