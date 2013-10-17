namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterRolesGetResponse : TopResponse
    {
        [XmlArrayItem("role"), XmlArray("roles")]
        public List<Role> Roles { get; set; }
    }
}

