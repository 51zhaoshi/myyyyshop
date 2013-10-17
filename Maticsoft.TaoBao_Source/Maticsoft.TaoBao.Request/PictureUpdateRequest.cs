namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureUpdateRequest : ITopRequest<PictureUpdateResponse>
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
            return "taobao.picture.update";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("new_name", this.NewName);
            dictionary.Add("picture_id", this.PictureId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("new_name", this.NewName);
            RequestValidator.ValidateMaxLength("new_name", this.NewName, 50);
            RequestValidator.ValidateRequired("picture_id", this.PictureId);
        }

        public string NewName { get; set; }

        public long? PictureId { get; set; }
    }
}

