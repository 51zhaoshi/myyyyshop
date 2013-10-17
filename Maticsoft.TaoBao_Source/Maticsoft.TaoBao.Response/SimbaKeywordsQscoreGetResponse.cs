namespace Maticsoft.TaoBao.Response
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SimbaKeywordsQscoreGetResponse : TopResponse
    {
        [XmlArray("keyword_qscore_list"), XmlArrayItem("keyword_qscore")]
        public List<KeywordQscore> KeywordQscoreList { get; set; }
    }
}

