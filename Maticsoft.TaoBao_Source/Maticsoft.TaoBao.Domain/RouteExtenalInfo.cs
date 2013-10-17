namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RouteExtenalInfo : TopObject
    {
        [XmlElement("cod")]
        public bool Cod { get; set; }

        [XmlElement("credit_opened")]
        public bool CreditOpened { get; set; }

        [XmlElement("credit_total_balance")]
        public string CreditTotalBalance { get; set; }

        [XmlElement("recommend")]
        public bool Recommend { get; set; }

        [XmlArrayItem("string"), XmlArray("special_codes")]
        public List<string> SpecialCodes { get; set; }

        [XmlElement("top")]
        public bool Top { get; set; }
    }
}

