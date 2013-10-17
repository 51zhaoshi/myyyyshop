namespace Maticsoft.Model.SNS
{
    using System;

    public class PostsSet
    {
        private bool _blog = true;
        private bool _narmal_pricture = true;
        private bool _normal_audio = true;
        private bool _normal_video = true;
        private bool _posttype_all = true;
        private bool _posttype_eachother = true;
        private bool _posttype_fellow = true;
        private bool _posttype_referme = true;
        private bool _posttype_user = true;
        private bool _pricture = true;
        private bool _product = true;

        public bool _Blog
        {
            get
            {
                return this._blog;
            }
            set
            {
                this._blog = value;
            }
        }

        public bool _Narmal_Audio
        {
            get
            {
                return this._normal_audio;
            }
            set
            {
                this._normal_audio = value;
            }
        }

        public bool _Narmal_Pricture
        {
            get
            {
                return this._narmal_pricture;
            }
            set
            {
                this._narmal_pricture = value;
            }
        }

        public bool _Narmal_Video
        {
            get
            {
                return this._normal_video;
            }
            set
            {
                this._normal_video = value;
            }
        }

        public bool _Picture
        {
            get
            {
                return this._pricture;
            }
            set
            {
                this._pricture = value;
            }
        }

        public bool _PostType_All
        {
            get
            {
                return this._posttype_all;
            }
            set
            {
                this._posttype_all = value;
            }
        }

        public bool _PostType_EachOther
        {
            get
            {
                return this._posttype_eachother;
            }
            set
            {
                this._posttype_eachother = value;
            }
        }

        public bool _PostType_Fellow
        {
            get
            {
                return this._posttype_fellow;
            }
            set
            {
                this._posttype_fellow = value;
            }
        }

        public bool _PostType_ReferMe
        {
            get
            {
                return this._posttype_referme;
            }
            set
            {
                this._posttype_referme = value;
            }
        }

        public bool _PostType_User
        {
            get
            {
                return this._posttype_user;
            }
            set
            {
                this._posttype_user = value;
            }
        }

        public bool _Product
        {
            get
            {
                return this._product;
            }
            set
            {
                this._product = value;
            }
        }
    }
}

