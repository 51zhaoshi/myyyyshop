namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class WlbOrderCancelResponse : TopResponse
    {
        [XmlElement("error_code_list")]
        public string ErrorCodeList { get; set; }

        [XmlElement("modify_time")]
        public string ModifyTime { get; set; }
    }
}

