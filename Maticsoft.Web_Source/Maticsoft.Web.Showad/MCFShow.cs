namespace Maticsoft.Web.Showad
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Web.UI;

    public class MCFShow : Page
    {
        public string strADContentHtml = "";
        public string strADID = "";
        public string strAutoStart = "";
        public string strDirection = "";
        public string strH = "";
        public string strStyle = "";
        public string strW = "";

        private string CreateAd(int AdTypeid, Maticsoft.Model.Settings.AdvertisePosition model, Maticsoft.BLL.Settings.Advertisement bll, string strADContent, int showType)
        {
            int num2;
            string str6;
            List<int> contentType = bll.GetContentType(model.AdvPositionId);
            if (contentType == null)
            {
                base.Response.Write("广告不存在。");
                return strADContent;
            }
            int num = -1;
            do
            {
                num2 = new Random().Next(0, 4);
            }
            while (!contentType.Contains(num2));
            num = num2;
            int? nullable = model.ShowType;
            if (nullable.HasValue)
            {
                int? nullable2 = model.ShowType;
                if ((nullable2.Value == 4) && !string.IsNullOrWhiteSpace(model.AdvHtml))
                {
                    num = 3;
                }
            }
            if (num == 0)
            {
                strADContent = bll.CreateTextTag(model.AdvPositionId, num);
                string str = "adtext.htm";
                this.strADContentHtml = this.ReadHtml(base.Server.MapPath(str)).Replace("<%=tabWidth%>", model.Width.ToString()).Replace("<%=tabHeight%>", model.Height.ToString()).Replace("<%=strADContent%>", strADContent).ToString();
                return strADContent;
            }
            if (num != 1)
            {
                switch (num)
                {
                    case 2:
                    {
                        string str7 = string.Empty;
                        if (bll.IsExist(model.AdvPositionId, num) > 1)
                        {
                            str7 = "adpic.htm";
                        }
                        else
                        {
                            str7 = "adsingle.htm";
                        }
                        string str8 = this.ReadHtml(base.Server.MapPath(str7));
                        string str9 = bll.CreateFlashTag(model.AdvPositionId, num);
                        str8 = str8.Replace("<%=tabWidth%>", model.Width.ToString()).Replace("<%=tabHeight%>", model.Height.ToString());
                        if (this.strAutoStart.Equals("0"))
                        {
                            str8 = str8.Replace("<%=tabStyle %>", "style=\"display:none;\"");
                        }
                        else
                        {
                            str8 = str8.Replace("<%=tabStyle %>", "");
                        }
                        this.strADContentHtml = str8.Replace("<%=strADContent%>", str9).ToString();
                        return strADContent;
                    }
                    case 3:
                        this.strW = model.Width.Value.ToString();
                        this.strH = model.Height.Value.ToString();
                        this.strADContentHtml = Globals.HtmlDecode(bll.GetDefindCode(model.AdvPositionId));
                        break;
                }
                return strADContent;
            }
            string path = string.Empty;
            string newValue = "";
            string str5 = "";
            switch (showType)
            {
                case 0:
                    path = "adshow.htm";
                    str5 = bll.CreatePicTag(model.AdvPositionId, num, true, null, null);
                    goto Label_034E;

                case 1:
                    path = "adUdshow.htm";
                    str5 = bll.CreatePicTag(model.AdvPositionId, num, true, model.RepeatColumns, null);
                    goto Label_034E;

                case 2:
                    if (!this.ADForProject.HasValue)
                    {
                        newValue = "<script src=\"moveleft.js\" type=\"text/javascript\"></script>";
                        path = "adshow.htm";
                        str5 = bll.CreatePicTag(model.AdvPositionId, num, true, null, null);
                    }
                    else
                    {
                        if (this.ADForProject.Value == 1)
                        {
                            path = "SNSadpic.htm";
                            str5 = bll.CreatePicTag(model.AdvPositionId, num, false, null, null);
                        }
                        if (this.ADForProject.Value == 2)
                        {
                            path = "SNSAlbumadpic.htm";
                            str5 = bll.CreatePicTag(model.AdvPositionId, num, false, null, new int?(this.ADForProject.Value));
                        }
                        if (this.ADForProject.Value == 3)
                        {
                            path = "TfxIndexAdPic.htm";
                            str5 = bll.CreatePicTag(model.AdvPositionId, num, false, null, new int?(this.ADForProject.Value));
                        }
                        if (this.ADForProject.Value == 4)
                        {
                            path = "TaoLeAdpic.htm";
                            str5 = bll.CreatePicTag(model.AdvPositionId, num, false, null, null);
                        }
                    }
                    goto Label_034E;

                case 3:
                    if (bll.IsExist(model.AdvPositionId, num) <= 1)
                    {
                        path = "adsingle.htm";
                        break;
                    }
                    path = "adpic.htm";
                    break;

                case 5:
                    strADContent = bll.CreateCodeTag(model.AdvPositionId, num);
                    path = "adcode.htm";
                    goto Label_034E;

                default:
                    goto Label_034E;
            }
            str5 = bll.CreatePicTag(model.AdvPositionId, num, false, null, null);
        Label_034E:
            str6 = this.ReadHtml(base.Server.MapPath(path));
            DataSet set = bll.GetTransitionImg(model.AdvPositionId, num, model.RepeatColumns);
            int count = 1;
            if (set != null)
            {
                count = set.Tables[0].Rows.Count;
            }
            if (showType == 1)
            {
                str6 = str6.Replace("<%=tabWidth%>", model.Width.ToString());
                this.strW = model.Width.ToString();
                int? height = model.Height;
                int num5 = count;
                str6 = str6.Replace("<%=tabHeight%>", ((height.HasValue ? new int?(height.GetValueOrDefault() * num5) : null) + 2).ToString());
                int? nullable32 = model.Height;
                int num6 = count;
                this.strH = ((nullable32.HasValue ? new int?(nullable32.GetValueOrDefault() * num6) : null) + 2).ToString();
            }
            else if (showType == 0)
            {
                int? width = model.Width;
                int num7 = count;
                str6 = str6.Replace("<%=tabWidth%>", ((width.HasValue ? new int?(width.GetValueOrDefault() * num7) : null) + 2).ToString());
                int? nullable42 = model.Width;
                int num4 = count;
                this.strW = ((nullable42.HasValue ? new int?(nullable42.GetValueOrDefault() * num4) : null) + 2).ToString();
                str6 = str6.Replace("<%=tabHeight%>", model.Height.ToString());
                this.strH = model.Height.ToString();
            }
            else
            {
                this.strW = model.Width.ToString();
                str6 = str6.Replace("<%=tabWidth%>", model.Width.ToString()).Replace("<%=tabHeight%>", model.Height.ToString());
                this.strH = model.Height.ToString();
            }
            if (this.strAutoStart.Equals("0"))
            {
                str6 = str6.Replace("<%=tabStyle %>", "style=\"display:none;\"");
            }
            else
            {
                str6 = str6.Replace("<%=tabStyle %>", "");
            }
            str6 = str6.Replace("<%=strADContent%>", str5).Replace("<%=tabScript %>", newValue);
            this.strADContentHtml = str6;
            return strADContent;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && !string.IsNullOrWhiteSpace(base.Request.Params["c"]))
            {
                if (!string.IsNullOrWhiteSpace(base.Request.Params["c"]))
                {
                    this.strADID = base.Request.Params["c"];
                }
                int adTypeid = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["t"]))
                {
                    adTypeid = Globals.SafeInt(base.Request.Params["t"], 0);
                }
                if (!string.IsNullOrWhiteSpace(base.Request.Params["a"]))
                {
                    this.strAutoStart = base.Request.Params["a"];
                }
                this.ShowAD(this.strADID, adTypeid);
            }
        }

        private string ReadHtml(string strPath)
        {
            StreamReader reader = new StreamReader(strPath);
            string str = "";
            ArrayList list = new ArrayList();
            while (str != null)
            {
                str = reader.ReadLine();
                if (str != null)
                {
                    list.Add(str);
                }
            }
            reader.Close();
            return string.Join(" ", (string[]) list.ToArray(typeof(string)));
        }

        private void ShowAD(string CallID, int AdTypeid)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.BLL.Settings.AdvertisePosition().GetModel(int.Parse(CallID));
            Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
            string strADContent = "";
            if (model != null)
            {
                strADContent = this.CreateAd(AdTypeid, model, bll, strADContent, model.ShowType.Value);
            }
            else
            {
                base.Response.Write("广告不存在。");
            }
        }

        public int? ADForProject
        {
            get
            {
                int num = 0;
                if (!string.IsNullOrWhiteSpace(base.Request.Params["p"]))
                {
                    num = Globals.SafeInt(base.Request.Params["p"], 0);
                }
                if (num == 0)
                {
                    return null;
                }
                return new int?(num);
            }
        }
    }
}

