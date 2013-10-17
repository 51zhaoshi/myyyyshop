namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class ReplyStatById : TopObject
    {
        [XmlElement("reply_num")]
        public long ReplyNum { get; set; }

        [XmlElement("user_id")]
        public string UserId { get; set; }
    }
}

