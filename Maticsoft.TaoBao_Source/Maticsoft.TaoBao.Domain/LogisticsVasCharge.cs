namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class LogisticsVasCharge : TopObject
    {
        [XmlElement("original_total_vas_cost")]
        public string OriginalTotalVasCost { get; set; }

        [XmlElement("total_vas_cost")]
        public string TotalVasCost { get; set; }

        [XmlElement("total_vas_save_cost")]
        public string TotalVasSaveCost { get; set; }

        [XmlArray("vas_cost_list"), XmlArrayItem("logistics_vas_item_charge")]
        public List<LogisticsVasItemCharge> VasCostList { get; set; }
    }
}

