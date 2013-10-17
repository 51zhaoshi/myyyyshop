namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class TopatsTasksGetResponse : TopResponse
    {
        [XmlArrayItem("task"), XmlArray("tasks")]
        public List<Task> Tasks { get; set; }
    }
}

