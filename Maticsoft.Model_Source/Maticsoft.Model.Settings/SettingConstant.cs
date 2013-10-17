namespace Maticsoft.Model.Settings
{
    using System;
    using System.Drawing;

    public static class SettingConstant
    {
        public const string PRODUCT_NORMAL_SIZE_KEY = "ProductNormalImageSize";
        public const string PRODUCT_THUMB_SIZE_KEY = "ProductThumbImageSize";
        public static readonly Size ProductNormalSize = new Size(300, 300);
        public static readonly Size ProductThumbSize = new Size(0x7f, 0x7f);
    }
}

