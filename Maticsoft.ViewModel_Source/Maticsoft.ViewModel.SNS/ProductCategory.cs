namespace Maticsoft.ViewModel.SNS
{
    using Maticsoft.Model.SNS;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class ProductCategory
    {
        private List<Comments> _commentlist;
        private PagedList<Maticsoft.Model.SNS.Products> _productPagedList;
        public List<Categories> ChildList = new List<Categories>();
        public Dictionary<long, IEnumerable<Comments>> DicCommentList = new Dictionary<long, IEnumerable<Comments>>();
        public Categories ParentModel = new Categories();
        public List<SonCategory> SonList = new List<SonCategory>();
        public List<CType> TagsList = new List<CType>();

        public void SetDicComment(long ProductId, IEnumerable<Comments> list)
        {
            this.DicCommentList.Add(ProductId, list);
        }

        private List<SearchWordTop> _keywordlist { get; set; }

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
                if (((this._commentlist != null) && (this._commentlist.Count > 0)) && ((this.ProductPagedList != null) && (this.ProductPagedList.Count > 0)))
                {
                    if (action == null)
                    {
                        action = delegate (Maticsoft.Model.SNS.Products item) {
                            this.SetDicComment(item.ProductID, from comment in this._commentlist
                                where comment.TargetId == item.ProductID
                                select comment);
                        };
                    }
                    this.ProductPagedList.ForEach(action);
                }
            }
        }

        public string CurrentCateName { get; set; }

        public int CurrentCid { get; set; }

        public int CurrentMaxPrice { get; set; }

        public int CurrentMinPrice { get; set; }

        public string CurrentSequence { get; set; }

        public List<SearchWordTop> KeyWordList
        {
            get
            {
                return this._keywordlist;
            }
            set
            {
                List<SearchWordTop>[] list;
                int index;
                this._keywordlist = value;
                if ((value != null) && (value.Count >= 1))
                {
                    list = new List<SearchWordTop>[] { new List<SearchWordTop>(), new List<SearchWordTop>(), new List<SearchWordTop>(), new List<SearchWordTop>() };
                    index = 0;
                    value.ForEach(delegate (SearchWordTop image) {
                        if (index == 4)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                    this.KeyWordList4For = list;
                }
            }
        }

        public List<SearchWordTop>[] KeyWordList4For { get; set; }

        public List<Maticsoft.Model.SNS.Products>[] ProductList4ForCol { get; set; }

        public List<Maticsoft.Model.SNS.Products> ProductListWaterfall { get; set; }

        public PagedList<Maticsoft.Model.SNS.Products> ProductPagedList
        {
            get
            {
                return this._productPagedList;
            }
            set
            {
                List<Maticsoft.Model.SNS.Products>[] list;
                int index;
                this._productPagedList = value;
                if ((value != null) && (value.Count >= 1))
                {
                    list = new List<Maticsoft.Model.SNS.Products>[] { new List<Maticsoft.Model.SNS.Products>(), new List<Maticsoft.Model.SNS.Products>(), new List<Maticsoft.Model.SNS.Products>(), new List<Maticsoft.Model.SNS.Products>() };
                    index = 0;
                    value.ForEach(delegate (Maticsoft.Model.SNS.Products image) {
                        if (index == 4)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                    this.ProductList4ForCol = list;
                }
            }
        }
    }
}

