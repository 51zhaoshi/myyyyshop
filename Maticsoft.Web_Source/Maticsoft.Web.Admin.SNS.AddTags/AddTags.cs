namespace Maticsoft.Web.Admin.SNS.AddTags
{
    using Maticsoft.BLL.SNS;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using Maticsoft.Web;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class AddTags : PageBaseAdmin
    {
        protected Button btnAddTags;
        protected Button btnSave;
        protected HtmlForm form1;
        protected HtmlHead Head1;
        protected HiddenField hidValue;
        protected TextBox InputTags;
        protected HtmlGenericControl lblTip;
        protected Literal Literal1;
        protected Literal Literal2;
        private Maticsoft.BLL.SNS.PhotoTags photoTags = new Maticsoft.BLL.SNS.PhotoTags();
        private Maticsoft.BLL.SNS.Tags productTags = new Maticsoft.BLL.SNS.Tags();
        protected StringBuilder strSelectValue = new StringBuilder();
        protected StringBuilder strTagsValue = new StringBuilder();

        private void BindTags()
        {
            if (this.Type == "Product")
            {
                int num = Globals.SafeInt(this.Id, 0);
                Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
                Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
                List<Maticsoft.Model.SNS.Tags> list = new List<Maticsoft.Model.SNS.Tags>();
                model = products.GetModel((long) num);
                if ((model != null) && !string.IsNullOrEmpty(model.Tags))
                {
                    foreach (string str in model.Tags.Split(new char[] { ',' }))
                    {
                        if (str.Length < 10)
                        {
                            this.strSelectValue.Append("<span class='SKUValue'><span class='span1'><a>" + str + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del'>删除</a></span> </span>");
                        }
                    }
                    this.hidValue.Value = model.Tags;
                }
                foreach (Maticsoft.Model.SNS.Tags tags in this.productTags.GetModelList("TypeId in (select ID from SNS_TagType where cid>=0)"))
                {
                    if ((model != null) && !string.IsNullOrEmpty(model.Tags))
                    {
                        if (!model.Tags.Contains(tags.TagName))
                        {
                            this.strTagsValue.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", tags.TagID, "'><span class='span1'><a id='tags", tags.TagID, "'>", tags.TagName, "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del", tags.TagID, "'>删除</a></span> </span>" }));
                        }
                    }
                    else
                    {
                        this.strTagsValue.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", tags.TagID, "'><span class='span1'><a id='tags", tags.TagID, "'>", tags.TagName, "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del", tags.TagID, "'>删除</a></span> </span>" }));
                    }
                }
            }
            if (this.Type == "Photo")
            {
                int photoID = Globals.SafeInt(this.Id, 0);
                Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
                Maticsoft.Model.SNS.Photos photos2 = new Maticsoft.Model.SNS.Photos();
                List<Maticsoft.Model.SNS.PhotoTags> list2 = new List<Maticsoft.Model.SNS.PhotoTags>();
                photos2 = photos.GetModel(photoID);
                if ((photos2 != null) && !string.IsNullOrEmpty(photos2.Tags))
                {
                    foreach (string str2 in photos2.Tags.Split(new char[] { ',' }))
                    {
                        this.strSelectValue.Append("<span class='SKUValue'><span class='span1'><a>" + str2 + "</a></span><span class='span2'><a href='javascript:void(0)'  class='del'>删除</a></span> </span>");
                    }
                    this.hidValue.Value = photos2.Tags;
                }
                foreach (Maticsoft.Model.SNS.PhotoTags tags2 in this.photoTags.GetModelList(""))
                {
                    if ((photos2 != null) && !string.IsNullOrEmpty(photos2.Tags))
                    {
                        if (!photos2.Tags.Contains(tags2.TagName))
                        {
                            this.strTagsValue.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", tags2.TagID, "'><span class='span1'><a id='tags", tags2.TagID, "'>", tags2.TagName, "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del", tags2.TagID, "'>删除</a></span> </span>" }));
                        }
                    }
                    else
                    {
                        this.strTagsValue.Append(string.Concat(new object[] { "<span class='SKUValue' id='span", tags2.TagID, "'><span class='span1'><a id='tags", tags2.TagID, "'>", tags2.TagName, "</a></span><span class='span2'><a href='javascript:void(0)' style='display:none' class='del' id='del", tags2.TagID, "'>删除</a></span> </span>" }));
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Button button1 = (Button) sender;
            if (this.Type == "Product")
            {
                int num = Globals.SafeInt(this.Id, 0);
                Maticsoft.BLL.SNS.Products products = new Maticsoft.BLL.SNS.Products();
                Maticsoft.Model.SNS.Products model = new Maticsoft.Model.SNS.Products();
                model = products.GetModel((long) num);
                if (model != null)
                {
                    model.Tags = this.hidValue.Value.TrimEnd(new char[] { ',' }).TrimStart(new char[] { ',' });
                    if (!string.IsNullOrEmpty(this.InputTags.Text))
                    {
                        model.Tags = model.Tags + "," + this.InputTags.Text;
                    }
                    model.Tags = model.Tags.TrimEnd(new char[] { ',' }).TrimStart(new char[] { ',' });
                    products.Update(model);
                    this.lblTip.Visible = true;
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加商品(ProductID=" + num + ")标签成功!", this);
                }
                Maticsoft.Model.SNS.Tags tags = new Maticsoft.Model.SNS.Tags {
                    TagName = this.InputTags.Text,
                    TypeId = this.GetTypeIdByProductId(Globals.SafeInt(this.Id, 0))
                };
                if (!string.IsNullOrEmpty(tags.TagName) && (tags.TypeId > 0))
                {
                    this.productTags.Add(tags);
                }
                this.InputTags.Text = "";
                this.BindTags();
            }
            if (this.Type == "Photo")
            {
                int photoID = Globals.SafeInt(this.Id, 0);
                Maticsoft.BLL.SNS.Photos photos = new Maticsoft.BLL.SNS.Photos();
                Maticsoft.Model.SNS.Photos photos2 = new Maticsoft.Model.SNS.Photos();
                photos2 = photos.GetModel(photoID);
                if (photos2 != null)
                {
                    photos2.Tags = this.hidValue.Value.TrimEnd(new char[] { ',' }).TrimStart(new char[] { ',' });
                    if (!string.IsNullOrEmpty(this.InputTags.Text))
                    {
                        photos2.Tags = photos2.Tags + "," + this.InputTags.Text;
                    }
                    photos2.Tags = photos2.Tags.TrimEnd(new char[] { ',' }).TrimStart(new char[] { ',' });
                    photos.Update(photos2);
                    this.lblTip.Visible = true;
                    LogHelp.AddUserLog(base.CurrentUser.UserName, base.CurrentUser.UserType, "增加图片（PhotoID=" + photoID + "）标签成功!", this);
                }
                Maticsoft.Model.SNS.PhotoTags tags2 = new Maticsoft.Model.SNS.PhotoTags {
                    TagName = this.InputTags.Text
                };
                if (!string.IsNullOrEmpty(tags2.TagName))
                {
                    this.photoTags.Add(tags2);
                }
                this.InputTags.Text = "";
                this.BindTags();
            }
        }

        public int GetTypeIdByProductId(int ProductId)
        {
            Maticsoft.Model.SNS.Products model = new Maticsoft.BLL.SNS.Products().GetModel((long) ProductId);
            if (model != null)
            {
                Maticsoft.BLL.SNS.Categories categories = new Maticsoft.BLL.SNS.Categories();
                Maticsoft.Model.SNS.Categories categories2 = new Maticsoft.Model.SNS.Categories();
                List<Maticsoft.Model.SNS.TagType> modelList = new List<Maticsoft.Model.SNS.TagType>();
                int num = 0;
                if (model.CategoryID.HasValue)
                {
                    categories2 = categories.GetModel(model.CategoryID.Value);
                    num = Globals.SafeInt(categories2.Path, 0);
                    string[] strArray = categories2.Path.Split(new char[] { '|' });
                    if (strArray.Length > 0)
                    {
                        num = Globals.SafeInt(strArray[0], 0);
                    }
                }
                Maticsoft.BLL.SNS.TagType type = new Maticsoft.BLL.SNS.TagType();
                modelList = type.GetModelList("Cid=" + num);
                if (((modelList != null) && (modelList.Count <= 0)) || (num == 0))
                {
                    modelList = type.GetModelList("Cid>0");
                }
                if ((modelList != null) && (modelList.Count > 0))
                {
                    return modelList[0].ID;
                }
            }
            return 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.BindTags();
            }
        }

        public string Id
        {
            get
            {
                return base.Request.QueryString["Id"];
            }
        }

        public string Type
        {
            get
            {
                return base.Request.QueryString["Type"];
            }
        }
    }
}

