namespace Maticsoft.Components.Setting
{
    using System;

    public interface IPageSetting
    {
        void Replace(params string[][] values);
        string ReplaceDescription(params string[][] values);
        string ReplaceKeywords(params string[][] values);
        string ReplaceTitle(params string[][] values);

        string Description { get; set; }

        string Keywords { get; set; }

        string Title { get; set; }
    }
}

