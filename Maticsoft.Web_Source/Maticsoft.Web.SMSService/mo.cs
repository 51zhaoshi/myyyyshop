namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, DebuggerStepThrough, XmlType(Namespace="http://sdkhttp.eucp.b2m.cn/"), DesignerCategory("code"), GeneratedCode("System.Xml", "4.0.30319.1015")]
    public class mo : INotifyPropertyChanged
    {
        private string addSerialField;
        private string addSerialRevField;
        private string channelnumberField;
        private string mobileNumberField;
        private PropertyChangedEventHandler PropertyChanged;
        private string sentTimeField;
        private string smsContentField;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                PropertyChangedEventHandler handler2;
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                do
                {
                    handler2 = propertyChanged;
                    PropertyChangedEventHandler handler3 = (PropertyChangedEventHandler) Delegate.Combine(handler2, value);
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, handler2);
                }
                while (propertyChanged != handler2);
            }
            remove
            {
                PropertyChangedEventHandler handler2;
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                do
                {
                    handler2 = propertyChanged;
                    PropertyChangedEventHandler handler3 = (PropertyChangedEventHandler) Delegate.Remove(handler2, value);
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, handler2);
                }
                while (propertyChanged != handler2);
            }
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=0)]
        public string addSerial
        {
            get
            {
                return this.addSerialField;
            }
            set
            {
                this.addSerialField = value;
                this.RaisePropertyChanged("addSerial");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=1)]
        public string addSerialRev
        {
            get
            {
                return this.addSerialRevField;
            }
            set
            {
                this.addSerialRevField = value;
                this.RaisePropertyChanged("addSerialRev");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=2)]
        public string channelnumber
        {
            get
            {
                return this.channelnumberField;
            }
            set
            {
                this.channelnumberField = value;
                this.RaisePropertyChanged("channelnumber");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=3)]
        public string mobileNumber
        {
            get
            {
                return this.mobileNumberField;
            }
            set
            {
                this.mobileNumberField = value;
                this.RaisePropertyChanged("mobileNumber");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=4)]
        public string sentTime
        {
            get
            {
                return this.sentTimeField;
            }
            set
            {
                this.sentTimeField = value;
                this.RaisePropertyChanged("sentTime");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=5)]
        public string smsContent
        {
            get
            {
                return this.smsContentField;
            }
            set
            {
                this.smsContentField = value;
                this.RaisePropertyChanged("smsContent");
            }
        }
    }
}

