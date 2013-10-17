namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogisticsCompany : TopObject
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("reg_mail_no")]
        public string RegMailNo { get; set; }
    }
}

