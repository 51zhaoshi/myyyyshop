namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Subtask : TopObject
    {
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

        [XmlElement("sub_task_request")]
        public string SubTaskRequest { get; set; }

        [XmlElement("sub_task_result")]
        public string SubTaskResult { get; set; }
    }
}

