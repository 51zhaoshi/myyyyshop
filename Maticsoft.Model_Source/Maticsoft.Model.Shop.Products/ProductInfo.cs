namespace Maticsoft.Model.Shop.Products
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ProductInfo
    {
        private DateTime _addeddate;
        private int _brandid;
        private int _categoryid;
        private string _description;
        private int _displaysequence;
        private string _extendcategorypath;
        private bool _hassku;
        private string _imageurl;
        private int _lineid;
        private decimal _lowestsaleprice;
        private string _maincategorypath;
        private decimal? _marketprice;
        private int? _maxquantity;
        private string _meta_description;
        private string _meta_keywords;
        private string _meta_title;
        private int? _minquantity;
        private List<int> _packageId;
        private int _penetrationstatus;
        private decimal? _points;
        private string[] _product_Categories;
        private string _productcode;
        private long _productid;
        private string _productname;
        private int? _regionid;
        private string[] _relatedProductId;
        private int _salecounts;
        private int _salestatus;
        private string _searchProductCategories;
        private string _seoimagealt;
        private string _seoimagetitle;
        private string _seourl;
        private string _shortdescription;
        private string _staticurl;
        private int _supplierid;
        private string _tags;
        private string _thumbnailurl1;
        private string _thumbnailurl2;
        private string _thumbnailurl3;
        private string _thumbnailurl4;
        private string _thumbnailurl5;
        private string _thumbnailurl6;
        private string _thumbnailurl7;
        private string _thumbnailurl8;
        private int? _typeid;
        private string _unit;
        private int _visticounts;
        private List<AttributeInfo> attributeInfoList = new List<AttributeInfo>();
        private List<ProductAccessorie> productAccessorieList = new List<ProductAccessorie>();
        private List<ProductImage> productImageList = new List<ProductImage>();
        private List<RelatedProduct> relatedProductList = new List<RelatedProduct>();
        private List<SKUInfo> skuInfoList = new List<SKUInfo>();

        public DateTime AddedDate
        {
            get
            {
                return this._addeddate;
            }
            set
            {
                this._addeddate = value;
            }
        }

        public List<AttributeInfo> AttributeInfos
        {
            get
            {
                return this.attributeInfoList;
            }
            set
            {
                this.attributeInfoList = value;
            }
        }

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

        public int CategoryId
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

        public string CategoryName { get; set; }

        public decimal CountDownId { get; set; }

        public string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }

        public int DisplaySequence
        {
            get
            {
                return this._displaysequence;
            }
            set
            {
                this._displaysequence = value;
            }
        }

        public string ExtendCategoryPath
        {
            get
            {
                return this._extendcategorypath;
            }
            set
            {
                this._extendcategorypath = value;
            }
        }

        public bool HasSKU
        {
            get
            {
                return this._hassku;
            }
            set
            {
                this._hassku = value;
            }
        }

        public string ImageUrl
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

        public bool isHot { get; set; }

        public bool isLowPrice { get; set; }

        public bool isNow { get; set; }

        public bool isRec { get; set; }

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

        public decimal LowestSalePrice
        {
            get
            {
                return this._lowestsaleprice;
            }
            set
            {
                this._lowestsaleprice = value;
            }
        }

        public string MainCategoryPath
        {
            get
            {
                return this._maincategorypath;
            }
            set
            {
                this._maincategorypath = value;
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

        public int? MaxQuantity
        {
            get
            {
                return this._maxquantity;
            }
            set
            {
                this._maxquantity = value;
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

        public string Meta_Title
        {
            get
            {
                return this._meta_title;
            }
            set
            {
                this._meta_title = value;
            }
        }

        public int? MinQuantity
        {
            get
            {
                return this._minquantity;
            }
            set
            {
                this._minquantity = value;
            }
        }

        public List<int> PackageId
        {
            get
            {
                return this._packageId;
            }
            set
            {
                this._packageId = value;
            }
        }

        public int PenetrationStatus
        {
            get
            {
                return this._penetrationstatus;
            }
            set
            {
                this._penetrationstatus = value;
            }
        }

        public decimal? Points
        {
            get
            {
                return this._points;
            }
            set
            {
                this._points = value;
            }
        }

        public string[] Product_Categories
        {
            get
            {
                return this._product_Categories;
            }
            set
            {
                this._product_Categories = value;
            }
        }

        public List<ProductAccessorie> ProductAccessories
        {
            get
            {
                return this.productAccessorieList;
            }
            set
            {
                this.productAccessorieList = value;
            }
        }

        public string ProductCode
        {
            get
            {
                return this._productcode;
            }
            set
            {
                this._productcode = value;
            }
        }

        public long ProductId
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

        public List<ProductImage> ProductImages
        {
            get
            {
                return this.productImageList;
            }
            set
            {
                this.productImageList = value;
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

        public DateTime ProSalesEndDate { get; set; }

        public decimal ProSalesPrice { get; set; }

        public int? RegionId
        {
            get
            {
                return this._regionid;
            }
            set
            {
                this._regionid = value;
            }
        }

        public string[] RelatedProductId
        {
            get
            {
                return this._relatedProductId;
            }
            set
            {
                this._relatedProductId = value;
            }
        }

        public List<RelatedProduct> RelatedProducts
        {
            get
            {
                return this.relatedProductList;
            }
            set
            {
                this.relatedProductList = value;
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

        public decimal SalePrice { get; set; }

        public int SaleStatus
        {
            get
            {
                return this._salestatus;
            }
            set
            {
                this._salestatus = value;
            }
        }

        public string SearchProductCategories
        {
            get
            {
                return this._searchProductCategories;
            }
            set
            {
                this._searchProductCategories = value;
            }
        }

        public string SeoImageAlt
        {
            get
            {
                return this._seoimagealt;
            }
            set
            {
                this._seoimagealt = value;
            }
        }

        public string SeoImageTitle
        {
            get
            {
                return this._seoimagetitle;
            }
            set
            {
                this._seoimagetitle = value;
            }
        }

        public string SeoUrl
        {
            get
            {
                return this._seourl;
            }
            set
            {
                this._seourl = value;
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

        public List<SKUInfo> SkuInfos
        {
            get
            {
                return this.skuInfoList;
            }
            set
            {
                this.skuInfoList = value;
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

        public int SupplierId
        {
            get
            {
                return this._supplierid;
            }
            set
            {
                this._supplierid = value;
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

        public string ThumbnailUrl1
        {
            get
            {
                return this._thumbnailurl1;
            }
            set
            {
                this._thumbnailurl1 = value;
            }
        }

        public string ThumbnailUrl2
        {
            get
            {
                return this._thumbnailurl2;
            }
            set
            {
                this._thumbnailurl2 = value;
            }
        }

        public string ThumbnailUrl3
        {
            get
            {
                return this._thumbnailurl3;
            }
            set
            {
                this._thumbnailurl3 = value;
            }
        }

        public string ThumbnailUrl4
        {
            get
            {
                return this._thumbnailurl4;
            }
            set
            {
                this._thumbnailurl4 = value;
            }
        }

        public string ThumbnailUrl5
        {
            get
            {
                return this._thumbnailurl5;
            }
            set
            {
                this._thumbnailurl5 = value;
            }
        }

        public string ThumbnailUrl6
        {
            get
            {
                return this._thumbnailurl6;
            }
            set
            {
                this._thumbnailurl6 = value;
            }
        }

        public string ThumbnailUrl7
        {
            get
            {
                return this._thumbnailurl7;
            }
            set
            {
                this._thumbnailurl7 = value;
            }
        }

        public string ThumbnailUrl8
        {
            get
            {
                return this._thumbnailurl8;
            }
            set
            {
                this._thumbnailurl8 = value;
            }
        }

        public int? TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
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

        public int VistiCounts
        {
            get
            {
                return this._visticounts;
            }
            set
            {
                this._visticounts = value;
            }
        }
    }
}

