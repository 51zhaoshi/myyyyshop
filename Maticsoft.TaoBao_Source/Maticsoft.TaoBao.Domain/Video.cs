namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Video : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("id")]
        public long Id { get; set; }

        [XmlElement("iid")]
        public string Iid { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("num_iid")]
        public long NumIid { get; set; }

        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("video_id")]
        public long VideoId { get; set; }
    }
}

