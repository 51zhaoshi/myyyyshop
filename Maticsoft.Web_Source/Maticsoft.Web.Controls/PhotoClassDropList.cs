namespace Maticsoft.Web.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class PhotoClassDropList : UserControl
    {
        protected HiddenField hfSelectedNode;
        public bool IsNull;

        public void Page_Load(object sender, EventArgs e)
        {
        }

        public string SelectedValue
        {
            get
            {
                return this.hfSelectedNode.Value;
            }
            set
            {
                this.hfSelectedNode.Value = value;
            }
        }
    }
}

