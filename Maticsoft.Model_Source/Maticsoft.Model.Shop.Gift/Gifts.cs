namespace Maticsoft.Model.Shop.Gift
{
    using System;

    [Serializable]
    public class Gifts
    {
        private int _categoryid;
        private decimal? _costprice;
        private DateTime _createdate;
        private bool _enabled;
        private int _giftid;
        private string _infocusimageurl;
        private string _longdescription;
        private decimal? _marketprice;
        private string _meta_description;
        private string _meta_keywords;
        private string _name;
        private int _needgrade;
        private int _needpoint;
        private int _salecounts;
        private decimal? _saleprice;
        private string _shortdescription;
        private int? _stock;
        private string _thumbnailsurl;
        private string _title;
        private string _unit;
        private int _weight;

        public int CategoryID
        {
            get
            {
                return this._categoryid;
            }
            set
            {
                this._categoryid = value;
            }
        }

        public decimal? CostPrice
        {
            get
            {
                return this._costprice;
            }
            set
            {
                this._costprice = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return this._createdate;
            }
            set
            {
                this._createdate = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                this._enabled = value;
            }
        }

        public int GiftId
        {
            get
            {
                return this._giftid;
            }
            set
            {
                this._giftid = value;
            }
        }

        public string InFocusImageUrl
        {
            get
            {
                return this._infocusimageurl;
            }
            set
            {
                this._infocusimageurl = value;
            }
        }

        public string LongDescription
        {
            get
            {
                return this._longdescription;
            }
            set
            {
                this._longdescription = value;
            }
        }

        public decimal? MarketPrice
        {
            get
            {
                return this._marketprice;
            }
            set
            {
                this._marketprice = value;
            }
        }

        public string Meta_Description
        {
            get
            {
                return this._meta_description;
            }
            set
            {
                this._meta_description = value;
            }
        }

        public string Meta_Keywords
        {
            get
            {
                return this._meta_keywords;
            }
            set
            {
                this._meta_keywords = value;
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

        public int NeedGrade
        {
            get
            {
                return this._needgrade;
            }
            set
            {
                this._needgrade = value;
            }
        }

        public int NeedPoint
        {
            get
            {
                return this._needpoint;
            }
            set
            {
                this._needpoint = value;
            }
        }

        public int SaleCounts
        {
            get
            {
                return this._salecounts;
            }
            set
            {
                this._salecounts = value;
            }
        }

        public decimal? SalePrice
        {
            get
            {
                return this._saleprice;
            }
            set
            {
                this._saleprice = value;
            }
        }

        public string ShortDescription
        {
            get
            {
                return this._shortdescription;
            }
            set
            {
                this._shortdescription = value;
            }
        }

        public int? Stock
        {
            get
            {
                return this._stock;
            }
            set
            {
                this._stock = value;
            }
        }

        public string ThumbnailsUrl
        {
            get
            {
                return this._thumbnailsurl;
            }
            set
            {
                this._thumbnailsurl = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }

        public int Weight
        {
            get
            {
                return this._weight;
            }
            set
            {
                this._weight = value;
            }
        }
    }
}

