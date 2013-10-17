namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class ProductTypeBrand
    {
        private int _brandid;
        private int _producttypeid;

        public int BrandId
        {
            get
            {
                return this._brandid;
            }
            set
            {
                this._brandid = value;
            }
        }

        public int ProductTypeId
        {
            get
            {
                return this._producttypeid;
            }
            set
            {
                this._producttypeid = value;
            }
        }
    }
}

