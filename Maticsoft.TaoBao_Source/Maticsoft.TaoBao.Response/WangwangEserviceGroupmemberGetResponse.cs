namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WangwangEserviceGroupmemberGetResponse : TopResponse
    {
        [XmlArray("group_member_list"), XmlArrayItem("group_member")]
        public List<GroupMember> GroupMemberList { get; set; }
    }
}

