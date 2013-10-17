namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SpmeffectGetResponse : TopResponse
    {
        [XmlElement("spm_result")]
        public Maticsoft.TaoBao.Domain.SpmResult SpmResult { get; set; }
    }
}

