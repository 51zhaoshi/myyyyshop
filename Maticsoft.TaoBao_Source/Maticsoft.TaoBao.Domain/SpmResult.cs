namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class SpmResult : TopObject
    {
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }

        [XmlArrayItem("top_spm"), XmlArray("spm_modules")]
        public List<TopSpm> SpmModules { get; set; }

        [XmlArrayItem("top_spm"), XmlArray("spm_pages")]
        public List<TopSpm> SpmPages { get; set; }

        [XmlElement("spm_site")]
        public TopSpm SpmSite { get; set; }
    }
}

