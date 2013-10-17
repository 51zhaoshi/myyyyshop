namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureUserinfoGetResponse : TopResponse
    {
        [XmlElement("user_info")]
        public Maticsoft.TaoBao.Domain.UserInfo UserInfo { get; set; }
    }
}

