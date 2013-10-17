namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;

    public class PictureUserinfoGetRequest : ITopRequest<PictureUserinfoGetResponse>
    {
        private IDictionary<string, string> otherParameters;

        public void AddOtherParameter(string key, string value)
        {
            if (this.otherParameters == null)
            {
                this.otherParameters = new TopDictionary();
            }
            this.otherParameters.Add(key, value);
        }

        public string GetApiName()
        {
            return "taobao.picture.userinfo.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }
    }
}

