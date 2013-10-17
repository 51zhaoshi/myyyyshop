namespace Maticsoft.Web.Admin.Shop.Tags
{
    using Maticsoft.BLL.Shop.Tags;
    using Maticsoft.Common;
    using Maticsoft.Json;
    using Maticsoft.Model.Shop.Tags;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.UI;

    public class SelectTagsCategory : Page
    {
        private Maticsoft.BLL.Shop.Tags.TagCategories manage = new Maticsoft.BLL.Shop.Tags.TagCategories();

        private void DoCallback()
        {
            string str = base.Request.Form["Action"];
            base.Response.Clear();
            base.Response.ContentType = "application/json";
            string s = string.Empty;
            string str3 = str;
            if (str3 != null)
            {
                if (!(str3 == "GetInfo"))
                {
                    if (str3 == "GetList")
                    {
                        s = this.GetCategoriesList();
                    }
                }
                else
                {
                    s = this.GetCategoryInfo();
                }
            }
            base.Response.Write(s);
            base.Response.End();
        }

        private string GetCategoriesList()
        {
            JsonObject obj2 = new JsonObject();
            int parentCategoryId = Globals.SafeInt(base.Request.Form["ParentCategoryId"], -1);
            if (parentCategoryId < 0)
            {
                obj2.Put("ERROR", "NOPARENTCATEGORYID");
                return obj2.ToString();
            }
            List<Maticsoft.Model.Shop.Tags.TagCategories> categorysByParentId = this.manage.GetCategorysByParentId(parentCategoryId);
            if ((categorysByParentId == null) || (categorysByParentId.Count < 1))
            {
                obj2.Put("STATUS", "NODATA");
                return obj2.ToString();
            }
            categorysByParentId.Sort((Comparison<Maticsoft.Model.Shop.Tags.TagCategories>) ((x, y) => x.DisplaySequence.CompareTo(y.DisplaySequence)));
            obj2.Put("STATUS", "OK");
            JsonArray data = new JsonArray();
            categorysByParentId.ForEach(delegate (Maticsoft.Model.Shop.Tags.TagCategories info) {
                data.Add(new JsonObject(new string[] { "CategoryId", "HasChildren", "CategoryName" }, new object[] { info.ID.ToString(CultureInfo.InvariantCulture), info.HasChildren, info.CategoryName }));
            });
            obj2.Put("DATA", data);
            return obj2.ToString();
        }

        private string GetCategoryInfo()
        {
            JsonObject obj2 = new JsonObject();
            int iD = Globals.SafeInt(base.Request.Form["CategoryId"], -1);
            if (iD < 1)
            {
                obj2.Put("ERROR", "NOCATEGORYID");
                return obj2.ToString();
            }
            Maticsoft.Model.Shop.Tags.TagCategories model = this.manage.GetModel(iD);
            if (model == null)
            {
                obj2.Put("STATUS", "NODATA");
                return obj2.ToString();
            }
            obj2.Put("STATUS", "OK");
            obj2.Put("DATA", model);
            return obj2.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(base.Request.Form["Callback"]) && (base.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
        }
    }
}

