namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class FenxiaoGradesGetResponse : TopResponse
    {
        [XmlArrayItem("fenxiao_grade"), XmlArray("fenxiao_grades")]
        public List<FenxiaoGrade> FenxiaoGrades { get; set; }

        [XmlElement("total_results")]
        public long TotalResults { get; set; }
    }
}

