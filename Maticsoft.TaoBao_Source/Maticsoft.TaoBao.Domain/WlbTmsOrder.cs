namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WlbTmsOrder : TopObject
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("company_code")]
        public string CompanyCode { get; set; }

        [XmlElement("order_code")]
        public string OrderCode { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }
    }
}

