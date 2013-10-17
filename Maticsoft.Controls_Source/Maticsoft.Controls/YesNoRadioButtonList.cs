namespace Maticsoft.Controls
{
    using System;
    using System.Web;
    using System.Web.UI.WebControls;

    public class YesNoRadioButtonList : RadioButtonList
    {
        public YesNoRadioButtonList()
        {
            this.Items.Clear();
            this.Items.Add(new ListItem((string) HttpContext.GetGlobalResourceObject("Resources", "Yes"), "True"));
            this.Items.Add(new ListItem((string) HttpContext.GetGlobalResourceObject("Resources", "No"), "False"));
            this.RepeatDirection = RepeatDirection.Horizontal;
            this.SelectedValue = true;
        }

        public bool SelectedValue
        {
            get
            {
                return bool.Parse(base.SelectedValue);
            }
            set
            {
                base.SelectedIndex = base.Items.IndexOf(base.Items.FindByValue(value ? "True" : "False"));
            }
        }
    }
}

