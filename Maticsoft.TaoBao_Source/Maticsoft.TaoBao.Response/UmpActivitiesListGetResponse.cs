namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpActivitiesListGetResponse : TopResponse
    {
        [XmlArray("activities"), XmlArrayItem("string")]
        public List<string> Activities { get; set; }
    }
}

