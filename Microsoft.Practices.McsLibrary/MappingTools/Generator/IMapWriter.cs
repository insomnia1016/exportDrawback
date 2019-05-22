using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MappingTools.Generator
{
    public struct MapItem
    {
        public string PropertyName;

        public string ColumnName;
        public DbType ColumnType;
        public bool IsKeyColumn;
        public bool CanBeNull;
        public int MaxLength;

        public bool PropertyToColumn;
        public bool ColumnToProperty;

        public MapItem(string propertyName, string columnName, DbType columnType, bool isKeyColumn, bool canBeNull, int maxLength, bool p2c, bool c2p)
        {
            PropertyName = propertyName;

            ColumnName = columnName;
            ColumnType = columnType;
            IsKeyColumn = isKeyColumn;
            CanBeNull = canBeNull;
            MaxLength = maxLength;

            PropertyToColumn = p2c;
            ColumnToProperty = c2p;
        }
    }

    public interface IMapWriter
    {
        string AssemblyName
        {
            set;
        }

        string ClassName
        {
            set;
        }

        string TableName
        {
            set;
        }

        void AppendItem(MapItem item);
        void ClearItem();

        void WriteOut();
    }
}
