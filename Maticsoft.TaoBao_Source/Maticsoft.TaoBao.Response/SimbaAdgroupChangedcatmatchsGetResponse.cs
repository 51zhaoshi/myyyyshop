namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupChangedcatmatchsGetResponse : TopResponse
    {
        [XmlElement("changed_catmatchs")]
        public ADGroupCatMatchPage ChangedCatmatchs { get; set; }
    }
}

