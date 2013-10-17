namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemGetRequest : ITopRequest<ItemGetResponse>
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
            return "taobao.item.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("track_iid", this.TrackIid);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 1L);
        }

        public string Fields { get; set; }

        public long? NumIid { get; set; }

        public string TrackIid { get; set; }
    }
}

