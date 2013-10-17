namespace Maticsoft.OAuth
{
    using System;

    public class Parameter
    {
        private string name;
        private string value;

        public Parameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string toParamString()
        {
            return (this.name + "=" + this.value);
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
    }
}

