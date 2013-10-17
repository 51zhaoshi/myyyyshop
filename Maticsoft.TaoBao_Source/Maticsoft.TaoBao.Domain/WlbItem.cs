namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbItem : TopObject
    {
        [XmlElement("brand_id")]
        public long BrandId { get; set; }

        [XmlElement("color")]
        public string Color { get; set; }

        [XmlElement("creator")]
        public string Creator { get; set; }

        [XmlElement("flag")]
        public string Flag { get; set; }

        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }

        [XmlElement("goods_cat")]
        public string GoodsCat { get; set; }

        [XmlElement("height")]
        public long Height { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("is_dangerous")]
        public bool IsDangerous { get; set; }

        [XmlElement("is_friable")]
        public bool IsFriable { get; set; }

        [XmlElement("is_sku")]
        public bool IsSku { get; set; }

        [XmlElement("item_code")]
        public string ItemCode { get; set; }

        [XmlElement("last_modifier")]
        public string LastModifier { get; set; }

        [XmlElement("length")]
        public long Length { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("package_material")]
        public string PackageMaterial { get; set; }

        [XmlElement("parent_id")]
        public long ParentId { get; set; }

        [XmlElement("price")]
        public long Price { get; set; }

        [XmlElement("pricing_cat")]
        public string PricingCat { get; set; }

        [XmlElement("properties")]
        public string Properties { get; set; }

        [XmlElement("publish_version")]
        public long PublishVersion { get; set; }

        [XmlElement("remark")]
        public string Remark { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("user_nick")]
        public string UserNick { get; set; }

        [XmlElement("volume")]
        public long Volume { get; set; }

        [XmlElement("weight")]
        public long Weight { get; set; }

        [XmlElement("width")]
        public long Width { get; set; }
    }
}

