namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbItemAuthorizationAddResponse : TopResponse
    {
        [XmlArray("rule_id_list"), XmlArrayItem("number")]
        public List<long> RuleIdList { get; set; }
    }
}

