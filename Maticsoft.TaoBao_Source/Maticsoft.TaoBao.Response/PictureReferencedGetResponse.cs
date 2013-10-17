namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureReferencedGetResponse : TopResponse
    {
        [XmlArray("references"), XmlArrayItem("reference_detail")]
        public List<ReferenceDetail> References { get; set; }
    }
}

