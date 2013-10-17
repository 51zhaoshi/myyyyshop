namespace Maticsoft.ViewModel.CMS
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Webdiyer.WebControls.Mvc;

    public class Photo
    {
        private PagedList<Photo> _PhotoPagedList;

        public List<PhotoClass> PhotoClassList { get; set; }

        public List<Photo>[] PhotoList4ForCol { get; set; }

        public List<Photo> PhotoListWaterfall { get; set; }

        public PagedList<Photo> PhotoPagedList
        {
            get
            {
                return this._PhotoPagedList;
            }
            set
            {
                List<Photo>[] list;
                int index;
                this._PhotoPagedList = value;
                if ((value != null) && (value.Count >= 1))
                {
                    list = new List<Photo>[] { new List<Photo>(), new List<Photo>(), new List<Photo>(), new List<Photo>() };
                    index = 0;
                    value.ForEach(delegate (Photo Photo) {
                        if (index == 4)
                        {
                            index = 0;
                        }
                        list[index++].Add(Photo);
                    });
                    this.PhotoList4ForCol = list;
                }
            }
        }
    }
}

