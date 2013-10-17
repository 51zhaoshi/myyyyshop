namespace Maticsoft.Web.Admin.SysManage
{
    using Maticsoft.Web;
    using Resources;
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.UI.WebControls;

    public class ClearCache : PageBaseAdmin
    {
        protected Button btnClear;
        protected Label Label1;
        protected Literal Literal1;
        protected Literal Literal2;
        protected Literal Literal3;

        protected void btnClear_Click(object sender, EventArgs e)
        {
            IDictionaryEnumerator enumerator = base.Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            StringBuilder builder = new StringBuilder();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key.ToString());
            }
            foreach (string str in list)
            {
                base.Cache.Remove(str);
                builder.Append("<li>" + str + "......OK! <br>");
            }
            this.Label1.Text = string.Format("<br>{0}<br>{1}", builder.ToString(), SysManage.lblClearSucceed);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override int Act_PageLoad
        {
            get
            {
                return 0x3e;
            }
        }
    }
}

