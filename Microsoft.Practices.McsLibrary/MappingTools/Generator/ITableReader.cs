using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MappingTools.Generator
{
    public struct TableColumn
    {
        public string Name;
        public DbType DatabaseType;
        public Type DotNetType;
        public int MaxLength;
        public bool IsKey;
        public bool CanBeNull;

        public TableColumn(string name, DbType databaseType, Type dotNetType, int maxLength, bool isKey, bool canBeNull)
        {
            Name = name;
            DatabaseType = databaseType;
            DotNetType = dotNetType;
            MaxLength = maxLength;
            IsKey = isKey;
            CanBeNull = canBeNull;
        }
    }

    public interface ITableReader
    {
        string TableName
        {
            get;
        }

        void Open();
        bool Read();
        void Close();

        TableColumn? CurrentColumn();
    }
}
