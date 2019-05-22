using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MappingTools.Generator
{
    public class CSharpClassWriter : FileMedia, IClassWriter
    {
        private string _nameSpace;
        private string _className;
        string _tableName;
        private Hashtable _propertyList = Hashtable.Synchronized(new Hashtable());
        Hashtable _fieldList = Hashtable.Synchronized(new Hashtable());

        public CSharpClassWriter()
        {
        }

        public CSharpClassWriter(string basePath)
        {
            BasePath = basePath;
        }

        void WriteComment(StreamWriter writer, string comment)
        {
            writer.WriteLine(
@"        /// <summary>
        /// {0}
        /// </summary>", comment.Replace("\n", "\n ///"));
        }


        #region IClassWriter 成员

        public string TableName
        {
            set { _tableName = value; }
            get { return _tableName; }
        }

        public void AppendProperty(string propertyName, TableColumn col, Type propertyType)
        {
            _propertyList.Add(propertyName,propertyType);
            _fieldList.Add(propertyName, col);
        }

        public string NameSpace
        {
            set { _nameSpace = value; }
            get { return _nameSpace; }
        }

        public string ClassName
        {
            set { _className = value; }
            get { return _className; }
        }

        public void AppendProperty(string propertyName, Type propertyType)
        {
            _propertyList[propertyName] = propertyType;
        }

        public void RemoveProperty(string propertyName)
        {
            _propertyList.Remove(propertyName);
            _fieldList.Remove(propertyName);
        }

        public void ClearProperty()
        {
            _propertyList.Clear();
            _fieldList.Clear();
        }

        public void WriteOut()
        {
            string fileName = string.Format("{0}\\{1}.cs", _basePath, _className);

            using (StreamWriter writer = new StreamWriter(fileName,false,System.Text.UTF8Encoding.Default))
            {
                WriteComment(writer);
                WriteHeader(writer);
                WriteBody(writer);
            }
        }

        #endregion

        private void WriteComment(StreamWriter writer)
        {
            writer.WriteLine("/******************************************************");
            writer.WriteLine("* author :  chenjianwu");
            writer.WriteLine("* email  :  chenjianwu@sh.intra.customs.gov.cn");
            writer.WriteLine("* function: ");
            writer.WriteLine("* Auto generated by MappingTools at {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteLine("******************************************************/");

            writer.WriteLine();
        }

        private void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Collections.Generic;");
            writer.WriteLine("using System.Text;");

            writer.WriteLine();
        }

        private void WriteBody(StreamWriter writer)
        {
            writer.WriteLine("namespace {0}", string.IsNullOrEmpty(_nameSpace) ? "NoNameSpace" : _nameSpace);
            writer.WriteLine("{");
            WriteComment(writer,Misc.GetComment(this._tableName));
            writer.WriteLine("\tpublic partial class {0}", _className);
            writer.WriteLine("\t{");

            foreach (DictionaryEntry entry in _propertyList)
            {
                WriteProperty(writer, (string)(entry.Key), (Type)(entry.Value));
            }
            writer.WriteLine("#region 名称常量定义");
            WriteComment(writer, "名称常量定义");
            writer.WriteLine(@"public static class NameDefine
        {");
            WriteComment(writer, "默认表名");
            writer.WriteLine(@"public const string DefaultTableName = ""{0}"";",this._tableName);
            foreach (DictionaryEntry entry in _fieldList)
            {
                string key = (string)entry.Key;
                TableColumn col = (TableColumn)entry.Value;
                string comment = Misc.GetComment(_tableName, col.Name);
                string displayName = Misc.GetDisplayName(comment);
                WriteComment(writer,"属性名称定义 " + displayName);
                writer.WriteLine(@"public const string PropertyName{0} = ""{0}"";",key);
                WriteComment(writer, "字段名称定义 " + displayName);
                writer.WriteLine(@"public const string FieldName{0} = ""{1}"";",key,col.Name);
           
            }
            writer.WriteLine(@"
        }");
            writer.WriteLine("#endregion");
            writer.WriteLine("\t}");

            writer.WriteLine("}");
        }

        private void WriteProperty(StreamWriter writer, string propertyName, Type propertyType)
        {
            string privateName = Misc.GetPrivateName(propertyName);
            string typeName = propertyType.Name;
            switch(propertyName)
            {
                case "ExeMark":
                    typeName = "ExeMark";
                    break;
                case "ProcMark":
                    typeName = "ProcMark";
                    break;
                default:
                    break;
            }
            TableColumn col = (TableColumn)_fieldList[propertyName];
            
            if (propertyType.IsValueType && col.CanBeNull) typeName += "?";

            writer.WriteLine("\t\tprivate {0} {1};", typeName, privateName);
            string comment = Misc.GetComment(_tableName, col.Name);
            WriteComment(writer, comment);
            writer.WriteLine("\t\tpublic {0} {1}", typeName, propertyName);
            writer.Write("\t\t{ get { return " + privateName + "; }" );
            writer.Write(" set { " + privateName + " = value; }" );
            writer.WriteLine(" }");
            writer.WriteLine();
        }
    }
}
