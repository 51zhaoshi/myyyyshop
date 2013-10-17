namespace Maticsoft.TaoBao.Domain
{
    using Maticsoft.TaoBao;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    [Serializable]
    public class RdsDbInfo : TopObject
    {
        [XmlElement("charset")]
        public string Charset { get; set; }

        [XmlElement("comment")]
        public string Comment { get; set; }

        [XmlElement("db_id")]
        public string DbId { get; set; }

        [XmlElement("db_name")]
        public string DbName { get; set; }

        [XmlElement("db_status")]
        public string DbStatus { get; set; }

        [XmlElement("db_type")]
        public string DbType { get; set; }

        [XmlElement("instance_id")]
        public string InstanceId { get; set; }

        [XmlElement("instance_name")]
        public string InstanceName { get; set; }

        [XmlElement("instance_type")]
        public string InstanceType { get; set; }

        [XmlElement("max_account")]
        public string MaxAccount { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }

        [XmlElement("uid")]
        public string Uid { get; set; }

        [XmlElement("user_name")]
        public string UserName { get; set; }
    }
}

