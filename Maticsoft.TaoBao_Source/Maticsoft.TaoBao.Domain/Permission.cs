namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Permission : TopObject
    {
        [XmlElement("is_authorize")]
        public long IsAuthorize { get; set; }

        [XmlElement("parent_code")]
        public string ParentCode { get; set; }

        [XmlElement("permission_code")]
        public string PermissionCode { get; set; }

        [XmlElement("permission_name")]
        public string PermissionName { get; set; }
    }
}

