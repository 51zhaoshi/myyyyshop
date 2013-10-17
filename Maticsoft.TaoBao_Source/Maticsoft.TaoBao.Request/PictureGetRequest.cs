namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureGetRequest : ITopRequest<PictureGetResponse>
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
            return "taobao.picture.get";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("deleted", this.Deleted);
            dictionary.Add("end_date", this.EndDate);
            dictionary.Add("modified_time", this.ModifiedTime);
            dictionary.Add("order_by", this.OrderBy);
            dictionary.Add("page_no", this.PageNo);
            dictionary.Add("page_size", this.PageSize);
            dictionary.Add("picture_category_id", this.PictureCategoryId);
            dictionary.Add("picture_id", this.PictureId);
            dictionary.Add("start_date", this.StartDate);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
        }

        public string Deleted { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public string OrderBy { get; set; }

        public long? PageNo { get; set; }

        public long? PageSize { get; set; }

        public long? PictureCategoryId { get; set; }

        public long? PictureId { get; set; }

        public DateTime? StartDate { get; set; }

        public string Title { get; set; }
    }
}

