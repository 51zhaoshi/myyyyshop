namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class DescModuleInfo : TopObject
    {
        [XmlElement("anchor_module_ids")]
        public string AnchorModuleIds { get; set; }

        [XmlElement("type")]
        public long Type { get; set; }
    }
}

