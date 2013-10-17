namespace Maticsoft.TaoBao
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public abstract class TopResponse
    {
        protected TopResponse()
        {
        }

        public string Body { get; set; }

        [XmlElement("code")]
        public string ErrCode { get; set; }

        [XmlElement("msg")]
        public string ErrMsg { get; set; }

        public bool IsError
        {
            get
            {
                if (string.IsNullOrEmpty(this.ErrCode))
                {
                    return !string.IsNullOrEmpty(this.SubErrCode);
                }
                return true;
            }
        }

        public string ReqUrl { get; set; }

        [XmlElement("sub_code")]
        public string SubErrCode { get; set; }

        [XmlElement("sub_msg")]
        public string SubErrMsg { get; set; }

        [XmlElement("top_forbidden_fields")]
        public string TopForbiddenFields { get; set; }
    }
}

