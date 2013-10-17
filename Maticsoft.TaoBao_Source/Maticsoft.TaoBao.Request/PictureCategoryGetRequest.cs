namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureCategoryGetRequest : ITopRequest<PictureCategoryGetResponse>
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
            return "taobao.picture.category.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("modified_time", this.ModifiedTime);
            dictionary.Add("parent_id", this.ParentId);
            dictionary.Add("picture_category_id", this.PictureCategoryId);
            dictionary.Add("picture_category_name", this.PictureCategoryName);
            dictionary.Add("type", this.Type);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public DateTime? ModifiedTime { get; set; }

        public long? ParentId { get; set; }

        public long? PictureCategoryId { get; set; }

        public string PictureCategoryName { get; set; }

        public string Type { get; set; }
    }
}

