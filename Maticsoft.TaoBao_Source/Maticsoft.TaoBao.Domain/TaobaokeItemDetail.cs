namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TaobaokeItemDetail : TopObject
    {
        [XmlElement("click_url")]
        public string ClickUrl { get; set; }

        [XmlElement("item")]
        public Maticsoft.TaoBao.Domain.Item Item { get; set; }

        [XmlElement("seller_credit_score")]
        public long SellerCreditScore { get; set; }

        [XmlElement("shop_click_url")]
        public string ShopClickUrl { get; set; }
    }
}

