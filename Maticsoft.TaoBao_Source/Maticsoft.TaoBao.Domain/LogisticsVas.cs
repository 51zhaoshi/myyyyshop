namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogisticsVas : TopObject
    {
        [XmlElement("charge_calculate_type")]
        public string ChargeCalculateType { get; set; }

        [XmlElement("comments")]
        public string Comments { get; set; }

        [XmlElement("default_selected")]
        public bool DefaultSelected { get; set; }

        [XmlElement("displayable")]
        public bool Displayable { get; set; }

        [XmlElement("needed")]
        public bool Needed { get; set; }

        [XmlElement("value_displayable")]
        public bool ValueDisplayable { get; set; }

        [XmlElement("vas_code")]
        public string VasCode { get; set; }

        [XmlElement("vas_name")]
        public string VasName { get; set; }
    }
}

