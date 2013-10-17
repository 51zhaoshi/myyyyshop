namespace Maticsoft.Common
{
    using System;
    using System.Collections;
    using System.Xml;

    public class XmlHelper
    {
        private XmlDocument xmldoc;
        private XmlElement xmlelem;
        private XmlNode xmlnode;

        public bool CreateXmlDocument(string FileName, string rootName, string Encode)
        {
            try
            {
                this.xmldoc = new XmlDocument();
                XmlDeclaration newChild = this.xmldoc.CreateXmlDeclaration("1.0", Encode, null);
                this.xmldoc.AppendChild(newChild);
                this.xmlelem = this.xmldoc.CreateElement("", rootName, "");
                this.xmldoc.AppendChild(this.xmlelem);
                this.xmldoc.Save(FileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteNodes(string XmlFile, string fatherNode)
        {
            bool flag;
            try
            {
                this.xmldoc = new XmlDocument();
                this.xmldoc.Load(XmlFile);
                this.xmlnode = this.xmldoc.SelectSingleNode(fatherNode);
                this.xmlnode.RemoveAll();
                this.xmldoc.Save(XmlFile);
                flag = true;
            }
            catch (XmlException exception)
            {
                throw new XmlException(exception.Message);
            }
            return flag;
        }

        public bool InsertNode(string XmlFile, string NewNodeName, bool HasAttributes, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            bool flag;
            try
            {
                this.xmldoc = new XmlDocument();
                this.xmldoc.Load(XmlFile);
                XmlNode node = this.xmldoc.SelectSingleNode(fatherNode);
                this.xmlelem = this.xmldoc.CreateElement(NewNodeName);
                if ((htAtt != null) && HasAttributes)
                {
                    this.SetAttributes(this.xmlelem, htAtt);
                    this.SetNodes(this.xmlelem.Name, this.xmldoc, this.xmlelem, htSubNode);
                }
                else
                {
                    this.SetNodes(this.xmlelem.Name, this.xmldoc, this.xmlelem, htSubNode);
                }
                node.AppendChild(this.xmlelem);
                this.xmldoc.Save(XmlFile);
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        private void SetAttributes(XmlElement xe, Hashtable htAttribute)
        {
            foreach (DictionaryEntry entry in htAttribute)
            {
                xe.SetAttribute(entry.Key.ToString(), entry.Value.ToString());
            }
        }

        private void SetNodes(string rootNode, XmlDocument XmlDoc, XmlElement rootXe, Hashtable SubNodes)
        {
            foreach (DictionaryEntry entry in SubNodes)
            {
                this.xmlnode = XmlDoc.SelectSingleNode(rootNode);
                XmlElement newChild = XmlDoc.CreateElement(entry.Key.ToString());
                newChild.InnerText = entry.Value.ToString();
                rootXe.AppendChild(newChild);
            }
        }

        public bool UpdateNode(string XmlFile, string fatherNode, Hashtable htAtt, Hashtable htSubNode)
        {
            bool flag;
            try
            {
                this.xmldoc = new XmlDocument();
                this.xmldoc.Load(XmlFile);
                XmlNodeList childNodes = this.xmldoc.SelectSingleNode(fatherNode).ChildNodes;
                this.UpdateNodes(childNodes, htAtt, htSubNode);
                this.xmldoc.Save(XmlFile);
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }

        private void UpdateNodes(XmlNodeList root, Hashtable htAtt, Hashtable htSubNode)
        {
            foreach (XmlNode node in root)
            {
                this.xmlelem = (XmlElement) node;
                if (this.xmlelem.HasAttributes)
                {
                    foreach (DictionaryEntry entry in htAtt)
                    {
                        if (this.xmlelem.HasAttribute(entry.Key.ToString()))
                        {
                            this.xmlelem.SetAttribute(entry.Key.ToString(), entry.Value.ToString());
                        }
                    }
                }
                if (this.xmlelem.HasChildNodes)
                {
                    foreach (XmlNode node2 in this.xmlelem.ChildNodes)
                    {
                        XmlElement element = (XmlElement) node2;
                        foreach (DictionaryEntry entry2 in htSubNode)
                        {
                            if (element.Name == entry2.Key.ToString())
                            {
                                element.InnerText = entry2.Value.ToString();
                            }
                        }
                    }
                    continue;
                }
            }
        }
    }
}

