namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;

    public class ViewStar : Maticsoft.Model.SNS.Star
    {
        private int _fansCount;
        private int _favouritesCount;
        private bool _isFellow;
        private bool _isUserDPI;
        private int _productsCount;
        private string _singature;

        public ViewStar(Maticsoft.Model.SNS.Star sr)
        {
            base.CreatedDate = sr.CreatedDate;
            base.ID = sr.ID;
            base.ExpiredDate = sr.ExpiredDate;
            base.ApplyReason = sr.ApplyReason;
            base.NickName = sr.NickName;
            base.Status = sr.Status;
            base.TypeID = sr.TypeID;
            base.UserID = sr.UserID;
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

        public bool IsUserDPI
        {
            get
            {
                return this._isUserDPI;
            }
            set
            {
                this._isUserDPI = value;
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

