namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TaobaokeWidgetItemsConvertRequest : ITopRequest<TaobaokeWidgetItemsConvertResponse>
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
            return "taobao.taobaoke.widget.items.convert";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("fields", this.Fields);
            dictionary.Add("is_mobile", this.IsMobile);
            dictionary.Add("num_iids", this.NumIids);
            dictionary.Add("outer_code", this.OuterCode);
            dictionary.Add("track_iids", this.TrackIids);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("num_iids", this.NumIids, 50);
            RequestValidator.ValidateMaxListSize("track_iids", this.TrackIids, 50);
        }

        public string Fields { get; set; }

        public bool? IsMobile { get; set; }

        public string NumIids { get; set; }

        public string OuterCode { get; set; }

        public string TrackIids { get; set; }
    }
}

