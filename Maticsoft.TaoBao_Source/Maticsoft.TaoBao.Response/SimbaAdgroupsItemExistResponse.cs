namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaAdgroupsItemExistResponse : TopResponse
    {
        [XmlElement("exist")]
        public bool Exist { get; set; }
    }
}

