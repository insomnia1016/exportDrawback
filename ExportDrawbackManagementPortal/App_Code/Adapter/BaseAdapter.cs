using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ExportDrawbackManagement.Biz.Factory;
using ExportDrawbackManagement.Biz.Interface;
using ExportDrawbackManagement.Framework.Web;
using ExportDrawbackManagement.Biz.Library;


/// <summary>
/// BaseAdapter 的摘要说明
/// </summary>
[System.ComponentModel.DataObject] 
public class BaseAdapter<T>
{
    public BaseAdapter()
    {
        IBizFactory factory = BizFactoryBuilder.BuilderBizFactory();
        _manager = (T)(factory.CreateInstance(typeof(T)));

        _dataSetCache = new TypeCacheManager<DataSet>();
    }    

    T _manager;
    public T Manager
    {
        get { return _manager; }
    }

    TypeCacheManager<DataSet> _dataSetCache;

    public TypeCacheManager<DataSet> DataSetCache
    {
        get { return _dataSetCache; }
        set { _dataSetCache = value; }
    }
}
