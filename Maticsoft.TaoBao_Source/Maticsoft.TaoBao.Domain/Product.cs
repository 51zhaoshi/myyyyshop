namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Product : TopObject
    {
        [XmlElement("binds")]
        public string Binds { get; set; }

        [XmlElement("binds_str")]
        public string BindsStr { get; set; }

        [XmlElement("cat_name")]
        public string CatName { get; set; }

        [XmlElement("cid")]
        public long Cid { get; set; }

        [XmlElement("collect_num")]
        public long CollectNum { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("customer_props")]
        public string CustomerProps { get; set; }

        [XmlElement("desc")]
        public string Desc { get; set; }

        [XmlElement("level")]
        public long Level { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("outer_id")]
        public string OuterId { get; set; }

        [XmlElement("pic_path")]
        public string PicPath { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("price")]
        public string Price { get; set; }

        [XmlElement("product_id")]
        public long ProductId { get; set; }

        [XmlArray("product_imgs"), XmlArrayItem("product_img")]
        public List<ProductImg> ProductImgs { get; set; }

        [XmlArrayItem("product_prop_img"), XmlArray("product_prop_imgs")]
        public List<ProductPropImg> ProductPropImgs { get; set; }

        [XmlElement("property_alias")]
        public string PropertyAlias { get; set; }

        [XmlElement("props")]
        public string Props { get; set; }

        [XmlElement("props_str")]
        public string PropsStr { get; set; }

        [XmlElement("sale_props")]
        public string SaleProps { get; set; }

        [XmlElement("sale_props_str")]
        public string SalePropsStr { get; set; }

        [XmlElement("status")]
        public long Status { get; set; }

        [XmlElement("tsc")]
        public string Tsc { get; set; }

        [XmlElement("vertical_market")]
        public long VerticalMarket { get; set; }
    }
}

