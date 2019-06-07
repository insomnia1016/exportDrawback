using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_QueryAndReports_contract_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            setData(id);
        }
    }
    private void setData(string id)
    {

    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {

    }
}