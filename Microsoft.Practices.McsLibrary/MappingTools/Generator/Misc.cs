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
    public class Misc
    {
        static DataSet dsComment;
        public static void RefreshComment()
        {
            dsComment = new DataSet();
            dsComment.Tables.Add("Tables");
            dsComment.Tables.Add("Fields");
            Database db = DatabaseFactory.CreateDatabase();
            string sql1 = null,sql2 = null;
            switch (LibCommon.Misc.GetDatabaseType(db.CreateConnection()))
            {
                case LibCommon.DatabaseType.SqlServer:
                    /* sql server 2000 语句
                    sql1 = @"select a.smallid,a.value as display_name,o.name as table_name,a.id from sysproperties a inner join sysobjects o on a.id=o.id where o.xtype='U' and a.type='3'";
                     */
                    sql1 = @"SELECT  
		                            a.minor_id AS smallid,
		                            a.value AS display_name ,
                                    o.name AS table_name ,
                                    a.major_id AS id
                            FROM    sys.extended_properties a
                                    INNER JOIN sysobjects o ON a.major_id = o.id
                            WHERE   a.minor_id = '0'";
                    /* sql server 2000 语句
                    sql2 = @"select o.name as table_name, a.smallid,a.value as display_name ,c.name as column_name,a.id from sysproperties a inner join syscolumns c on a.id=c.id and a.smallid=c.colid inner join sysobjects o on a.id=o.id where o.xtype='U'";
                     */
                    sql2 = @"SELECT  o.name AS table_name ,
                                    a.minor_id AS smallid,
                                    a.value AS display_name ,
                                    c.name AS column_name ,
                                    a.major_id AS id
                            FROM    sys.extended_properties a
                                    INNER JOIN syscolumns c ON a.major_id = c.id
                                                               AND a.minor_id = c.colid
                                    INNER JOIN sysobjects o ON a.major_id = o.id";
                    break;
                case LibCommon.DatabaseType.Oracle:
                    sql1 = @"select t.table_name,t.table_type,t.comments as display_name from sys.user_tab_comments t ";
                    sql2 = @"select t.table_name, t.column_name, t.comments as display_name from sys.user_col_comments t";
                    break;
                default:
                    break;
            }
            DbCommand cmd = db.GetSqlStringCommand(sql1);
            db.LoadDataSet(cmd, dsComment, "Tables");
            cmd = db.GetSqlStringCommand(sql2);
            db.LoadDataSet(cmd, dsComment, "Fields");
            //dsComment = db.ExecuteDataSet(cmd);
        }
        public static DataSet Comment
        {
            get
            {
                if (dsComment == null)
                {
                    RefreshComment();
                    if (dsComment.Tables.Count < 2)
                    {
                        throw new ApplicationException("注释数据不完整！");
                    }
                }
                return dsComment;
            }
        }
        public static string GetComment(string tableName, string fieldName)
        {
            DataTable dt = Comment.Tables["Fields"];
            DataRow[] rows = dt.Select(string.Format("TABLE_NAME = '{0}' AND column_name='{1}'", tableName, fieldName));
            //sDataRow[] rows = dt.Select(string.Format("column_name='{0}'",fieldName));
            return rows.Length == 0 ? string.Empty : rows[0]["DISPLAY_NAME"].ToString();
        }
        public static string GetComment(string tableName)
        {
            DataTable dt = Comment.Tables["Tables"];
            DataRow[] rows = dt.Select(string.Format("TABLE_NAME = '{0}'",tableName));
            return rows.Length == 0 ? string.Empty : rows[0]["DISPLAY_NAME"].ToString();
        }
        public static string GetDisplayName(string comment)
        {
            return comment.Split(':','\r','\n','：')[0];
        }
        public static string GetPublicName(string name)
        {
            string[] parts = name.Split('_');

            if (parts.Length > 1)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    StringBuilder sb = new StringBuilder(parts[i].ToLower());
                    sb[0] = parts[i].ToUpper()[0];
                    parts[i] = sb.ToString();
                }

                return string.Concat(parts);
            }
            else
            {
                bool isAllUpperCase = true;

                foreach (char c in name)
                {
                    if (Char.IsLetter(c) && Char.IsLower(c))
                    {
                        isAllUpperCase = false;
                        break;
                    }
                }

                if (isAllUpperCase) name = name.ToLower();

                StringBuilder sb = new StringBuilder(name);
                sb[0] = name.ToUpper()[0];

                return sb.ToString();
            }
        }

        public static string GetParameterName(string name)
        {
            string publicName = GetPublicName(name);

            StringBuilder sb = new StringBuilder(publicName);
            sb[0] = publicName.ToLower()[0];

            return sb.ToString();
        }

        public static string GetPrivateName(string name)
        {
            string parameterName = GetParameterName(name);

            return "_" + parameterName;
        }

        public static string[] GetDbTables(Database database)
        {
            DbConnection connection = database.CreateConnection();

            try
            {
                connection.Open();

                switch (LibCommon.Misc.GetDatabaseType(connection))
                {
                    case LibCommon.DatabaseType.SqlServer: return GetSqlDbTables(connection);
                    case LibCommon.DatabaseType.Oracle: return GetOracleDbTables(connection);
                }
            }
            finally
            {
                connection.Close();
            }

            throw new ApplicationException(string.Format("Unsupported database typename {0} found!", connection.GetType().FullName));
        }

        private static string[] GetSqlDbTables(DbConnection connection)
        {
            string[] restricts = new string[4];
            restricts[3] = "BASE TABLE";
            DataTable table = connection.GetSchema("Tables", restricts);

            ArrayList list = new ArrayList();
            foreach (DataRow row in table.Rows)
            {
                list.Add(row["table_name"]);
            }

            return (string[])(list.ToArray(typeof(string)));
        }

        private static string[] GetOracleDbTables(DbConnection connection)
        {
            DataTable table = connection.GetSchema("Tables");
            DataView view = new DataView(table, "TYPE = 'User'", "TABLE_NAME", DataViewRowState.CurrentRows);

            ArrayList list = new ArrayList();
            foreach (DataRowView row in view)
            {
                list.Add(row["TABLE_NAME"]);
            }

            return (string[])(list.ToArray(typeof(string)));
        }
    }
}
