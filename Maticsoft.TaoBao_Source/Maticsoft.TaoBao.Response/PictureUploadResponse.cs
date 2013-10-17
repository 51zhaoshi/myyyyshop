namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureUploadResponse : TopResponse
    {
        [XmlElement("picture")]
        public Maticsoft.TaoBao.Domain.Picture Picture { get; set; }
    }
}

