namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureIsreferencedGetResponse : TopResponse
    {
        [XmlElement("is_referenced")]
        public bool IsReferenced { get; set; }
    }
}

