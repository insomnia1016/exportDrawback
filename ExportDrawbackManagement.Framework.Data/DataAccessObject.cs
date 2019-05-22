using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.Common;
using ExportDrawbackManagement.Framework.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.McsLibrary.Data;
using Microsoft.Practices.McsLibrary.Data.Filtering;
using Microsoft.Practices.McsLibrary.Data.Mapping;

namespace ExportDrawbackManagement.Framework.Data
{
    /// <summary>
    /// 数据访问对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataAccessObject<T>
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAccessObject()
        {
            _entityType = typeof(T);
            _ORMapping = ORMappingFactory.CreateInstance();
             
            _tableName = ORMapping.Mapping.GetTableName(EntityType.FullName);
            _defaultTableName = _tableName;
            foreach (MappingItem item in ORMapping.Mapping.GetKeyItemsThruClass(_entityType.FullName))
            {
                KeyItems.Add(item);
            }
            foreach (MappingItem item in ORMapping.Mapping.GetMemberItemsThruClass(_entityType.FullName))
            {
                MemberItems.Add(item);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseName"></param>
        public DataAccessObject(string databaseName)
            : this(databaseName, null)
        {
            this.DatabaseName = databaseName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        public DataAccessObject(string databaseName, string tableName)
            : this()
        {
            TableName = tableName;
        }
        #endregion

        #region 属性
        string _defaultTableName;
        /// <summary>
        /// 默认表名
        /// </summary>
        public string DefaultTableName
        {
            get { return _defaultTableName; }
            set { _defaultTableName = value; }
        }

        List<MappingItem> _KeyItems = new List<MappingItem>();
        /// <summary>
        /// 获取关键字段集合
        /// </summary>
        protected List<MappingItem> KeyItems
        { get { return _KeyItems; } }

        List<MappingItem> _MemberItems = new List<MappingItem>();
        /// <summary>
        /// 获取成员字段集合
        /// </summary>
        protected List<MappingItem> MemberItems
        { get { return _MemberItems; } }

        Type _entityType;
        /// <summary>
        /// 获取或设置实体类型
        /// </summary>
        public Type EntityType
        { get { return _entityType; } }

        string _tableName;
        /// <summary>
        /// 获取或设置表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(_tableName))
                {
                    _tableName = ORMapping.Mapping.GetTableName(EntityType.FullName);
                }
                return _tableName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _tableName = ORMapping.Mapping.GetTableName(EntityType.FullName);
                }
                else
                {
                    _tableName = value;
                }
            }
        }

        ORMapping _ORMapping;
        /// <summary>
        /// ORMaping对象
        /// </summary>
        protected ORMapping ORMapping
        { get { return _ORMapping; } }

        string _DatabaseName;
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName
        { get { return _DatabaseName; } set { _DatabaseName = value; } }
        #endregion

        #region 保护方法
        /// <summary>
        /// 根据关键字段值列表创建表达式
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        protected IExpression GetKeysExpression(object keyValue, params object[] keyValues)
        {
            IExpression exp = ExpressionFactory.Eq(KeyItems[0].PropertyName, keyValue); ;
            for (int i = 0; i < KeyItems.Count; i++)
            {
                if (exp == null)
                {
                    exp = ExpressionFactory.And(exp, ExpressionFactory.Eq(KeyItems[i].PropertyName, keyValues[i]));
                }
            }
            return exp;
        }

        #endregion

        #region GetDatabase
        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <returns></returns>
        public Database GetDatabase()
        {
            return GetDatabase(DatabaseName);
        }

        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public Database GetDatabase(string databaseName)
        {
            ExportDrawbackManagementContext context = ExportDrawbackManagementContext.Current;
            string key = string.Format("Database#{0}", databaseName);
            Database db = (Database)(context.GetContent(key));
            if (db == null)
            {
                if (string.IsNullOrEmpty(databaseName))
                {
                    db = DatabaseFactory.CreateDatabase();
                }
                else
                {
                    db = DatabaseFactory.CreateDatabase(databaseName);
                }
                context.SetContent(key, db);
            }
            return db;
        }
        #endregion

        #region NewConnection
        /// <summary>
        /// 获得新连接
        /// </summary>
        /// <returns></returns>
        public DbConnection NewConnection()
        {
            return GetDatabase().CreateConnection();
        }
        #endregion

        /// <summary>
        /// 比较两个实体对象
        /// </summary>
        /// <param name="originalEntity"></param>
        /// <param name="currentEntity"></param>
        /// <returns></returns>
        public EntityCompareResult Compare(T originalEntity, T currentEntity)
        {
            EntityCompareResult result = new EntityCompareResult();
            result.ObjectClassName = EntityType.FullName;
            result.TableName = TableName;
            result.DisplayName = ORMapping.Mapping.GetDisplayName(EntityType.FullName);
            MappingItem[] items = ORMapping.Mapping.GetMemberItemsThruClass(EntityType.FullName);
            foreach (MappingItem item in items)
            {
                if (item.IsLogModify)
                {
                    PropertyInfo property = EntityType.GetProperty(item.PropertyName);
                    object originalValue = property.GetValue(originalEntity, null);
                    object currentValue = property.GetValue(currentEntity, null);
                    if (property.PropertyType == typeof(string))
                    {
                        if (string.IsNullOrEmpty((string)originalValue))
                        {
                            originalValue = string.Empty;
                        }
                        if (string.IsNullOrEmpty((string)currentValue))
                        {
                            currentValue = string.Empty;
                        }
                    }
                    if(!object.Equals(originalValue,currentValue))
                    {
                        DifferenceItem diffItem = new DifferenceItem(item.PropertyName, item.ColumnName, item.DisplayName, originalValue, currentValue);
                        result.DifferenceItems.Add(diffItem);
                    }
                }
            }
            return result;
        }


        #region GetList
        /// <summary>
        /// 获得对象集合
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="allCount"></param>
        /// <returns></returns>
        public List<T> GetList(Criteria criteria, int startRowIndex, int pageSize, out int allCount)
        {
            List<T> list = new List<T>();
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                IList ls = ORMapping.SelectPaging(cn, EntityType, TableName, criteria,startRowIndex,pageSize,out allCount);
                foreach (T entity in ls)
                    list.Add(entity);
                return list;
            }
        }


        /// <summary>
        /// 获得对象集合
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<T> GetList(Criteria criteria)
        {
            List<T> list = new List<T>();
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                IList ls = ORMapping.FindEntities(cn, EntityType, TableName, criteria);               
                foreach (T entity in ls)
                    list.Add(entity);
                return list;
            }
        }

        /// <summary>
        /// 获得对象集合
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            List<T> list = new List<T>();
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                IList ls = ORMapping.FindEntities(cn, EntityType, TableName);
                foreach (T entity in ls)
                    list.Add(entity);
                return list;
            }
        }

        /// <summary>
        ///  获得对象集合
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public List<T> GetList(DbCommand cmd)
        {
            List<T> list = new List<T>();
            string tableName = ORMapping.Mapping.GetTableName(EntityType.FullName);
            Database db = GetDatabase();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add((T)ORMapping.GetEntity(reader, tableName));
                }
                
            }
            return list;
        }
        #endregion

        #region GetDataSet
        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            Criteria criteria = new Criteria();
            return GetDataSet(criteria);
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataSet GetDataSet(Criteria criteria)
        {
            Database db = GetDatabase();
            using (DbConnection cn = db.CreateConnection())
            {
                cn.Open();
                DbCommand cmd = ORMapping.DbBuilder.GetScopeSelectCommand(cn, EntityType.FullName, TableName, criteria);
                return db.ExecuteDataSet(cmd);                
            }
        }
        #endregion

        #region GetEntity
        /// <summary>
        /// 按照关键字段查找对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T GetEntity(object keyValue, params object[] keyValues)
        {
            T entity;
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                entity = (T)(ORMapping.FindEntity(cn, EntityType, TableName, new Criteria(exp)));
            }
            return entity;
        }

        /// <summary>
        /// 按照关键字段查找对象
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T GetEntity(DbTransaction tx, object keyValue, params object[] keyValues)
        {
            T entity;
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            entity = (T)(ORMapping.FindEntity(tx, EntityType, TableName, new Criteria(exp)));

            return entity;
        }

        /// <summary>
        /// 按照关键字段查找对象
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public T GetEntity(Criteria criteria)
        {
            T entity;
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                entity = (T)(ORMapping.FindEntity(cn, EntityType, TableName, criteria));
            }
            return entity;
        }

        /// <summary>
        /// 按照关键字段查找对象
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public T GetEntity(DbTransaction tx, Criteria criteria)
        {
            T entity;
            entity = (T)(ORMapping.FindEntity(tx, EntityType, TableName, criteria));

            return entity;
        }
        #endregion

        #region Create

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity"></param>
        public void Create(T entity)
        {
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                ORMapping.Save(cn, entity, TableName);
            }
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="entity"></param>
        public void Create(DbTransaction tx, T entity)
        {
            ORMapping.Save(tx, entity, TableName);
        }
        #endregion

        #region Update
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(T entity)
        {
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                return ORMapping.Update(cn, entity, TableName);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(DbTransaction tx, T entity)
        {
            return ORMapping.Update(tx, entity, TableName);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="target"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public int Update(Target target, Criteria criteria)
        {
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                return ORMapping.Update(cn, EntityType, target, criteria);
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="target"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public int Update(DbTransaction tx, Target target, Criteria criteria)
        {
            return ORMapping.Update(tx, EntityType, target, criteria);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="target"></param>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public int Update(Target target, object keyValue, params object[] keyValues)
        {
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                return ORMapping.Update(cn, EntityType, TableName, target, new Criteria(exp));
            }
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="target"></param>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public int Update(DbTransaction tx, Target target, object keyValue, params object[] keyValues)
        {
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            return ORMapping.Update(tx, EntityType, TableName, target, new Criteria(exp));            
        }
        #endregion

        #region CreateOrUpdate
        /// <summary>
        /// 创建或更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateOrUpdate(T entity)
        {
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                return ORMapping.SaveOrUpdate(cn, entity, TableName);
            }
        }

        /// <summary>
        /// 创建或更新实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int CreateOrUpdate(DbTransaction tx, T entity)
        {
            return ORMapping.SaveOrUpdate(tx, entity, TableName);
        }

        /// <summary>
        /// 创建或更新实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="entity"></param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public int CreateOrUpdate(DbTransaction tx, T entity, String TableName)
        {
            return ORMapping.SaveOrUpdate(tx, entity, TableName);
        }
        #endregion

        #region Delete
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public int Delete(object keyValue, params object[] keyValues)
        {
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            return Delete(new Criteria(exp));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public int Delete(DbTransaction tx, object keyValue, params object[] keyValues)
        {
            IExpression exp = GetKeysExpression(keyValue, keyValues);
            return Delete(tx, new Criteria(exp));
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public int Delete(Criteria criteria)
        {
            using (DbConnection cn = NewConnection())
            {
                cn.Open();
                return ORMapping.Delete(cn, EntityType, TableName, criteria);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public int Delete(DbTransaction tx, Criteria criteria)
        {

            return ORMapping.Delete(tx, EntityType, TableName, criteria);
        }
        #endregion

    }
}
