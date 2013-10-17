namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class AlipayUserDetail : TopObject
    {
        [XmlElement("alipay_user_id")]
        public string AlipayUserId { get; set; }

        [XmlElement("certified")]
        public bool Certified { get; set; }

        [XmlElement("logon_id")]
        public string LogonId { get; set; }

        [XmlElement("real_name")]
        public string RealName { get; set; }

        [XmlElement("sex")]
        public string Sex { get; set; }

        [XmlElement("user_status")]
        public string UserStatus { get; set; }

        [XmlElement("user_type")]
        public string UserType { get; set; }
    }
}

