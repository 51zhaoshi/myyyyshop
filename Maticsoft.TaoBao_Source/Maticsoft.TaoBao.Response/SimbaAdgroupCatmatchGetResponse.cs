namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupCatmatchGetResponse : TopResponse
    {
        [XmlElement("adgroupcatmatch")]
        public ADGroupCatmatch Adgroupcatmatch { get; set; }
    }
}

