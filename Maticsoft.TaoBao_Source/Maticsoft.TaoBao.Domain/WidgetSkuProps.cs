namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class WidgetSkuProps : TopObject
    {
        [XmlElement("alias")]
        public string Alias { get; set; }

        [XmlElement("key_name")]
        public string KeyName { get; set; }

        [XmlElement("pic_url")]
        public string PicUrl { get; set; }

        [XmlElement("prop_key")]
        public long PropKey { get; set; }

        [XmlElement("prop_value")]
        public long PropValue { get; set; }

        [XmlElement("value_name")]
        public string ValueName { get; set; }
    }
}

