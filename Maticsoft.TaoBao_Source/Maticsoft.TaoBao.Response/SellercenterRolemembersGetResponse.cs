namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SellercenterRolemembersGetResponse : TopResponse
    {
        [XmlArrayItem("sub_user_info"), XmlArray("subusers")]
        public List<SubUserInfo> Subusers { get; set; }
    }
}

