namespace Maticsoft.Model.Shop.Supplier
{
    using System;

    [Serializable]
    public class SupplierRankThemes
    {
        private int _rankid;
        private int _themeid;

        public int RankId
        {
            get
            {
                return this._rankid;
            }
            set
            {
                this._rankid = value;
            }
        }

        public int ThemeId
        {
            get
            {
                return this._themeid;
            }
            set
            {
                this._themeid = value;
            }
        }
    }
}

