using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using ExportDrawbackManagement.Biz.Entity;

namespace ExportDrawbackManagement.Biz.Interface
{
    /// <summary>
    /// 申报单操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDeclareManager<T>
    {
        

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="option"></param>
        /// <param name="keyValue"></param>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T GetEntityByKeys(TableOption option,object keyValue,params object[] keyValues);

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="entryID"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        T GetEntityByEntryID(TableOption option, long entryID);

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="option"></param>
        /// <param name="signNo"></param>
        /// <returns></returns>
        T GetEntityBySignNo(TableOption option, string signNo);

        /// <summary>
        /// 获得实体
        /// </summary>
        /// <param name="externalNo"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        T GetEntityByExternalNo(TableOption option, string externalNo);

        /// <summary>
        /// 暂存
        /// </summary>
        /// <param name="entity"></param>
        void SaveToTmp(T entity);

        /// <summary>
        /// 提交
        /// 之前状态={草稿|退单}
        /// </summary>
        /// <param name="entity"></param>
        void Submit(T entity);

        /// <summary>
        /// 拒绝
        /// 之前的状态={已提交|已受理}
        /// </summary>
        /// <param name="entity"></param>
        void Reject(T entity);

        /// <summary>
        /// 受理
        /// 之前状态={已提交}
        /// </summary>
        /// <param name="entity"></param>
        void Accept(T entity);

        /// <summary>
        /// 终审
        /// 之前状态={受理通过}
        /// </summary>
        /// <param name="entity"></param>
        void FinalCheck(T entity);

        /// <summary>
        /// 变更
        /// 之前状态={可执行}
        /// </summary>
        /// <param name="entity"></param>
        void Modify(T entity);

        /// <summary>
        /// 确认变更
        /// 之前状态={申请变更}
        /// </summary>
        /// <param name="entity"></param>
        void AcceptModify(T entity);

        /// <summary>
        /// 冻结
        /// 之前状态={可执行}
        /// 同时冻结所有人员
        /// </summary>
        /// <param name="entity"></param>
        void Freeze(T entity);

        /// <summary>
        /// 解冻
        /// 之前状态={冻结}
        /// 不解冻下属人员
        /// </summary>
        /// <param name="entity"></param>
        void Thaw(T entity);

        /// <summary>
        /// 注销
        /// 之前状态={可执行}
        /// 没有反向动作
        /// </summary>
        /// <param name="entity"></param>
        void Logout(T entity);

        /// <summary>
        /// 从TMP表删除，删除后同时删除表体
        /// </summary>
        /// <param name="entity"></param>
        void DeleteEntityFromTmp(T entity);
    }
}
