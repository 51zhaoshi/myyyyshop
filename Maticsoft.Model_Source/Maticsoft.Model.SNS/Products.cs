namespace Maticsoft.Model.SNS
{
    using System;

    [Serializable]
    public class Products
    {
        private int? _categoryid;
        private string _color;
        private int _commentcount;
        private DateTime _createddate;
        private string _creatednickname;
        private int _createuserid;
        private int _favouritecount;
        private int? _forwardedcount = 0;
        private int? _groupbuycount = 0;
        private int _isrecomend;
        private string _normalimageurl;
        private long? _originalid;
        private int? _ownerproductid;
        private decimal? _price;
        private string _productdescription;
        private long _productid;
        private string _productname;
        private int? _productsourceid;
        private string _producturl;
        private int _pvcount;
        private int _sequence;
        private string _sharedescription;
        private int _skipcount;
        private int? _sourcetype;
        private string _staticurl;
        private int _status;
        private string _tags;
        private string _thumbimageurl;
        private string _topcommentsid;

        public int? CategoryID
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

        public string Color
        {
            get
            {
                return this._color;
            }
            set
            {
                this._color = value;
            }
        }

        public int CommentCount
        {
            get
            {
                return this._commentcount;
            }
            set
            {
                this._commentcount = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return this._createddate;
            }
            set
            {
                this._createddate = value;
            }
        }

        public string CreatedNickName
        {
            get
            {
                return this._creatednickname;
            }
            set
            {
                this._creatednickname = value;
            }
        }

        public int CreateUserID
        {
            get
            {
                return this._createuserid;
            }
            set
            {
                this._createuserid = value;
            }
        }

        public int FavouriteCount
        {
            get
            {
                return this._favouritecount;
            }
            set
            {
                this._favouritecount = value;
            }
        }

        public int? ForwardedCount
        {
            get
            {
                return this._forwardedcount;
            }
            set
            {
                this._forwardedcount = value;
            }
        }

        public int? GroupBuyCount
        {
            get
            {
                return this._groupbuycount;
            }
            set
            {
                this._groupbuycount = value;
            }
        }

        public int IsRecomend
        {
            get
            {
                return this._isrecomend;
            }
            set
            {
                this._isrecomend = value;
            }
        }

        public string NormalImageUrl
        {
            get
            {
                return this._normalimageurl;
            }
            set
            {
                this._normalimageurl = value;
            }
        }

        public long? OriginalID
        {
            get
            {
                return this._originalid;
            }
            set
            {
                this._originalid = value;
            }
        }

        public int? OwnerProductId
        {
            get
            {
                return this._ownerproductid;
            }
            set
            {
                this._ownerproductid = value;
            }
        }

        public decimal? Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string ProductDescription
        {
            get
            {
                return this._productdescription;
            }
            set
            {
                this._productdescription = value;
            }
        }

        public long ProductID
        {
            get
            {
                return this._productid;
            }
            set
            {
                this._productid = value;
            }
        }

        public string ProductName
        {
            get
            {
                return this._productname;
            }
            set
            {
                this._productname = value;
            }
        }

        public int? ProductSourceID
        {
            get
            {
                return this._productsourceid;
            }
            set
            {
                this._productsourceid = value;
            }
        }

        public string ProductUrl
        {
            get
            {
                return this._producturl;
            }
            set
            {
                this._producturl = value;
            }
        }

        public int PVCount
        {
            get
            {
                return this._pvcount;
            }
            set
            {
                this._pvcount = value;
            }
        }

        public int Sequence
        {
            get
            {
                return this._sequence;
            }
            set
            {
                this._sequence = value;
            }
        }

        public string ShareDescription
        {
            get
            {
                return this._sharedescription;
            }
            set
            {
                this._sharedescription = value;
            }
        }

        public int SkipCount
        {
            get
            {
                return this._skipcount;
            }
            set
            {
                this._skipcount = value;
            }
        }

        public int? SourceType
        {
            get
            {
                return this._sourcetype;
            }
            set
            {
                this._sourcetype = value;
            }
        }

        public string StaticUrl
        {
            get
            {
                return this._staticurl;
            }
            set
            {
                this._staticurl = value;
            }
        }

        public int Status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;
            }
        }

        public string Tags
        {
            get
            {
                return this._tags;
            }
            set
            {
                this._tags = value;
            }
        }

        public string ThumbImageUrl
        {
            get
            {
                return this._thumbimageurl;
            }
            set
            {
                this._thumbimageurl = value;
            }
        }

        public string TopCommentsId
        {
            get
            {
                return this._topcommentsid;
            }
            set
            {
                this._topcommentsid = value;
            }
        }
    }
}

