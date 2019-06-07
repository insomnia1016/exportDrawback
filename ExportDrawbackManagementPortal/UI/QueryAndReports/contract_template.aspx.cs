using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ExportDrawbackManagement.Biz.Entity;
using System.Configuration;
using System.IO;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;

public partial class UI_QueryAndReports_contract_template : System.Web.UI.Page
{
    public DataSet ds { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
     
        if (!IsPostBack)
        {

            UI_QueryAndReports_contract contract = (UI_QueryAndReports_contract)Context.Handler;
            ds = contract.ds;
            show();
            DropDownListBind();
            delivery_mode_Bind();
            CalendarBox2.Value = CalendarBox1.Value = DateTime.Now.ToString("yyyy 年 MM 月dd 日");
            
        }
    }

    private void show()
    {
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }

    private void delivery_mode_Bind()
    {
        CommonAdapter ca = new CommonAdapter();
        DropDownList1.DataSource = ca.getDeliveryMode();
        DropDownList1.DataTextField = "name";
        DropDownList1.DataValueField = "code";
        DropDownList1.DataBind();
        DropDownList1.SelectedIndex = 0;
    }

    public void DropDownListBind()
    {
        CommonAdapter ca = new CommonAdapter();
        DataSet ds= ca.getCustomers();
        DataRow dr = ds.Tables[0].NewRow();
        dr["company_name"] = "请选择";
        dr["id"] = 0;
        ds.Tables[0].Rows.InsertAt(dr, 0);
        ddlModel.DataSource = ds;
        ddlModel.DataTextField = "company_name";
        ddlModel.DataValueField = "id";
        
        ddlModel.DataBind();
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
            TextBox tb = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_delivery_date");
            tb.Visible = false;
            GridView1.Rows[i].Cells[1].ColumnSpan += 4;
            GridView1.Rows[i].Cells[2].Visible = false;
            GridView1.Rows[i].Cells[3].Visible = false;
            GridView1.Rows[i].Cells[4].Visible = false;
        }
        else
        {
            TextBox tb = (TextBox)GridView1.Rows[0].Cells[0].FindControl("txt_delivery_date");
            tb.Visible = false;
            GridView1.Rows[0].Cells[1].ColumnSpan += 4;
            GridView1.Rows[0].Cells[2].Visible = false;
            GridView1.Rows[0].Cells[3].Visible = false;
            GridView1.Rows[0].Cells[4].Visible = false;
        }
    }
    protected void txt_invoice_price_TextChanged(object sender, EventArgs e)
    {
        //计算每一行的含税金额
        TextBox tb =  sender as TextBox;
        decimal decl_price = 0;
        if (!string.IsNullOrEmpty(tb.Text.Trim()))
            decl_price = Decimal.Parse(tb.Text.Trim());
       
        GridViewRow row = tb.Parent.Parent as GridViewRow;
        decimal g_qty = Decimal.Parse(row.Cells[2].Text.Trim());
        decimal decl_total = decl_price * g_qty;
        Label lb = row.Cells[5].FindControl("txt_invoice_total") as Label;
        lb.Text = decl_total.ToString();

        decimal decl_total_all = 0;
        //计算合计：
        for ( int i = 0; i <= GridView1.Rows.Count-1; i++)
        {
            if(i<GridView1.Rows.Count-1){
                Label inner_lb = GridView1.Rows[i].Cells[5].FindControl("txt_invoice_total") as Label;
                decimal inner_decl_total = 0;
                if (!string.IsNullOrEmpty(inner_lb.Text.Trim()))
                {
                    inner_decl_total = decimal.Parse(inner_lb.Text.Trim());
                }
                 
                decl_total_all += inner_decl_total;
            }else{
                Label inner_lb_all = GridView1.Rows[i].Cells[5].FindControl("txt_invoice_total") as Label;
                inner_lb_all.Text=decl_total_all.ToString();
            }
            
        }

      

    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.FindControl("txt_invoice_price") != null)
            {
                TextBox tb = (TextBox)e.Row.FindControl("txt_invoice_price");
                tb.TextChanged += txt_invoice_price_TextChanged;
            }
        }
    }
    protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        string companyName = ddl.SelectedItem.Text.Trim();
        if (companyName == "请选择" || string.IsNullOrEmpty(companyName))
        {
            return;
        }
        TextBox12.Text = txtModel.Text = TextBox15.Text = companyName;

        int id = Int32.Parse(ddl.SelectedValue);
        CompanyAdapter ca = new CompanyAdapter();
        T_Customers customer = ca.getCompanyInfoById(id);
        TextBox4.Text = customer.Address;
        TextBox3.Text = customer.Jingban;
        TextBox2.Text = customer.Tel;

        TextBox8.Text = customer.Fadingdaibiaoren;
        TextBox10.Text = customer.Dailiren;
    }
    /// <summary>
    /// 保存并导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = "";
        Label1.Visible = false;
        //合同表头
        T_ContractHead contractHead = new T_ContractHead();
        //需方信息

        contractHead.Xufang = TextBox12.Text.Trim();
        contractHead.XufangAddress = TextBox4.Text.Trim();
        contractHead.XufangJingbanren = TextBox3.Text.Trim();
        contractHead.XufangTel = TextBox2.Text.Trim();
        contractHead.XufangFadingdaibiaoren = TextBox8.Text.Trim();
        contractHead.XufangDailiren = TextBox10.Text.Trim();
        if (!string.IsNullOrEmpty(CalendarBox1.Text.Trim()))
        {
            contractHead.XufangQianziDate = DateTime.Parse(CalendarBox1.Text.Trim());
        }

        //供方信息
        contractHead.Gongfang = TextBox16.Text.Trim();
        contractHead.GongfangTel = TextBox6.Text.Trim();
        contractHead.GongfangJingbanren = TextBox5.Text.Trim();
        contractHead.GongfangDailiren = TextBox11.Text.Trim();
        contractHead.GongfangFadingdaibiaoren = TextBox9.Text.Trim();
        if (!string.IsNullOrEmpty(CalendarBox2.Text.Trim()))
        {
            contractHead.GongfangQianziDate = DateTime.Parse(CalendarBox2.Text.Trim());
        }

        //合同其他信息
        if (string.IsNullOrEmpty(TextBox1.Text.Trim()))
        {
            return;
        }
        contractHead.ContractId = TextBox1.Text.Trim();

        TextBox tb = (TextBox)GridView1.Rows[0].Cells[0].FindControl("txt_delivery_date");
        if (!string.IsNullOrEmpty(tb.Text.Trim()))
        {
            contractHead.DeliveryDate = DateTime.Parse(tb.Text.Trim());
        }

        Label inner_lb_all = GridView1.Rows[GridView1.Rows.Count - 1].Cells[5].FindControl("txt_invoice_total") as Label;
        if (!string.IsNullOrEmpty(inner_lb_all.Text.Trim()))
        {
            contractHead.InvoiceAll = decimal.Parse(inner_lb_all.Text.Trim());
        }
        else
        {
            contractHead.InvoiceAll = Decimal.Zero;

        }
        if (!string.IsNullOrEmpty(TextBox7.Text.Trim()))
        {
            contractHead.PaymentDays = Int32.Parse(TextBox7.Text.Trim());

        }
        contractHead.DeliveryMode = DropDownList1.SelectedValue;


        //合同表体
        List<T_ContractList> contract_lists = new List<T_ContractList>();

        for (int i = 0; i < GridView1.Rows.Count - 1; i++)
        {
            T_ContractList list = new T_ContractList();
            list.ContractId = contractHead.ContractId;
            list.ContractNo = i + 1;
            HiddenField hdf1 = GridView1.Rows[i].Cells[0].FindControl("hdf_entry_id") as HiddenField;
            list.EntryId = hdf1.Value;
            HiddenField hdf2 = GridView1.Rows[i].Cells[0].FindControl("hdf_g_no") as HiddenField;
            list.GNo = Int32.Parse(hdf2.Value.Trim());
            list.GName = GridView1.Rows[i].Cells[1].Text;
            list.GQty = decimal.Parse(GridView1.Rows[i].Cells[2].Text.Trim());
            list.GUnit = GridView1.Rows[i].Cells[3].Text;
            TextBox tb1 = GridView1.Rows[i].Cells[4].FindControl("txt_invoice_price") as TextBox;
            if (!string.IsNullOrEmpty(tb1.Text.Trim()))
            {
                list.InvoicePrice = decimal.Parse(tb1.Text.Trim());
            }
            else
            {
                list.InvoicePrice = decimal.Zero;

            }
            Label lb1 = GridView1.Rows[i].Cells[5].FindControl("txt_invoice_total") as Label;
            if (!string.IsNullOrEmpty(lb1.Text.Trim()))
            {
                list.InvoiceTotal = decimal.Parse(lb1.Text.Trim());
            }
            else
            {
                list.InvoiceTotal = decimal.Zero;
            }
            contract_lists.Add(list);
        }


        try
        {
            ContractAdapter ca = new ContractAdapter();
            EntryAdapter ea = new EntryAdapter();
            TaxListAdapter ta = new TaxListAdapter();
            ca.addContractHead(contractHead);
            ca.addContractList(contract_lists);
            //生成tax_list记录
            decimal bilu = decimal.Parse(ConfigurationManager.AppSettings["bilv"].ToString());
            ta.generateTaxList(bilu);
            //更新entry_list的invoice_flag标志位为Ture
            ea.invoice(contract_lists);
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
            Label1.Visible = true;
            return;
        }


        string filename;
        string server_file_path;

        bool result = DataTableToExcel(contractHead, contract_lists,out filename,out server_file_path);

        if (result)
        {
            //开始下载
            BigFileDownload(filename, server_file_path);
        }
        else
        {
            Response.Write("导出数据失败");
        }
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", " <script lanuage=javascript>alert('开票成功!');window.opener=null;window.top.open('','_self','');window.top.close(this);</script>");

                
        
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

    public bool DataTableToExcel(T_ContractHead head, List<T_ContractList> lists, out string filename, out string server_file_path)
    {
        #region 声明变量
        bool result = false;
        IWorkbook workbook = null;
        FileStream fs = null;
        FileStream file = null;
        IRow row = null;
        ISheet sheet = null;
        ICell cell = null;
        CommonAdapter ca = new CommonAdapter();
        #endregion
        filename = "合同" + head.ContractId + ".xlsx";
        server_file_path = Server.MapPath("../../template/" + filename);
        try
        {
           

            if (head != null && lists != null && lists.Count > 0)
            {
                //打开Excel模板文件 , 我这里有一个模板Excel文件 , 数据全部写入模板文件
                file = new FileStream(Server.MapPath("../../template/template.xlsx"), FileMode.Open, FileAccess.Read);

                //这里可能会报错 , 是因为系统Excel版本不对应的原因 , 就像Ajax在IE和其他浏览器的声明 , 但我一直找不到好的解决方案 , 如果你有更好的 , 也可以分享一下 , 我这里仅作参考代码
                workbook = new XSSFWorkbook(file);

                sheet = workbook.GetSheetAt(0);//获取Excel 中 的sheet 


                cell = sheet.GetRow(0).GetCell(0);
                cell.SetCellValue(head.Xufang);

                cell = sheet.GetRow(4).GetCell(0);
                cell.SetCellValue("需方:"+head.Xufang);

                cell = sheet.GetRow(4).GetCell(5);
                cell.SetCellValue( head.ContractId);

                cell = sheet.GetRow(6).GetCell(0);
                cell.SetCellValue("地址："+head.XufangAddress);

                cell = sheet.GetRow(7).GetCell(0);
                cell.SetCellValue("经办：" + head.XufangJingbanren);
                cell = sheet.GetRow(7).GetCell(5);
                cell.SetCellValue("TEL: " + head.XufangTel);

                cell = sheet.GetRow(9).GetCell(0);
                cell.SetCellValue("供方：" + head.Gongfang);
                cell = sheet.GetRow(10).GetCell(0);
                cell.SetCellValue("经办：" + head.GongfangJingbanren);
                cell = sheet.GetRow(10).GetCell(5);
                cell.SetCellValue("TEL: " + head.GongfangTel);

                cell = sheet.GetRow(13).GetCell(0);
                cell.SetCellValue(string.Format("{0:d}", head.DeliveryDate));

                cell = sheet.GetRow(17).GetCell(5);
                cell.SetCellValue( head.InvoiceAll+"");
                cell = sheet.GetRow(25).GetCell(0);
                cell.SetCellValue("四、交货地点：" + ca.getDeliveryModeNameByCode(head.DeliveryMode));

                cell = sheet.GetRow(27).GetCell(0);
                cell.SetCellValue("五、结算方式：以增值税发票日后__" + head.PaymentDays + "__天");

                cell = sheet.GetRow(38).GetCell(0);
                cell.SetCellValue("需方：" + head.Xufang);

                cell = sheet.GetRow(38).GetCell(3);
                cell.SetCellValue(head.Gongfang);

                cell = sheet.GetRow(40).GetCell(0);
                cell.SetCellValue("法定代表人：" + head.XufangFadingdaibiaoren);

                cell = sheet.GetRow(40).GetCell(2);
                cell.SetCellValue("        法定代表人：" + head.GongfangFadingdaibiaoren);

                cell = sheet.GetRow(41).GetCell(0);
                cell.SetCellValue("或代理人：" + head.XufangDailiren);

                cell = sheet.GetRow(41).GetCell(2);
                cell.SetCellValue("        或代理人：" + head.GongfangDailiren);

                cell = sheet.GetRow(42).GetCell(0);
                cell.SetCellValue(string.Format("{0:yyyy 年 MM 月 dd 日}", head.XufangQianziDate));

                cell = sheet.GetRow(42).GetCell(2);
                cell.SetCellValue(string.Format("        {0:yyyy 年 MM 月 dd 日}", head.GongfangQianziDate));

                int startRow = 17;//开始插入行索引
                if (lists.Count > 4)
                {
                    sheet.ShiftRows(startRow, sheet.LastRowNum, lists.Count - 4, true, false);
                    var rowSource = sheet.GetRow(13);
                    var rowStyle = rowSource.RowStyle;//获取当前行样式
                    for (int i = startRow; i < startRow + lists.Count - 4; i++)
                    {
                        var rowInsert = sheet.CreateRow(i);
                        if (rowStyle != null)
                            rowInsert.RowStyle = rowStyle;
                        rowInsert.Height = rowSource.Height;

                        for (int col = 0; col < rowSource.LastCellNum; col++)
                        {
                            var cellsource = rowSource.GetCell(col);
                            var cellInsert = rowInsert.CreateCell(col);
                            var cellStyle = cellsource.CellStyle;
                            //设置单元格样式　　　　
                            if (cellStyle != null)
                                cellInsert.CellStyle = cellsource.CellStyle;

                        }


                    }
                    sheet.AddMergedRegion(new CellRangeAddress(13, 13 + lists.Count - 1, 0, 0));        
                }

                //绑定数据
                for (int j = 0; j < lists.Count; j++)
                {
                    //单元格赋值等其他代码
                    IRow r = sheet.GetRow(j + 13);
                    r.Cells[1].SetCellValue(lists[j].GName);
                    r.Cells[2].SetCellValue(lists[j].GQty + "");
                    r.Cells[3].SetCellValue(lists[j].GUnit);
                    r.Cells[4].SetCellValue(lists[j].InvoicePrice + "");
                    r.Cells[5].SetCellValue(lists[j].InvoiceTotal + "");

                }

                

               
                using (fs = File.OpenWrite(server_file_path))
                {
                    workbook.Write(fs);//向打开的这个xls文件中写入数据  
                    result = true;
                }

            }
            return result;
        }
        catch (Exception ex)
        {
            if (fs != null)
            {
                fs.Close();
            }
            file.Close();
            return false;
        }
    }

    #endregion
}