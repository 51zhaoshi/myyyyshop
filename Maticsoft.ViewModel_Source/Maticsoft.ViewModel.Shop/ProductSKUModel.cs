namespace Maticsoft.ViewModel.Shop
{
    using System;
    using System.Collections.Generic;

    public class ProductSKUModel
    {
        public SortedListAttribute ListAttrSKUItems = new SortedListAttribute();
        public List<SKUInfo> ListSKUInfos = new List<SKUInfo>();
        public List<SKUItem> ListSKUItems = new List<SKUItem>();
    }
}

