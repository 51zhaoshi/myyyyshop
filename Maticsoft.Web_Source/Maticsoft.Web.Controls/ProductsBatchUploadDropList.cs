namespace Maticsoft.Web.Controls
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ProductsBatchUploadDropList : UserControl
    {
        protected HiddenField hfSelectedNode;
        public bool IsNull;

        protected void Page_Load(object sender, EventArgs e)
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

