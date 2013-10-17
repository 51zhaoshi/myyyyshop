namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureUploadRequest : ITopUploadRequest<PictureUploadResponse>, ITopRequest<PictureUploadResponse>
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
            return "taobao.picture.upload";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("img", this.Img);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("image_input_title", this.ImageInputTitle);
            dictionary.Add("picture_category_id", this.PictureCategoryId);
            dictionary.Add("title", this.Title);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("image_input_title", this.ImageInputTitle);
            RequestValidator.ValidateRequired("img", this.Img);
            RequestValidator.ValidateRequired("picture_category_id", this.PictureCategoryId);
        }

        public string ImageInputTitle { get; set; }

        public FileItem Img { get; set; }

        public long? PictureCategoryId { get; set; }

        public string Title { get; set; }
    }
}

