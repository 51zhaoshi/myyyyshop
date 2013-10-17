namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class FenxiaoGrade : TopObject
    {
        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("grade_id")]
        public long GradeId { get; set; }

        [XmlElement("modified")]
        public string Modified { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}

