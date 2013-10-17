namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.Members;
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TargetDetail
    {
        private int _commentcount;
        private List<Comments> _commentlist;
        private DateTime _createddate;
        private int _favouritecount;
        private string _imageurl;
        private int _isrecommand;
        private string _nickname;
        private decimal _price;
        private string _producturl;
        private int _pvcount;
        private List<Photos> _recommandphoto;
        private List<Maticsoft.Model.SNS.Products> _recommandproduct;
        private string _sharedes;
        private string _tags;
        private int _targetid;
        private string _targetname;
        private string _thumbimageurl;
        private string _type;
        private int _userid;
        public List<string> AlumsCoverList = new List<string>();
        public Dictionary<long, IEnumerable<Comments>> DicCommentList = new Dictionary<long, IEnumerable<Comments>>();
        private Photos photo;
        public List<PhotoTags> PhotoTagList = new List<PhotoTags>();
        private Maticsoft.Model.SNS.Products product;
        public List<Maticsoft.Model.SNS.Tags> ProductTagList = new List<Maticsoft.Model.SNS.Tags>();
        public UserAlbums UserAlums = new UserAlbums();
        public UsersExpModel UserModel = new UsersExpModel();

        public void SetDicComment(long ProductId, IEnumerable<Comments> list)
        {
            this.DicCommentList.Add(ProductId, list);
        }

        public int Commentcount
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

        public List<Comments> CommentList
        {
            get
            {
                return this._commentlist;
            }
            set
            {
                Action<Maticsoft.Model.SNS.Products> action = null;
                this._commentlist = value;
                if (((this._commentlist != null) && (this._commentlist.Count > 0)) && ((this.RecommandProduct != null) && (this.RecommandProduct.Count > 0)))
                {
                    if (action == null)
                    {
                        action = delegate (Maticsoft.Model.SNS.Products item) {
                            this.SetDicComment(item.ProductID, from comment in this._commentlist
                                where comment.TargetId == item.ProductID
                                select comment);
                        };
                    }
                    this.RecommandProduct.ForEach(action);
                }
            }
        }

        public int CommentPageSize { get; set; }

        public List<string> CovorImageList { get; set; }

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

        public int FavCount { get; set; }

        public int Favouritecount
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

        public List<UserFavourite> FavUserList { get; set; }

        public string Imageurl
        {
            get
            {
                return this._imageurl;
            }
            set
            {
                this._imageurl = value;
            }
        }

        public int IsRecommand
        {
            get
            {
                return this._isrecommand;
            }
            set
            {
                this._isrecommand = value;
            }
        }

        public string Nickname
        {
            get
            {
                return this._nickname;
            }
            set
            {
                this._nickname = value;
            }
        }

        public Photos Photo
        {
            get
            {
                return this.photo;
            }
            set
            {
                this.photo = value;
                if (this.photo != null)
                {
                    this.TargetId = this.photo.PhotoID;
                    this.Type = "Photo";
                    this.Userid = this.photo.CreatedUserID;
                    this.Nickname = this.photo.CreatedNickName;
                    this.Commentcount = this.photo.CommentCount;
                    this.CreatedDate = this.photo.CreatedDate;
                    this.Favouritecount = this.photo.FavouriteCount;
                    this.Sharedes = this.photo.Description;
                    this._targetname = this.photo.PhotoName;
                    this._thumbimageurl = this.photo.PhotoUrl;
                    this.PvCount = this.photo.PVCount;
                    this.Imageurl = this.photo.PhotoUrl;
                    this.IsRecommand = this.photo.IsRecomend;
                    this.Tags = this.photo.Tags;
                }
            }
        }

        public decimal Price
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

        public Maticsoft.Model.SNS.Products Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
                if (this.product != null)
                {
                    this.TargetId = Convert.ToInt32(this.product.ProductID);
                    this.Type = "Product";
                    this.Userid = this.product.CreateUserID;
                    this.Nickname = this.product.CreatedNickName;
                    this.Commentcount = this.product.CommentCount;
                    this.CreatedDate = this.product.CreatedDate;
                    this.Favouritecount = this.product.FavouriteCount;
                    this.Sharedes = this.product.ShareDescription;
                    this._targetname = this.product.ProductName;
                    this._thumbimageurl = this.product.NormalImageUrl;
                    this.ProductUrl = this.product.ProductUrl;
                    this.Price = this.Product.Price.HasValue ? this.Product.Price.Value : 0M;
                    this.PvCount = this.Product.PVCount;
                    this.IsRecommand = this.product.IsRecomend;
                    this.Tags = this.product.Tags;
                }
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

        public int PvCount
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

        public List<Photos> RecommandPhoto
        {
            get
            {
                return this._recommandphoto;
            }
            set
            {
                List<Photos>[] listPhoto;
                int index;
                this._recommandphoto = value;
                if ((value != null) && (value.Count >= 1))
                {
                    listPhoto = new List<Photos>[] { new List<Photos>(), new List<Photos>(), new List<Photos>() };
                    index = 0;
                    value.ForEach(delegate (Photos image) {
                        if (index == 3)
                        {
                            index = 0;
                        }
                        listPhoto[index++].Add(image);
                    });
                    this.RecommandPhoto4ThreeCol = listPhoto;
                }
            }
        }

        public List<Photos>[] RecommandPhoto4ThreeCol { get; set; }

        public List<Maticsoft.Model.SNS.Products> RecommandProduct
        {
            get
            {
                return this._recommandproduct;
            }
            set
            {
                List<Maticsoft.Model.SNS.Products>[] list;
                int index;
                this._recommandproduct = value;
                if ((value != null) && (value.Count >= 1))
                {
                    list = new List<Maticsoft.Model.SNS.Products>[] { new List<Maticsoft.Model.SNS.Products>(), new List<Maticsoft.Model.SNS.Products>(), new List<Maticsoft.Model.SNS.Products>() };
                    index = 0;
                    value.ForEach(delegate (Maticsoft.Model.SNS.Products image) {
                        if (index == 3)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                    this.RecommandProduct4ThreeCol = list;
                }
            }
        }

        public List<Maticsoft.Model.SNS.Products>[] RecommandProduct4ThreeCol { get; set; }

        public string Sharedes
        {
            get
            {
                return this._sharedes;
            }
            set
            {
                this._sharedes = value;
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

        public int TargetId
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
            }
        }

        public string Targetname
        {
            get
            {
                return this._targetname;
            }
            set
            {
                this._targetname = value;
            }
        }

        public string Thumbimageurl
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

        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public int Userid
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }
    }
}

