using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Suppliery 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
// [System.Web.Script.Services.ScriptService]
public class Suppliery : System.Web.Services.WebService {

    public Suppliery () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] SelectSearchInfo(string prefixText, int count)
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet lst = ca.getSuppliersBySearchKeyName(prefixText.Trim());
        int num = lst.Tables[0].Rows.Count;
        if (num > 0)
        {
            string[] result = new string[num];

            for (int i = 0; i < num; i++)
            {
                result[i] = lst.Tables[0].Rows[i][1].ToString();
            }
            return result;
        }
        else
        {
            string[] result = { "没有该联系人！" };
            return result;
        }
    }  
    
}
