namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TmallProductSpecPicUploadResponse : TopResponse
    {
        [XmlElement("spec_pic_url")]
        public string SpecPicUrl { get; set; }
    }
}

