namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AfterSale : TopObject
    {
        [XmlElement("after_sale_id")]
        public long AfterSaleId { get; set; }

        [XmlElement("after_sale_name")]
        public string AfterSaleName { get; set; }

        [XmlElement("after_sale_path")]
        public string AfterSalePath { get; set; }
    }
}

