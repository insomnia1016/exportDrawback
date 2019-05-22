using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace MappingTools.Generator
{
    public class XmlMapWriter : FileMedia, IMapWriter
    {
        private string _assemblyName;
        private string _className;
        private string _tableName;

        private ArrayList _keyItems = ArrayList.Synchronized(new ArrayList());
        private ArrayList _memberItems = ArrayList.Synchronized(new ArrayList());

        public XmlMapWriter()
        {
        }

        public XmlMapWriter(string basePath)
        {
            BasePath = basePath;
        }

        #region IMapWriter Members

        public string AssemblyName
        {
            set { _assemblyName = value; }
        }

        public string ClassName
        {
            set { _className = value; }
        }

        public string TableName
        {
            set { _tableName = value; }
        }

        public void AppendItem(MapItem item)
        {
            ArrayList list = item.IsKeyColumn ? _keyItems : _memberItems;

            list.Add(item);
        }

        public void ClearItem()
        {
            _keyItems.Clear();
            _memberItems.Clear();
        }

        public void WriteOut()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<?xml version=""1.0""?><ORMapping/>");

            XmlElement root = doc.DocumentElement;

            root.Attributes.Append(NewAttribute(doc, "tableName", _tableName));
            root.Attributes.Append(NewAttribute(doc, "className", _className));
            root.Attributes.Append(NewAttribute(doc,"displayName",Misc.GetComment(_tableName)));

            if (_assemblyName != null && _assemblyName.Length > 0) root.Attributes.Append(NewAttribute(doc, "assemblyName", _assemblyName));

            foreach (MapItem item in _keyItems)
            {
                root.AppendChild(NewItemNode(doc, item));
            }

            foreach (MapItem item in _memberItems)
            {
                root.AppendChild(NewItemNode(doc, item));
            }

            string fileName = string.Format("{0}\\{1}.xml", _basePath, _tableName);

            using (XmlTextWriter writer = new XmlTextWriter(fileName, null))
            {
                writer.Formatting = Formatting.Indented;
                doc.Save(writer);
            }
        }

        #endregion

        private XmlAttribute NewAttribute(XmlDocument doc, string attrName, string attrValue)
        {
            XmlAttribute attr = doc.CreateAttribute(attrName);
            attr.Value = attrValue;
            return attr;
        }

        private XmlElement NewElement(XmlDocument doc, string elementName, string elementValue)
        {
            XmlElement element = doc.CreateElement(elementName);
            element.InnerText = elementValue;
            return element;
        }

        private XmlElement NewItemNode(XmlDocument doc, MapItem item)
        {
            XmlElement element = doc.CreateElement(item.IsKeyColumn ? "KeyItem" : "MemberItem");

            element.AppendChild(NewPropertyNode(doc, item));
            element.AppendChild(NewColumnNode(doc, item));
            element.AppendChild(NewTransferNode(doc, item));

            return element;
        }

        private XmlElement NewPropertyNode(XmlDocument doc, MapItem item)
        {
            XmlElement element = doc.CreateElement("Property");

            element.AppendChild(NewElement(doc, "Name", item.PropertyName));   
            string comment = Misc.GetComment(_tableName,item.ColumnName);
            string displayName = Misc.GetDisplayName(comment);
            element.AppendChild(NewElement(doc,"DisplayName",displayName));
            element.AppendChild(NewElement(doc, "Comment", comment));
            return element;

        }

        private XmlElement NewColumnNode(XmlDocument doc, MapItem item)
        {
            XmlElement element = doc.CreateElement("Column");

            element.AppendChild(NewElement(doc, "Name", item.ColumnName));
            element.AppendChild(NewElement(doc, "Type", Enum.GetName(typeof(DbType), item.ColumnType)));
            element.AppendChild(NewElement(doc, "MaxLength", item.MaxLength.ToString()));
            element.AppendChild(NewElement(doc, "CanBeNull", item.CanBeNull.ToString()));

            return element;
        }

        private XmlElement NewTransferNode(XmlDocument doc, MapItem item)
        {
            XmlElement element = doc.CreateElement("Transfer");

            element.AppendChild(NewElement(doc, "PropertyToColumn", item.PropertyToColumn.ToString()));
            element.AppendChild(NewElement(doc, "ColumnToProperty", item.ColumnToProperty.ToString()));
            element.AppendChild(NewElement(doc, "IsLogModify", "True"));
            return element;
        }
    }
}
