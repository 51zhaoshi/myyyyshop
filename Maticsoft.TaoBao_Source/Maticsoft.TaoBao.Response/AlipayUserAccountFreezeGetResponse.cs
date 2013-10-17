namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class AlipayUserAccountFreezeGetResponse : TopResponse
    {
        [XmlArrayItem("account_freeze"), XmlArray("freeze_items")]
        public List<AccountFreeze> FreezeItems { get; set; }

        [XmlElement("total_results")]
        public string TotalResults { get; set; }
    }
}

