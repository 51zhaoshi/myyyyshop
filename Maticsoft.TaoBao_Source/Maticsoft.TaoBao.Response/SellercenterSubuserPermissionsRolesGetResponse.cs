namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterSubuserPermissionsRolesGetResponse : TopResponse
    {
        [XmlElement("subuser_permission")]
        public SubUserPermission SubuserPermission { get; set; }
    }
}

