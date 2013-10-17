namespace Maticsoft.BLL.SysManage
{
    using System;

    public class FolderItem
    {
        public string filename;
        public string filetype;
        public int number;
        public int size;

        public FolderItem(string filename, string filetype, int size, int number)
        {
            this.filename = filename;
            this.filetype = filetype;
            this.size = size;
            this.number = number;
        }
    }
}

