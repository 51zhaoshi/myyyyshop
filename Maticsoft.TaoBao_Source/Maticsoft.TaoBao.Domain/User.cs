namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class User : TopObject
    {
        [XmlElement("alipay_bind")]
        public string AlipayBind { get; set; }

        [XmlElement("auto_repost")]
        public string AutoRepost { get; set; }

        [XmlElement("avatar")]
        public string Avatar { get; set; }

        [XmlElement("birthday")]
        public string Birthday { get; set; }

        [XmlElement("buyer_credit")]
        public UserCredit BuyerCredit { get; set; }

        [XmlElement("consumer_protection")]
        public bool ConsumerProtection { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("has_more_pic")]
        public bool HasMorePic { get; set; }

        [XmlElement("has_shop")]
        public bool HasShop { get; set; }

        [XmlElement("has_sub_stock")]
        public bool HasSubStock { get; set; }

        [XmlElement("is_golden_seller")]
        public bool IsGoldenSeller { get; set; }

        [XmlElement("is_lightning_consignment")]
        public bool IsLightningConsignment { get; set; }

        [XmlElement("item_img_num")]
        public long ItemImgNum { get; set; }

        [XmlElement("item_img_size")]
        public long ItemImgSize { get; set; }

        [XmlElement("last_visit")]
        public string LastVisit { get; set; }

        [XmlElement("liangpin")]
        public bool Liangpin { get; set; }

        [XmlElement("location")]
        public Maticsoft.TaoBao.Domain.Location Location { get; set; }

        [XmlElement("magazine_subscribe")]
        public bool MagazineSubscribe { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("online_gaming")]
        public bool OnlineGaming { get; set; }

        [XmlElement("promoted_type")]
        public string PromotedType { get; set; }

        [XmlElement("prop_img_num")]
        public long PropImgNum { get; set; }

        [XmlElement("prop_img_size")]
        public long PropImgSize { get; set; }

        [XmlElement("seller_credit")]
        public UserCredit SellerCredit { get; set; }

        [XmlElement("sex")]
        public string Sex { get; set; }

        [XmlElement("sign_food_seller_promise")]
        public bool SignFoodSellerPromise { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }

        [XmlElement("uid")]
        public string Uid { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("vertical_market")]
        public string VerticalMarket { get; set; }

        [XmlElement("vip_info")]
        public string VipInfo { get; set; }
    }
}

