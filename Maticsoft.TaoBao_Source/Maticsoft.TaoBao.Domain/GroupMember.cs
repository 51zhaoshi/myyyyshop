namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class GroupMember : TopObject
    {
        [XmlElement("group_id")]
        public long GroupId { get; set; }

        [XmlElement("group_name")]
        public string GroupName { get; set; }

        [XmlElement("member_list")]
        public string MemberList { get; set; }
    }
}

