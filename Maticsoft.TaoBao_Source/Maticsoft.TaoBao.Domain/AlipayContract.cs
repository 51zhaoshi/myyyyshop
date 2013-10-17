namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AlipayContract : TopObject
    {
        [XmlElement("alipay_user_id")]
        public string AlipayUserId { get; set; }

        [XmlElement("contract_content")]
        public string ContractContent { get; set; }

        [XmlElement("end_time")]
        public string EndTime { get; set; }

        [XmlElement("page_url")]
        public string PageUrl { get; set; }

        [XmlElement("start_time")]
        public string StartTime { get; set; }

        [XmlElement("subscribe")]
        public bool Subscribe { get; set; }
    }
}

