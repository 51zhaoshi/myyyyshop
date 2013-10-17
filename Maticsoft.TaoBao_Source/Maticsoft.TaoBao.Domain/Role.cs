namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Role : TopObject
    {
        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("modified_time")]
        public string ModifiedTime { get; set; }

        [XmlArrayItem("permission"), XmlArray("permissions")]
        public List<Permission> Permissions { get; set; }

        [XmlElement("role_id")]
        public long RoleId { get; set; }

        [XmlElement("role_name")]
        public string RoleName { get; set; }

        [XmlElement("seller_id")]
        public long SellerId { get; set; }
    }
}

