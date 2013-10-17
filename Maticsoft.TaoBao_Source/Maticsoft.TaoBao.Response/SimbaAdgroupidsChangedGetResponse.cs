namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupidsChangedGetResponse : TopResponse
    {
        [XmlArray("changed_adgroupids"), XmlArrayItem("number")]
        public List<long> ChangedAdgroupids { get; set; }
    }
}

