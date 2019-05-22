using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ExportDrawbackManagement.Biz.Interface
{
    /// <summary>
    /// 字典管理器基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDictionaryManager<T>
    {
        /// <summary>
        /// 获得字典列表
        /// </summary>
        /// <returns></returns>
        List<T> GetList();
        /// <summary>
        /// 获得字典数据集
        /// </summary>
        /// <returns></returns>
        DataSet GetDataSet();
        /// <summary>
        /// 创建字典
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);
        /// <summary>
        /// 更新字典
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
        /// <summary>
        /// 删除字典
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
    }
}
