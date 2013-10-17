namespace Maticsoft.ViewModel.SNS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class Stars
    {
        private PagedList<ViewStar> _StarPagedList;
        public List<StarRank> HotStarList = new List<StarRank>();
        public List<ViewStar> StarNewList = new List<ViewStar>();
        public List<StarRank> StarRankList = new List<StarRank>();

        public List<ViewStar> StarList { get; set; }

        public List<ViewStar>[] StarList3ForCol { get; set; }

        public PagedList<ViewStar> StarPagedList
        {
            get
            {
                return this._StarPagedList;
            }
            set
            {
                List<ViewStar>[] list;
                int index;
                this._StarPagedList = value;
                if ((value != null) && (value.Count >= 1))
                {
                    list = new List<ViewStar>[] { new List<ViewStar>(), new List<ViewStar>(), new List<ViewStar>() };
                    index = 0;
                    value.ForEach(delegate (ViewStar Star) {
                        if (index == 3)
                        {
                            index = 0;
                        }
                        list[index++].Add(Star);
                    });
                    this.StarList3ForCol = list;
                }
            }
        }
    }
}

