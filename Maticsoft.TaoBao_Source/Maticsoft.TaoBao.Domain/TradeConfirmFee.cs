namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TradeConfirmFee : TopObject
    {
        [XmlElement("confirm_fee")]
        public string ConfirmFee { get; set; }

        [XmlElement("confirm_post_fee")]
        public string ConfirmPostFee { get; set; }

        [XmlElement("is_last_order")]
        public bool IsLastOrder { get; set; }
    }
}

