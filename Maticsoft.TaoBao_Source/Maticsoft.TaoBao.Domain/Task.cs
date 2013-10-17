namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class Task : TopObject
    {
        [XmlElement("check_code")]
        public string CheckCode { get; set; }

        [XmlElement("created")]
        public string Created { get; set; }

        [XmlElement("download_url")]
        public string DownloadUrl { get; set; }

        [XmlElement("method")]
        public string Method { get; set; }

        [XmlElement("schedule")]
        public string Schedule { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlArrayItem("subtask"), XmlArray("subtasks")]
        public List<Subtask> Subtasks { get; set; }

        [XmlElement("task_id")]
        public long TaskId { get; set; }
    }
}

