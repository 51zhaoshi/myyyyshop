namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UsersGetResponse : TopResponse
    {
        [XmlArrayItem("user"), XmlArray("users")]
        public List<User> Users { get; set; }
    }
}

