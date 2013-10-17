namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AlipayAccount : TopObject
    {
        [XmlElement("alipay_user_id")]
        public string AlipayUserId { get; set; }

        [XmlElement("available_amount")]
        public string AvailableAmount { get; set; }

        [XmlElement("freeze_amount")]
        public string FreezeAmount { get; set; }

        [XmlElement("total_amount")]
        public string TotalAmount { get; set; }
    }
}

