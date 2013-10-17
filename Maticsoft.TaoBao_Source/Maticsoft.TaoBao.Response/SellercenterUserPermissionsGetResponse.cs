namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterUserPermissionsGetResponse : TopResponse
    {
        [XmlArray("permissions"), XmlArrayItem("permission")]
        public List<Permission> Permissions { get; set; }
    }
}

