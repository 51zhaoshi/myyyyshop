namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class UserTag : TopObject
    {
        [XmlElement("create_date")]
        public string CreateDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("tag_id")]
        public long TagId { get; set; }

        [XmlElement("tag_name")]
        public string TagName { get; set; }
    }
}

