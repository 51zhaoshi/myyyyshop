namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FavoriteAddResponse : TopResponse
    {
        [XmlElement("success")]
        public bool Success { get; set; }
    }
}

