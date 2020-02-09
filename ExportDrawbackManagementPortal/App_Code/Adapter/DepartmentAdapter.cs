using ExportDrawbackManagement.Biz.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;

/// <summary>
/// DepartmentAdapter 的摘要说明
/// </summary>
public class DepartmentAdapter : BaseAdapter<IDepartmentManager>, IDepartmentManager
{
	public DepartmentAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet getDepartments()
    {
        return Manager.getDepartments();
    }


    public void addDepartment(T_Department item)
    {
        Manager.addDepartment(item);
    }

    public void updateDepartment(T_Department item)
    {
        Manager.updateDepartment(item);
    }

    public void DeleteDepartmentById(int id)
    {
        Manager.DeleteDepartmentById(id);
    }
}