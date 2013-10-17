namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class IncrementCustomersGetResponse : TopResponse
    {
        [XmlArrayItem("app_customer"), XmlArray("app_customers")]
        public List<AppCustomer> AppCustomers { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

