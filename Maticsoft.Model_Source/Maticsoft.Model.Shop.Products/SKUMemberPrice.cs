namespace Maticsoft.Model.Shop.Products
{
    using System;

    [Serializable]
    public class SKUMemberPrice
    {
        private int _gradeid;
        private decimal _membersaleprice;
        private long _skuid;

        public int GradeId
        {
            get
            {
                return this._gradeid;
            }
            set
            {
                this._gradeid = value;
            }
        }

        public decimal MemberSalePrice
        {
            get
            {
                return this._membersaleprice;
            }
            set
            {
                this._membersaleprice = value;
            }
        }

        public long SkuId
        {
            get
            {
                return this._skuid;
            }
            set
            {
                this._skuid = value;
            }
        }
    }
}

