namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;

    public class StarRank : Maticsoft.Model.SNS.StarRank
    {
        private int _fansCount;
        private int _favouritesCount;
        private bool _isFellow;
        private int _productsCount;
        private string _singature;

        public StarRank(Maticsoft.Model.SNS.StarRank sr)
        {
            base.CreatedDate = sr.CreatedDate;
            base.ID = sr.ID;
            base.EndDate = sr.EndDate;
            base.IsRecommend = sr.IsRecommend;
            base.NickName = sr.NickName;
            base.RankDate = sr.RankDate;
            base.Sequence = sr.Sequence;
            base.StartDate = sr.StartDate;
            base.Status = sr.Status;
            base.TimeUnit = sr.TimeUnit;
            base.TypeId = sr.TypeId;
            base.UserId = sr.UserId;
            base.UserGravatar = sr.UserGravatar;
        }

        public int FansCount
        {
            get
            {
                return this._fansCount;
            }
            set
            {
                this._fansCount = value;
            }
        }

        public int FavouritesCount
        {
            get
            {
                return this._favouritesCount;
            }
            set
            {
                this._favouritesCount = value;
            }
        }

        public bool IsFellow
        {
            get
            {
                return this._isFellow;
            }
            set
            {
                this._isFellow = value;
            }
        }

        public int ProductsCount
        {
            get
            {
                return this._productsCount;
            }
            set
            {
                this._productsCount = value;
            }
        }

        public string Singature
        {
            get
            {
                return this._singature;
            }
            set
            {
                this._singature = value;
            }
        }
    }
}

