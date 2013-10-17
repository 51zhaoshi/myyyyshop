namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Evaluation : TopObject
    {
        [XmlElement("evaluation_name")]
        public string EvaluationName { get; set; }

        [XmlElement("evaluation_num")]
        public string EvaluationNum { get; set; }
    }
}

