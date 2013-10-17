namespace Maticsoft.Web.Admin.Settings.FilterWord
{
    using Maticsoft.BLL.Settings;
    using Maticsoft.Common;
    using Maticsoft.Model.Settings;
    using Maticsoft.Web;
    using System;
    using System.Web.UI.WebControls;

    public class Add : PageBaseAdmin
    {
        protected Button btnCancle;
        protected Button btnSave;
        protected Literal Literal1;
        protected Literal Literal2;
        protected TextBox txtWords;

        public void btnCancle_Click(object sender, EventArgs e)
        {
            base.Response.Redirect("list.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtWords.Text))
            {
                MessageBox.ShowFailTip(this, "请输入敏感词！");
            }
            else
            {
                Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords();
                Maticsoft.BLL.Settings.FilterWords words2 = new Maticsoft.BLL.Settings.FilterWords();
                foreach (string str in this.txtWords.Text.TrimEnd(new char[0]).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (str.IndexOf("=") < 0)
                    {
                        MessageBox.ShowFailTip(this, "输入的字符格式不正确！");
                        return;
                    }
                    string[] strArray2 = str.Split(new char[] { '=' });
                    string wordPattern = strArray2[0];
                    string str3 = strArray2[1];
                    model.WordPattern = wordPattern;
                    switch (str3)
                    {
                        case "{BANNED}":
                            model.ActionType = 0;
                            model.RepalceWord = "";
                            break;

                        case "{MOD}":
                            model.ActionType = 1;
                            model.RepalceWord = "";
                            break;

                        case "{REPLACE}":
                            model.ActionType = 2;
                            model.RepalceWord = string.IsNullOrWhiteSpace(str3) ? "**" : str3;
                            break;
                    }
                    Maticsoft.Model.Settings.FilterWords byWordPattern = words2.GetByWordPattern(wordPattern);
                    if (byWordPattern != null)
                    {
                        words2.Delete(byWordPattern.FilterId);
                    }
                    words2.Add(model);
                }
                words2.ClearCache();
                MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x177;
            }
        }
    }
}

