using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LibCommon = Microsoft.Practices.McsLibrary.Data.Common;

namespace MappingTools.Generator
{
    public class DbTableReader : ITableReader
    {
        private string _tableName;

        private ArrayList _columnList = ArrayList.Synchronized(new ArrayList());
        private int _maxIndex;

        private int? _curIndex = null;

        private Database _db;

        public DbTableReader()
        {
        }

        public DbTableReader(Database db)
        {
            _db = db;
        }

        public Database Db
        {
            set { _db = value; }
        }

        #region ITableReader Members

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public void Open()
        {
            DbConnection connection = _db.CreateConnection();

            switch (LibCommon.Misc.GetDatabaseType(connection))
            {
                case LibCommon.DatabaseType.SqlServer:
                    GetDbType = GetDbTypeFromSql;
                    break;
                case LibCommon.DatabaseType.Oracle:
                    GetDbType = GetDbTypeFromOracle;
                    break;
            }

            DbCommand command = _db.GetSqlStringCommand(string.Format("select * from {0}", _tableName));
            DbDataAdapter adapter = _db.GetDataAdapter();
            DataTable table = new DataTable();

            command.Connection = connection;
            adapter.SelectCommand = command;
            adapter.FillSchema(table, SchemaType.Mapped);

            try
            {
                connection.Open();

                _columnList.Clear();

                foreach (DataColumn column in table.Columns)
                {
                    bool isKey = false;

                    foreach (DataColumn keyColum in table.PrimaryKey)
                    {
                        if (keyColum.ColumnName == column.ColumnName)
                        {
                            isKey = true;
                            break;
                        }
                    }
                    int precision, scale;
                    DbType dbType = GetDbType(connection, _tableName, column.ColumnName, out precision, out scale);
                    Type dataType = column.DataType;
                    if (dbType == DbType.Decimal && scale == 0)
                    {
                        if (precision <= 10)
                        {
                            dataType = typeof(Int32);
                            dbType = DbType.Int32;
                        }
                        else if (precision <= 20)
                        {
                            dbType = DbType.Int64;
                            dataType = typeof(Int64);
                        }
                    }
                    _columnList.Add(new TableColumn(column.ColumnName, dbType, dataType, column.MaxLength, isKey, column.AllowDBNull));
                }
            }
            finally
            {
                connection.Close();
            }

            _maxIndex = _columnList.Count - 1;
            _curIndex = -1;
        }

        public bool Read()
        {
            if (_curIndex == null) return false;
            if (_curIndex > _maxIndex) return false;

            _curIndex++;

            if (_curIndex < 0 || _curIndex > _maxIndex) return false;

            return true;
        }

        public void Close()
        {
            _curIndex = null;
        }

        public TableColumn? CurrentColumn()
        {
            if (_curIndex == null) return null;

            if (_curIndex < 0 || _curIndex > _maxIndex) return null;

            return (TableColumn)(_columnList[_curIndex.Value]);
        }

        #endregion

        private delegate DbType GetDbTypeFromDb(DbConnection connection, string tableName, string columnName,out int precision,out int scale);

        private GetDbTypeFromDb GetDbType;

        private DbType GetDbTypeFromSql(DbConnection connection, string tableName, string columnName, out int precision, out int scale)
        {
            precision = 0;
            scale = 0;
            string[] restricts = new string[4];
            restricts[2] = tableName;
            restricts[3] = columnName;
            DataTable table = connection.GetSchema("Columns", restricts);
            DataRow row = table.Rows[0];
            string dataType = (string)(row["data_type"]);
            DbType dbType = TranslateSqlType(dataType);
            if (dbType == DbType.Decimal)
            {
                precision = (int)row["precision"];
                scale = (int)row["scale"];
            }
            return dbType;
        }

        private DbType GetDbTypeFromOracle(DbConnection connection, string tableName, string columnName,out int precision,out int scale)
        {
            precision = 0;
            scale = 0;
            string[] restricts = new string[3];
            restricts[1] = tableName;
            restricts[2] = columnName;
            DataTable table = connection.GetSchema("Columns", restricts);
            DataRow row = table.Rows[0];
            string dataType = (string)(row["DATATYPE"]);           
            DbType dbType = TranslateOracleType(dataType);
            if(dbType == DbType.Decimal)
            {
                int.TryParse(row["precision"].ToString(),out precision);
                int.TryParse(row["scale"].ToString(), out scale);
            }
            return dbType;
        }

        private DbType TranslateSqlType(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "bigint": return DbType.Int64;
                case "binary": return DbType.Binary;
                case "bit": return DbType.Boolean;
                case "char": return DbType.AnsiStringFixedLength;
                case "datetime": return DbType.DateTime;
                case "decimal": return DbType.Decimal;
                case "float": return DbType.Double;
                case "image": return DbType.Binary;
                case "int": return DbType.Int32;
                case "money": return DbType.Currency;
                case "nchar": return DbType.StringFixedLength;
                case "ntext": return DbType.String;
                case "numeric": return DbType.Decimal;
                case "nvarchar": return DbType.String;
                case "real": return DbType.Single;
                case "smalldatetime": return DbType.DateTime;
                case "smallint": return DbType.Int16;
                case "smallmoney": return DbType.Currency;
                case "sql_variant": return DbType.Object;
                case "text": return DbType.AnsiString;
                case "timestamp": return DbType.Binary;
                case "tinyint": return DbType.Byte;
                case "uniqueidentifier": return DbType.Guid;
                case "varbinary": return DbType.Binary;
                case "varchar": return DbType.AnsiString;
                default: throw new ApplicationException(string.Format("Unsupported column type {0} found!", dataType));
            }
        }

        private DbType TranslateOracleType(string dataType)
        {
            switch (dataType.ToLower())
            {
                case "char": return DbType.AnsiStringFixedLength;
                case "varchar2": return DbType.AnsiString;
                case "nchar": return DbType.StringFixedLength;
                case "nvarchar2": return DbType.String;
                case "number": return DbType.Decimal;
                case "date": return DbType.DateTime;
                case "long": return DbType.AnsiString;
                case "raw": return DbType.Binary;
                case "long raw": return DbType.Binary;
                case "rowid": return DbType.AnsiString;
                case "blob": return DbType.Binary;
                case "clob": return DbType.AnsiString;
                case "nclob": return DbType.String;
                case "bfile": return DbType.Binary;
                case "urowid": return DbType.AnsiString;
                case "float": return DbType.Decimal;
                default: throw new ApplicationException(string.Format("Unsupported column type {0} found!", dataType));
            }
        }
    }
}
