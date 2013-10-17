namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LoginLog : TopObject
    {
        [XmlElement("time")]
        public string Time { get; set; }

        [XmlElement("type")]
        public string Type { get; set; }
    }
}

