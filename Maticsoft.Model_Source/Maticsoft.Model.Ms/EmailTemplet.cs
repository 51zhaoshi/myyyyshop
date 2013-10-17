namespace Maticsoft.Model.Ms
{
    using System;

    [Serializable]
    public class EmailTemplet
    {
        private string _emailbody;
        private string _emaildescription;
        private int _emailpriority;
        private string _emailsubject;
        private string _emailtype;
        private string _tagdescription;
        private int _templetid;

        public string EmailBody
        {
            get
            {
                return this._emailbody;
            }
            set
            {
                this._emailbody = value;
            }
        }

        public string EmailDescription
        {
            get
            {
                return this._emaildescription;
            }
            set
            {
                this._emaildescription = value;
            }
        }

        public int EmailPriority
        {
            get
            {
                return this._emailpriority;
            }
            set
            {
                this._emailpriority = value;
            }
        }

        public string EmailSubject
        {
            get
            {
                return this._emailsubject;
            }
            set
            {
                this._emailsubject = value;
            }
        }

        public string EmailType
        {
            get
            {
                return this._emailtype;
            }
            set
            {
                this._emailtype = value;
            }
        }

        public string TagDescription
        {
            get
            {
                return this._tagdescription;
            }
            set
            {
                this._tagdescription = value;
            }
        }

        public int TempletId
        {
            get
            {
                return this._templetid;
            }
            set
            {
                this._templetid = value;
            }
        }
    }
}

