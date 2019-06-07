<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contract_detail.aspx.cs" Inherits="UI_QueryAndReports_contract_detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合同详细</title>
    <style type="text/css">
        body {
        }

        .box {
            margin: 0 auto;
            width: 800px;
            /*border: 1px solid #00F;*/
        }

        p {
            text-align: center;
        }

        ul li {
            list-style-type: none;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box">
            <div style="width: 800px; font-size: 13px;">

                <ul>
                    <li style="text-align: center;">
                        <asp:Label ID="lbl_title" runat="server" Style="text-align: center; width: 680px; font-size: 20px; font-weight: bold; height: 22px;" Text="Label"></asp:Label>
                    </li>
                </ul>

                <ul>
                    <li style="width: 100%; text-align: center; font-size: 15px; font-weight: bold">订购合同
                    </li>
                </ul>
                <ul>
                    <li>
                        <span>需方：</span>
                        <asp:Label ID="lbl_xufang" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 180px;">合同编号：</span>
                        <asp:Label ID="lbl_contract_id" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li><span>地址：</span>
                        <asp:Label ID="lbl_xufang_address" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li style="margin-top: 5px;">
                        <span>经办：</span>
                        <asp:Label ID="lbl_xufang_jingbanren" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 320px;">TEL：</span>
                        <asp:Label ID="lbl_xufang_tel" runat="server" Text="Label"></asp:Label>
                    </li>

                    <li>
                        <span>供方：</span>
                        <asp:Label ID="lbl_gongfang" runat="server" Text="Label"></asp:Label>
                    </li>


                    <li style="margin-top: 5px;">
                        <span>经办：</span>
                        <asp:Label ID="lbl_gongfang_jingban" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 320px;">TEL：</span>
                        <asp:Label ID="lbl_gongfang_tel" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>

                <ul>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnDataBound="GridView1_DataBound">

                        <Columns>
                            <asp:BoundField DataField="delivery_date" HeaderText="供货日期" />


                            <asp:BoundField DataField="g_name" HeaderText="产品名称" />
                            <asp:BoundField DataField="g_qty" DataFormatString="{0:N}" HeaderText="数量" HtmlEncode="False" />
                            <asp:BoundField DataField="g_unit" HeaderText="单位" />
                            <asp:BoundField DataField="invoice_price" HeaderText="单价" />
                            <asp:BoundField DataField="invoice_total" HeaderText="含税金额（元）" />


                        </Columns>
                    </asp:GridView>
                </ul>

                <ul>
                    <li>
                        <span>一、人权要求：供方不得招收监狱劳工以及18周岁以下人员，员工每月工资不得低于当地最低生活保障等人权要求，违者需方将取消其供应商资格。
                        </span></li>
                    <li>
                        <span>二、质量要求：供方必须按需方的最终确认样品或需方要求的质量及技术标准</span>
                    </li>
                    <li>
                        <span style="color: red">三、运输方式：公路运输</span>
                    </li>

                    <li>
                        <span>四、交货地点：</span>
                        <asp:Label ID="lbl_delivery_mode" runat="server" ></asp:Label>

                    </li>
                    <li>
                        <span style="color: red">五、结算方式：以增值税发票日后</span>
                        <asp:Label ID="lbl_payment_days" runat="server" ForeColor="Red"></asp:Label><span style="color: red"> 天</span>
                    </li>
                    <li>
                        <span>六、违约责任：任何由于供方质量问题及迟延交货原因而造成需方经济损失，由供方承担责</span>
                    </li>
                    <li>
                        <span>七、备注事项：所有供需双方追加意见或更改意见一经双方确认后，即构成购销合同的组成部分。未经需方同意，供方不得将需方的样品、款式、物料展示、销售给不利于需方的其他企业，违者将承担相应的法律责任。</span>
                    </li>
                    <li>
                        <span>八、供方确认本合同后请签字回传至需方，本合同经供、需双方签字后即生效。</span>
                    </li>
                    <li>
                        <span>需方：</span>
                        <asp:Label ID="lbl_xufang2" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 220px;">供方：</span>
                        <asp:Label ID="lbl_gongfang2" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li>
                        <span>法定代表人： </span>
                        <asp:Label ID="lbl_xufang_fadingdaibiaoren" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 180px;">法定代表人： </span>
                        <asp:Label ID="lbl_gongfang_fadingdaibiaoren" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li style="margin-top: 1px;">
                        <span>或代理人： </span>
                        <asp:Label ID="lbl_xufang_dailiren" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 193px;">或代理人： </span>
                        <asp:Label ID="lbl_gongfang_dailiren" runat="server" Text="Label"></asp:Label>
                    </li>
                    <li style="margin-bottom: 60px; margin-top: 1px;">
                        <span>
                            <asp:Label ID="lbl_xufang_qianding_date" runat="server" Width="120px" Text="Label"></asp:Label>
                        </span>
                        <span style="margin-left: 180px;">
                            <asp:Label ID="lbl_gongfang_qianding_date" runat="server" Width="120px" Text="Label"></asp:Label>
                        </span>
                    </li>
                </ul>
                <ul>
                    <li>
                        <p>
                            <asp:Button ID="Button1" Style="font-weight: bold; font-size: 23px;" runat="server" Text="下载excel" />

                        </p>
                    </li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
