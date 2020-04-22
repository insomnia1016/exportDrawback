using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;

public partial class UI_Security_add : System.Web.UI.Page
{
    public List<Funcs> roles { get; set; }
    UsersAdapter ua = new UsersAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRolesControl();
            show();
        }

    }

    private List<Funcs> BindRolesControl()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Server.MapPath("~/App_Data/Menus.xml"));
        roles = new List<Funcs>();
        foreach (XmlNode mainMenuNode in doc.SelectNodes(string.Format("/Root/MenuItems/MenuItem")))
        {
            addToRoles(mainMenuNode, roles, 0);
        }
        roles = roles.OrderBy(item => item.Roles).ToList();
        DataTable dt = new DataTable();
        dt.Columns.Add("Roles");
        dt.Columns.Add("DisplayName");

        //TODO:绑定到下拉多选框控件上
        foreach (Funcs item in roles)
        {
            DataRow dr = dt.Rows.Add();
            dr["Roles"] = item.Roles;
            dr["DisplayName"] = item.DisplayName;
        }

        DropDownCheckBoxListDataSouceBind(dt);
        return roles;
    }

    private void DropDownCheckBoxListDataSouceBind(DataTable dt)
    {
        DropDownCheckBoxList1.DataTextField = "DisplayName";
        DropDownCheckBoxList1.DataValueField = "Roles";
        DropDownCheckBoxList1.DataSource = dt;
        DropDownCheckBoxList1.DataBind();
    }

    private void addToRoles(XmlNode node, List<Funcs> list, int level)
    {
        foreach (XmlNode item in node.SelectNodes(string.Format("./MenuItems/MenuItem")))
        {
            addToRoles(item, list, ++level);
        }
        Funcs t = new Funcs();
        string funcstr = GetAttrValue(node, "Roles");
        if (string.IsNullOrEmpty(funcstr))
        {
            return;
        }
        t.Roles = GetAttrValue(node, "Roles");
        t.DisplayName = GetAttrValue(node, "DisplayName");
        list.Add(t);
    }

    string GetAttrValue(XmlNode node, string attrName)
    {
        XmlAttribute attr = node.Attributes[attrName];
        if (attr == null)
            return "";
        return attr.Value;
    }
   
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectIndex = GridView1.SelectedIndex;
        txtName.Text = GridView1.Rows[selectIndex].Cells[1].Text;
        txtUsername.Text = GridView1.Rows[selectIndex].Cells[2].Text;
        txtDerparment.Text = GridView1.Rows[selectIndex].Cells[4].Text;
        txtPassword.Text = GridView1.Rows[selectIndex].Cells[5].Text;
        string quanxian = GridView1.Rows[selectIndex].Cells[3].Text.Trim();
        string Values = translateRoles(quanxian);
        DropDownCheckBoxList1.SelectedValue = Values;
        DropDownCheckBoxList1.SelectedText = getDisplayNamesByValues(Values);
    }

    private string translateRoles(string role)
    {
        string newRoles = string.Empty;
        if (role == "ALL")
        {
            roles = BindRolesControl();
            foreach (Funcs item in roles)
            {
                if (string.IsNullOrEmpty(newRoles))
                {
                    newRoles = item.Roles;
                }
                else
                {
                    newRoles += "," + item.Roles;
                }
            }
        }
        else
        {
            newRoles = role;
        }
        return newRoles;
    }

    private string fanTranslateRoles(string role)
    {
        string newRoles = string.Empty;
        roles = BindRolesControl();
        
        foreach (Funcs item in roles)
        {
            if (string.IsNullOrEmpty(newRoles))
            {
                newRoles = item.Roles;
            }
            else
            {
                newRoles += "," + item.Roles;
            }
        }
        var result2 = distinct(newRoles);
        var result1 = distinct(role);
        if (result1 == result2)
        {
            return "ALL";
        }
        else
        {
            return role;
        }
    }
    private static string distinct(string role)
    {
        List<string> dropdpwnlistArray = role.Split(',').ToList<String>();
        var query = dropdpwnlistArray.Distinct().OrderBy(c => c).ToList();
        var result = String.Join(",", query.ToArray());
        return result;
    }
    private string getDisplayNamesByValues(string values)
    {
        string[] roles_array = values.Split(',');
        string newValues = string.Empty;
        roles = BindRolesControl();
        foreach (string value in roles_array)
        {
            foreach (Funcs role in roles)
            {
                if (role.Roles == value)
                {
                    if (string.IsNullOrEmpty(newValues))
                    {
                        newValues = role.DisplayName;
                    }
                    else
                    {
                        newValues += ","+ role.DisplayName;
                    }
                }
            }
        }
        return newValues;
    }
    /// <summary>
    /// 根据角色查找功能名
    /// </summary>
    /// <param name="role">角色名</param>
    /// <returns></returns>
    private Funcs getDisplayNameByRoles(string role)
    {
        BindRolesControl();

        foreach (Funcs item in roles)
        {
            if (item.Roles == "role")
            {
                return item;
            }
        }
        return null;
    }

    private void init()
    {
        DropDownCheckBoxList1.SelectedValue = "";
        DropDownCheckBoxList1.SelectedText = "";
        txtName.Text = "";
        txtUsername.Text = "";
        txtPassword.Text = "";
        txtDerparment.Text = "";

    }
    private void show()
    {
       
        DataSet ds = ua.getUsers();
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void updateUser_Click(object sender, EventArgs e)
    {
        T_Users user = new T_Users();
        user.Roles = fanTranslateRoles(DropDownCheckBoxList1.Text);
        user.Name = txtName.Text.Trim();
        if (string.IsNullOrEmpty(user.Name))
        {
            Label1.Text = "姓名不能为空";
            return;
        }
        user.Username = txtUsername.Text.Trim();
        user.Password = txtPassword.Text.Trim();
        if (string.IsNullOrEmpty(user.Password))
        {
            Label1.Text = "密码不能为空";
            return;
        }
        user.Derpartment = txtDerparment.Text.Trim();

        
        try
        {
            ua.add(user);
            init();
            Label1.Text = "新增成功";
            show();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void benDelete_Click(object sender, EventArgs e)
    {
        int person_id = Int32.Parse(((HiddenField)((Button)sender).Parent.FindControl("hdfId")).Value);
        try
        {
            ua.deleteUser(person_id);

            Label1.Text = "删除成功";
            show();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }

    }
}
