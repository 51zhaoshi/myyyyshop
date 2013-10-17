namespace Maticsoft.Web.Controls
{
    using Resources;
    using System;

    public class GridViewUIText : IGridViewUIText
    {
        public string ExportExcel
        {
            get
            {
                return Site.GVTextExportExcel;
            }
        }

        public string ExportWord
        {
            get
            {
                return Site.GVTextExportWord;
            }
        }

        public string First
        {
            get
            {
                return Site.GVTextFirst;
            }
        }

        public string Last
        {
            get
            {
                return Site.GVTextLast;
            }
        }

        public string Next
        {
            get
            {
                return Site.GVTextNext;
            }
        }

        public string Page
        {
            get
            {
                return Site.GVTextPage;
            }
        }

        public string Previous
        {
            get
            {
                return Site.GVTextPrevious;
            }
        }

        public string Record
        {
            get
            {
                return Site.GVTextRecord;
            }
        }
    }
}

