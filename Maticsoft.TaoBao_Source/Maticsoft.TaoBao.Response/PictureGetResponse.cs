namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PictureGetResponse : TopResponse
    {
        [XmlArray("pictures"), XmlArrayItem("picture")]
        public List<Picture> Pictures { get; set; }

        [XmlElement("totalResults")]
        public long TotalResults { get; set; }
    }
}

