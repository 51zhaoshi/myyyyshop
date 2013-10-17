namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class GroupSearch
    {
        public List<Groups> HotList { get; set; }

        public List<Groups> RecommandList { get; set; }

        public PagedList<Groups> SearchList { get; set; }
    }
}

