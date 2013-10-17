namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LoginUser : TopObject
    {
        [XmlElement("create_time")]
        public string CreateTime { get; set; }

        [XmlElement("nick")]
        public string Nick { get; set; }

        [XmlElement("user_id")]
        public long UserId { get; set; }

        [XmlElement("user_type")]
        public string UserType { get; set; }
    }
}

