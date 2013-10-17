namespace Maticsoft.Controls
{
    using System;
    using System.Web.UI.WebControls;

    public class SortImageColumn : ImageField
    {
        private string fallUrl = (Globals.ApplicationPath + "/Images/pics/desc.png");
        private string riseUrl = (Globals.ApplicationPath + "/Images/pics/asc.png");

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);
            if (cell == null)
            {
                throw new ArgumentNullException("cell");
            }
            ImageButton button3 = new ImageButton {
                ID = "rise",
                ImageUrl = this.RiseUrl,
                CommandName = "Rise"
            };
            ImageButton child = button3;
            ImageButton button4 = new ImageButton {
                ID = "fall",
                ImageUrl = this.FallUrl,
                CommandName = "Fall"
            };
            ImageButton button2 = button4;
            if (cellType == DataControlCellType.DataCell)
            {
                cell.Controls.Add(button2);
                cell.Controls.Add(child);
            }
        }

        public string FallUrl
        {
            get
            {
                return this.fallUrl;
            }
            set
            {
                this.fallUrl = value;
            }
        }

        public string RiseUrl
        {
            get
            {
                return this.riseUrl;
            }
            set
            {
                this.riseUrl = value;
            }
        }
    }
}

