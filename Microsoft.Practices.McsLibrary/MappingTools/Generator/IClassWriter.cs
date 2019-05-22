using System;
using System.Collections.Generic;
using System.Text;

namespace MappingTools.Generator
{
    public interface IClassWriter
    {
        string NameSpace
        {
            set;
            get;
        }

        string ClassName
        {
            set;
            get;
        }

        string TableName
        {
            set;
            get;
        }
       
        void AppendProperty(string propertyName, TableColumn col,Type propertyType);
        void RemoveProperty(string propertyName);
        void ClearProperty();

        void WriteOut();
    }
}
