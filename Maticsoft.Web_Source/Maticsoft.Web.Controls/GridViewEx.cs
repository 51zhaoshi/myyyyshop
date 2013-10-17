namespace Maticsoft.Web.Controls
{
    using System;
    using System.Web.UI;

    [ToolboxData("<{0}:GridViewEx runat='server'></{0}:GridViewEx>")]
    public class GridViewEx : GridViewExBase
    {
        public GridViewEx() : base(new GridViewUIText())
        {
        }
    }
}

