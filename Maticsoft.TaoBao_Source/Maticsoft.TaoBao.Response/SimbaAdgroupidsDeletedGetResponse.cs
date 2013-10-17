namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupidsDeletedGetResponse : TopResponse
    {
        [XmlArray("deleted_adgroup_ids"), XmlArrayItem("number")]
        public List<long> DeletedAdgroupIds { get; set; }
    }
}

