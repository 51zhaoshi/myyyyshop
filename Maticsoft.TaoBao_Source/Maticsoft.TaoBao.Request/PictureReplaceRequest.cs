namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PictureReplaceRequest : ITopUploadRequest<PictureReplaceResponse>, ITopRequest<PictureReplaceResponse>
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
            return "taobao.picture.replace";
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> dictionary = new Dictionary<string, FileItem>();
            dictionary.Add("image_data", this.ImageData);
            return dictionary;
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("picture_id", this.PictureId);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("image_data", this.ImageData);
            RequestValidator.ValidateRequired("picture_id", this.PictureId);
        }

        public FileItem ImageData { get; set; }

        public long? PictureId { get; set; }
    }
}

