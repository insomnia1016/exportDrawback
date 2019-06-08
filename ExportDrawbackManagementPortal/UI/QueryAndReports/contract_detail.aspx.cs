using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExportDrawbackManagement.Biz.Entity;
using System.Data;
using System.IO;

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
        if (string.IsNullOrEmpty(id))
        {
            return;
        }
        ContractAdapter ca = new ContractAdapter();
        T_ContractHead head = ca.getContractDetail(id);
        lbl_title.Text = lbl_xufang.Text = lbl_xufang2.Text = head.Xufang;
        lbl_contract_id.Text = id;
        lbl_xufang_address.Text = head.XufangAddress;
        lbl_xufang_jingbanren.Text = head.XufangJingbanren;
        lbl_xufang_tel.Text = head.XufangTel;
        lbl_gongfang.Text = lbl_gongfang2.Text = head.Gongfang;
        lbl_gongfang_jingban.Text = head.GongfangJingbanren;
        lbl_gongfang_tel.Text = head.GongfangTel;
        lbl_delivery_mode.Text = head.DeliveryMode;
        lbl_payment_days.Text = head.PaymentDays.ToString();
        lbl_xufang_fadingdaibiaoren.Text = head.XufangFadingdaibiaoren;
        lbl_xufang_dailiren.Text = head.XufangDailiren;
        lbl_xufang_qianding_date.Text = string.Format("{0:yyyy 年 MM 月dd 日}", head.XufangQianziDate);
        lbl_gongfang_fadingdaibiaoren.Text = head.GongfangFadingdaibiaoren;
        lbl_gongfang_dailiren.Text = head.GongfangDailiren;
        lbl_gongfang_qianding_date.Text = string.Format("{0:yyyy 年 MM 月dd 日}", head.GongfangQianziDate);

        gridviewBind(head.lists);
    }
    private void gridviewBind(DataSet ds)
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }


    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        int i;
        for (i = 1; i < GridView1.Rows.Count - 1; i++)
        {
            if (GridView1.Rows[0].Cells[0].RowSpan == 0) GridView1.Rows[0].Cells[0].RowSpan++;
            GridView1.Rows[0].Cells[0].RowSpan++;
            GridView1.Rows[i].Cells[0].Visible = false;
        }
        if (GridView1.Rows.Count > 1)
        {
            GridView1.Rows[i].Cells[1].ColumnSpan += 4;
            GridView1.Rows[i].Cells[2].Visible = false;
            GridView1.Rows[i].Cells[3].Visible = false;
            GridView1.Rows[i].Cells[4].Visible = false;
        }
        else
        {
            GridView1.Rows[0].Cells[1].ColumnSpan += 4;
            GridView1.Rows[0].Cells[2].Visible = false;
            GridView1.Rows[0].Cells[3].Visible = false;
            GridView1.Rows[0].Cells[4].Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string contract_id = lbl_contract_id.Text;
        if (string.IsNullOrEmpty(contract_id))
        {
            return;
        }
        string filename = "合同" + contract_id + ".xlsx";
       string  server_file_path = Server.MapPath("../../template/" + filename);
       BigFileDownload(filename, server_file_path);
    }

    #region 文件下载

    /// 文件下载 
    /// </summary> 
    /// <param name="FileName">下载后的文件名</param> 
    /// <param name="FilePath">需要下载文件的server.path路径</param> 
    public void BigFileDownload(string FileName, string FilePath)
    {

        try
        {
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                //以字符流的形式下载文件

                byte[] bytes = new byte[(int)fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();

                //开始调用html页面下载窗
                Response.ContentType = "application/octet-stream;charset=gb2321";

                //通知浏览器下载文件而不是打开;对中文名称进行编码
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8));
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            Response.Flush();
            Response.End();

        }
    }
    #endregion
}