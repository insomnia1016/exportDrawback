using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExportDrawbackManagement.Biz.Interface;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;
/// <summary>
/// UsersAdapter 的摘要说明
/// </summary>
public class UsersAdapter : BaseAdapter<IUsersManager>, IUsersManager
{
	public UsersAdapter()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
	public bool login(string name, string pswd,out DataSet ds)
	{
		return Manager.login(name,pswd,out ds);
	}



	public string register(T_Users entity)
	{
	   return Manager.register(entity);
	}


	public bool changePassword(int person_id, string oldPswd, string newPswd, out string msg)
	{
		return Manager.changePassword(person_id, oldPswd, newPswd, out msg);
	}


	public DataSet getUsers()
	{
		return Manager.getUsers();
	}


	public void UpdateUserInfo(T_Users user)
	{
		Manager.UpdateUserInfo(user);
	}


	public void deleteUser(int person_id)
	{
		Manager.deleteUser(person_id);
	}


    public void add(T_Users entity)
    {
        Manager.add(entity);
    }
}