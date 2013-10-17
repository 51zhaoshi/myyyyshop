namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class ProductListModel
    {
        private List<ProductInfo>[] _productt4FourCol;

        public List<CategoryInfo> CategoryList { get; set; }

        public List<CategoryInfo> CategoryPathList { get; set; }

        public string CurrentCateName { get; set; }

        public int CurrentCid { get; set; }

        public string CurrentMod { get; set; }

        public List<ProductInfo>[] ProductList4FourCol
        {
            get
            {
                int index;
                if (this._productt4FourCol != null)
                {
                    return this._productt4FourCol;
                }
                List<ProductInfo>[] list = new List<ProductInfo>[] { new List<ProductInfo>(), new List<ProductInfo>(), new List<ProductInfo>(), new List<ProductInfo>() };
                if (this.ProductPagedList != null)
                {
                    index = 0;
                    this.ProductPagedList.ForEach(delegate (ProductInfo image) {
                        if (index == 4)
                        {
                            index = 0;
                        }
                        list[index++].Add(image);
                    });
                }
                return list;
            }
            set
            {
                this._productt4FourCol = value;
            }
        }

        public PagedList<ProductInfo> ProductPagedList { get; set; }

        public List<CategoryInfo> SubCategoryList { get; set; }
    }
}

