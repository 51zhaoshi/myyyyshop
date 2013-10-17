namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class RdsDbGetResponse : TopResponse
    {
        [XmlArray("rds_db_infos"), XmlArrayItem("rds_db_info")]
        public List<RdsDbInfo> RdsDbInfos { get; set; }
    }
}

