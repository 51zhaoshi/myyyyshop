namespace Maticsoft.TaoBao.Request
{
    using Maticsoft.TaoBao;
    using Maticsoft.TaoBao.Util;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ItemJointImgRequest : ITopRequest<ItemJointImgResponse>
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
            return "taobao.item.joint.img";
        }

        public IDictionary<string, string> GetParameters()
        {
            TopDictionary dictionary = new TopDictionary();
            dictionary.Add("id", this.Id);
            dictionary.Add("is_major", this.IsMajor);
            dictionary.Add("num_iid", this.NumIid);
            dictionary.Add("pic_path", this.PicPath);
            dictionary.Add("position", this.Position);
            dictionary.AddAll(this.otherParameters);
            return dictionary;
        }

        public void Validate()
        {
            RequestValidator.ValidateRequired("num_iid", this.NumIid);
            RequestValidator.ValidateMinValue("num_iid", this.NumIid, 0L);
            RequestValidator.ValidateRequired("pic_path", this.PicPath);
        }

        public long? Id { get; set; }

        public bool? IsMajor { get; set; }

        public long? NumIid { get; set; }

        public string PicPath { get; set; }

        public long? Position { get; set; }
    }
}

