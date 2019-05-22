using System;
using System.Collections.Generic;
using System.Text;

namespace MappingTools.Generator
{
    public interface IClassLogicWriter
    {
        string ClassLogicNameSpace
        {
            set;
        }

        string ClassLogicName
        {
            set;
        }

        string ClassNameSpace
        {
            set;
        }

        string ClassName
        {
            set;
        }

        void AppendProperty(string propertyName, Type propertyType, bool isKey);
        void ClearProperty();

        void WriteOut();
    }
}
