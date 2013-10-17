namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class StaffEvalStatById : TopObject
    {
        [XmlArrayItem("evaluation"), XmlArray("evaluations")]
        public List<Evaluation> Evaluations { get; set; }

        [XmlElement("service_staff_id")]
        public string ServiceStaffId { get; set; }
    }
}

