namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoLoginUserGetResponse : TopResponse
    {
        [XmlElement("login_user")]
        public Maticsoft.TaoBao.Domain.LoginUser LoginUser { get; set; }
    }
}

