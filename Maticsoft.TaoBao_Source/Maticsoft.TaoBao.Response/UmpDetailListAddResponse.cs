namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class UmpDetailListAddResponse : TopResponse
    {
        [XmlArrayItem("number"), XmlArray("detail_id_list")]
        public List<long> DetailIdList { get; set; }
    }
}

