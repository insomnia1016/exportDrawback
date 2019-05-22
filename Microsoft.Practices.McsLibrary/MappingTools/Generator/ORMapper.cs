using System;
using System.Collections.Generic;
using System.Text;

namespace MappingTools.Generator
{
    public class ORMapper
    {
        private string _nameSpace;
        private string _assemblyName;
        private string _logicNameSpace;

        public string NameSpace
        {
            set { _nameSpace = value; }
        }

        public string AssemblyName
        {
            set { _assemblyName = value; }
        }

        public string LogicNameSpace
        {
            set { _logicNameSpace = value; }
        }

        public void Generating(ITableReader tableReader, IClassWriter classWriter, IClassLogicWriter classLogicWriter, IMapWriter mapWriter)
        {
            tableReader.Close();
            classWriter.ClearProperty();
            mapWriter.ClearItem();

            if (classLogicWriter != null) classLogicWriter.ClearProperty();

            string className = FileMedia.ENTITY_CLASS_PRE + Misc.GetPublicName(tableReader.TableName);

            classWriter.NameSpace = _nameSpace;
            classWriter.ClassName = className;

            if (classLogicWriter != null)
            {
                classLogicWriter.ClassNameSpace = _nameSpace;
                classLogicWriter.ClassName = className;
                classLogicWriter.ClassLogicNameSpace = _logicNameSpace;
                classLogicWriter.ClassLogicName = className + "Helper";
            }

            mapWriter.ClassName = string.Format("{0}.{1}", _nameSpace, className);
            mapWriter.AssemblyName = _assemblyName;
            mapWriter.TableName = tableReader.TableName;

            try
            {
                tableReader.Open();

                while (tableReader.Read())
                {
                    TableColumn tableColumn = tableReader.CurrentColumn().Value;
                    string propertyName = Misc.GetPublicName(tableColumn.Name);
                    classWriter.AppendProperty(propertyName, tableColumn, tableColumn.DotNetType);

                    if (classLogicWriter != null) classLogicWriter.AppendProperty(propertyName, tableColumn.DotNetType, tableColumn.IsKey);

                    mapWriter.AppendItem(new MapItem(propertyName, tableColumn.Name, tableColumn.DatabaseType, tableColumn.IsKey, tableColumn.CanBeNull, tableColumn.MaxLength, true, true));
                }

                classWriter.WriteOut();

                if (classLogicWriter != null) classLogicWriter.WriteOut();

                mapWriter.WriteOut();
            }
            finally
            {
                tableReader.Close();
            }
        }

        internal void Generating(ITableReader tableReader, IClassWriter classWriter)
        {
            tableReader.Close();
            classWriter.ClearProperty();

            string className = FileMedia.QUERY_CLASS_PRE + Misc.GetPublicName(tableReader.TableName);
            classWriter.ClassName = className;

            if (string.IsNullOrEmpty(classWriter.NameSpace))
                classWriter.NameSpace = _nameSpace;

            try
            {
                tableReader.Open();

                while (tableReader.Read())
                {
                    TableColumn tableColumn = tableReader.CurrentColumn().Value;
                    string propertyName = Misc.GetPublicName(tableColumn.Name);
                    classWriter.AppendProperty(propertyName, tableColumn, tableColumn.DotNetType);
                }

                classWriter.WriteOut();
            }
            finally
            {
                tableReader.Close();
            }
        }
    }
}
