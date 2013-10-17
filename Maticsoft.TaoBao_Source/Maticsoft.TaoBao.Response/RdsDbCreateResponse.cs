namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Domain;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class RdsDbCreateResponse : TopResponse
    {
        [XmlElement("rds_db_info")]
        public Maticsoft.TaoBao.Domain.RdsDbInfo RdsDbInfo { get; set; }
    }
}

