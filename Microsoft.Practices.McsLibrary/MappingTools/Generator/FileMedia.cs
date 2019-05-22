using System;
using System.Collections.Generic;
using System.Text;

namespace MappingTools.Generator
{
    public class FileMedia
    {
        protected string _basePath;

        public string BasePath
        {
            set
            {
                _basePath = value.TrimEnd('\\');
            }
        }

        public const string ENTITY_CLASS_PRE = "T_";
        public const string QUERY_CLASS_PRE = "Q_";
    }
}
