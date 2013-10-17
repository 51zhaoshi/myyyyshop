namespace Maticsoft.Model.Poll
{
    using System;

    [Serializable]
    public class PollUsers
    {
        private int? _age;
        private string _email;
        private byte[] _password;
        private string _phone;
        private string _sex;
        private string _truename;
        private int _userid;
        private string _username;
        private string _usertype;

        public int? Age
        {
            get
            {
                return this._age;
            }
            set
            {
                this._age = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public byte[] Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public string Phone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
            }
        }

        public string Sex
        {
            get
            {
                return this._sex;
            }
            set
            {
                this._sex = value;
            }
        }

        public string TrueName
        {
            get
            {
                return this._truename;
            }
            set
            {
                this._truename = value;
            }
        }

        public int UserID
        {
            get
            {
                return this._userid;
            }
            set
            {
                this._userid = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public string UserType
        {
            get
            {
                return this._usertype;
            }
            set
            {
                this._usertype = value;
            }
        }
    }
}

