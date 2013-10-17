namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupDeletedcatmatchsGetResponse : TopResponse
    {
        [XmlElement("deleted_catmatchs")]
        public ADGroupCatMatchPage DeletedCatmatchs { get; set; }
    }
}

