namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SellerAuthorize : TopObject
    {
        [XmlArray("brands"), XmlArrayItem("brand")]
        public List<Brand> Brands { get; set; }

        [XmlArrayItem("item_cat"), XmlArray("item_cats")]
        public List<ItemCat> ItemCats { get; set; }

        [XmlArrayItem("item_cat"), XmlArray("xinpin_item_cats")]
        public List<ItemCat> XinpinItemCats { get; set; }
    }
}

