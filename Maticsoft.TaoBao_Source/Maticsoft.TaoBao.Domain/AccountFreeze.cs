namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AccountFreeze : TopObject
    {
        [XmlElement("freeze_amount")]
        public string FreezeAmount { get; set; }

        [XmlElement("freeze_name")]
        public string FreezeName { get; set; }

        [XmlElement("freeze_type")]
        public string FreezeType { get; set; }
    }
}

