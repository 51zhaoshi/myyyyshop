namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterSubusersGetResponse : TopResponse
    {
        [XmlArray("subusers"), XmlArrayItem("sub_user_info")]
        public List<SubUserInfo> Subusers { get; set; }
    }
}

