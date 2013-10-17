namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class EvalDetail : TopObject
    {
        [XmlElement("eval_code")]
        public long EvalCode { get; set; }

        [XmlElement("eval_recer")]
        public string EvalRecer { get; set; }

        [XmlElement("eval_sender")]
        public string EvalSender { get; set; }

        [XmlElement("eval_time")]
        public string EvalTime { get; set; }

        [XmlElement("send_time")]
        public string SendTime { get; set; }
    }
}

