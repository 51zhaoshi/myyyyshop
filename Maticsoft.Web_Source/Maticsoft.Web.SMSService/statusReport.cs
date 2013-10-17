namespace Maticsoft.Web.SMSService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, XmlType(Namespace="http://sdkhttp.eucp.b2m.cn/"), DesignerCategory("code"), GeneratedCode("System.Xml", "4.0.30319.1015"), DebuggerStepThrough]
    public class statusReport : INotifyPropertyChanged
    {
        private string errorCodeField;
        private string memoField;
        private string mobileField;
        private PropertyChangedEventHandler PropertyChanged;
        private string receiveDateField;
        private int reportStatusField;
        private long seqIDField;
        private string serviceCodeAddField;
        private string submitDateField;

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
        public string errorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
                this.RaisePropertyChanged("errorCode");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=1)]
        public string memo
        {
            get
            {
                return this.memoField;
            }
            set
            {
                this.memoField = value;
                this.RaisePropertyChanged("memo");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=2)]
        public string mobile
        {
            get
            {
                return this.mobileField;
            }
            set
            {
                this.mobileField = value;
                this.RaisePropertyChanged("mobile");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=3)]
        public string receiveDate
        {
            get
            {
                return this.receiveDateField;
            }
            set
            {
                this.receiveDateField = value;
                this.RaisePropertyChanged("receiveDate");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=4)]
        public int reportStatus
        {
            get
            {
                return this.reportStatusField;
            }
            set
            {
                this.reportStatusField = value;
                this.RaisePropertyChanged("reportStatus");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=5)]
        public long seqID
        {
            get
            {
                return this.seqIDField;
            }
            set
            {
                this.seqIDField = value;
                this.RaisePropertyChanged("seqID");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=6)]
        public string serviceCodeAdd
        {
            get
            {
                return this.serviceCodeAddField;
            }
            set
            {
                this.serviceCodeAddField = value;
                this.RaisePropertyChanged("serviceCodeAdd");
            }
        }

        [XmlElement(Form=XmlSchemaForm.Unqualified, Order=7)]
        public string submitDate
        {
            get
            {
                return this.submitDateField;
            }
            set
            {
                this.submitDateField = value;
                this.RaisePropertyChanged("submitDate");
            }
        }
    }
}

