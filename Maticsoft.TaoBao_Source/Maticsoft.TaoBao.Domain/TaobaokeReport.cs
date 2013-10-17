namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class TaobaokeReport : TopObject
    {
        [XmlArray("taobaoke_report_members"), XmlArrayItem("taobaoke_report_member")]
        public List<TaobaokeReportMember> TaobaokeReportMembers { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

