namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SubUserPermission : TopObject
    {
        [XmlArray("permissions"), XmlArrayItem("permission")]
        public List<Permission> Permissions { get; set; }

        [XmlArrayItem("role"), XmlArray("roles")]
        public List<Role> Roles { get; set; }
    }
}

