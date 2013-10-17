namespace Maticsoft.BLL.SNS
{
    using Maticsoft.BLL.SysManage;
    using Maticsoft.Common;
    using Maticsoft.Model.SNS;
    using System;

    public class ConfigSystem : Maticsoft.BLL.SysManage.ConfigSystem
    {
        public static PostsSet GetPostSetByCache()
        {
            string cacheKey = "ConfigSystemPostSetting";
            object cache = DataCache.GetCache(cacheKey);
            if (cache == null)
            {
                try
                {
                    PostsSet set = new PostsSet {
                        _Narmal_Pricture = PostSetHelper("SNS_Narmal_Pricture_IsShow"),
                        _Narmal_Audio = PostSetHelper("SNS_Narmal_Audio_IsShow"),
                        _Narmal_Video = PostSetHelper("SNS_Narmal_Video_IsShow"),
                        _Picture = PostSetHelper("SNS_Picture_IsShow"),
                        _Product = PostSetHelper("SNS_Product_IsShow"),
                        _PostType_All = PostSetHelper("SNS_PostType_All_IsShow"),
                        _PostType_EachOther = PostSetHelper("_PostType_EachOther_IsShow"),
                        _PostType_Fellow = PostSetHelper("SNS_PostType_Fellow_IsShow"),
                        _PostType_ReferMe = PostSetHelper("SNS_PostType_ReferMe_IsShow"),
                        _PostType_User = PostSetHelper("SNS_PostType_User_IsShow")
                    };
                    cache = set;
                    if (cache != null)
                    {
                        int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValue("CacheTime"), 30);
                        DataCache.SetCache(cacheKey, cache, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
                    }
                }
                catch
                {
                }
            }
            return (PostsSet) cache;
        }

        public static bool PostSetHelper(string name)
        {
            string text = Maticsoft.BLL.SysManage.ConfigSystem.GetValue(name);
            return ((text == null) || ((text != null) && (Globals.SafeInt(text, -1) == 1)));
        }

        public static void UpdatePostSet(PostsSet model)
        {
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Narmal_Pricture_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Narmal_Pricture_IsShow", model._Narmal_Pricture ? "1" : "0", "微博上传图片是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Narmal_Pricture_IsShow", model._Narmal_Pricture ? "1" : "0", "微博上传图片是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Narmal_Audio_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Narmal_Audio_IsShow", model._Narmal_Audio ? "1" : "0", "微博上传音乐是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Narmal_Audio_IsShow", model._Narmal_Audio ? "1" : "0", "微博上传音乐是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Narmal_Video_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Narmal_Video_IsShow", model._Narmal_Video ? "1" : "0", "微博上传视频是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Narmal_Video_IsShow", model._Narmal_Video ? "1" : "0", "微博上传视频是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Picture_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Picture_IsShow", model._Picture ? "1" : "0", "上传图片模块是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Picture_IsShow", model._Picture ? "1" : "0", "上传图片模块是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Blog_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Blog_IsShow", model._Blog ? "True" : "False", "发表文章");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Blog_IsShow", model._Blog ? "True" : "False", "发表文章");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_Product_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_Product_IsShow", model._Product ? "1" : "0", "上传商品模块是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_Product_IsShow", model._Product ? "1" : "0", "上传商品模块是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostType_All_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostType_All_IsShow", model._PostType_All ? "1" : "0", "全部微博是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostType_All_IsShow", model._PostType_All ? "1" : "0", "全部微博是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostType_EachOther_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostType_EachOther_IsShow", model._PostType_EachOther ? "1" : "0", "互相关注的微博是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostType_EachOther_IsShow", model._PostType_EachOther ? "1" : "0", "互相关注的微博是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostType_Fellow_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostType_Fellow_IsShow", model._PostType_Fellow ? "1" : "0", "我关注的微博是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostType_Fellow_IsShow", model._PostType_Fellow ? "1" : "0", "我关注的微博是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostType_ReferMe_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostType_ReferMe_IsShow", model._PostType_ReferMe ? "1" : "0", "提到我的微博是否显示");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostType_ReferMe_IsShow", model._PostType_ReferMe ? "1" : "0", "提到我的微博是否显示");
            }
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("SNS_PostType_User_IsShow"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("SNS_PostType_User_IsShow", model._PostType_User ? "1" : "0", "我发表的");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("SNS_PostType_User_IsShow", model._PostType_User ? "1" : "0", "我发表的");
            }
            string cacheKey = "ConfigSystemPostSetting";
            if (DataCache.GetCache(cacheKey) != null)
            {
                DataCache.DeleteCache("ConfigSystemPostSetting");
            }
            object objObject = model;
            int num = Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValue("CacheTime"), 30);
            DataCache.SetCache(cacheKey, objObject, DateTime.Now.AddMinutes((double) num), TimeSpan.Zero);
        }
    }
}

