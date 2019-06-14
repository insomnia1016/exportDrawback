<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contract_template.aspx.cs" Inherits="UI_QueryAndReports_contract_template" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>
<%@ Reference Page="~/UI/QueryAndReports/contract.aspx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>合同模板</title>
    <style type="text/css">
        body {
        }

        .box {
            margin: 0 auto;
            width: 800px;
            /*border: 1px solid #00F;*/
        }
        p {
            text-align:center;
        }
        ul li {
            list-style-type: none;
            margin-top: 20px;
        }
    </style>

</head>
<body>
    <script type="text/javascript">
        function bb() {
            var obj1 = document.getElementById("txtModel");
            var obj2 = document.getElementById("TextBox12");
            var obj3 = document.getElementById("TextBox15");

            obj3.value = obj2.value = obj1.value;
        }
        function cc() {
            var obj1 = document.getElementById("TextBox12");
            var obj2 = document.getElementById("TextBox15");
            obj2.value = obj1.value;
        }
        function dd() {
            var obj1 = document.getElementById("TextBox16");
            var obj2 = document.getElementById("TextBox14");
            obj2.value = obj1.value;
        }

       
    </script>
    <form id="form1" runat="server">
        <div class="box">
            <div style="width: 800px; font-size: 13px;">

                <ul>
                    <li style="position:relative">
                        <div style="z-index: 0; visibility: visible; clip: rect(0px 105px 80px 85px);">
                            <asp:DropDownList ID="ddlModel" runat="server" AutoPostBack="true" Style="width: 100%; font-size: 20px; text-align: center; font-weight: bold; z-index: -1; "
                                 OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                                
                            </asp:DropDownList>
                        </div>
                        <asp:TextBox ID="txtModel" onkeyup="bb()" runat="server" Style="z-index: 1; text-align: center; width: 680px; font-size: 20px; font-weight: bold;position:absolute;height:22px;top:0px;"></asp:TextBox>
                    </li>
                </ul>
               
                <ul>
                    <li style="width: 100%; text-align: center; font-size: 15px; font-weight: bold">订购合同
                    </li>
                </ul>
                <ul>
                    <li>
                        <span>需方：</span>
                        <asp:TextBox ID="TextBox12" onkeyup="cc()" Width="220px" runat="server"></asp:TextBox>

                        <span style="margin-left: 180px;">合同编号：</span>
                        <asp:TextBox ID="TextBox1" Width="80px" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate ="TextBox1" ErrorMessage="合同号不能为空"></asp:RequiredFieldValidator>
                    </li>
                    <li><span>地址：</span>
                        <asp:TextBox ID="TextBox4" Width="300px" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: 5px;">
                        <span>经办：</span>
                        <asp:TextBox ID="TextBox3" Width="80px" runat="server"></asp:TextBox>

                        <span style="margin-left: 320px;">TEL：</span>
                        <asp:TextBox ID="TextBox2" Width="100px" onKeyPress="if (event.keyCode!=45 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" runat="server"></asp:TextBox>
                    </li>

                    <li>
                        <span>供方：</span>
                        <asp:DropDownList ID="ddlGongFang" AutoPostBack="true" Width="300px" runat="server" OnSelectedIndexChanged="ddlGongFang_SelectedIndexChanged"></asp:DropDownList>
                    </li>


                    <li style="margin-top: 5px;">
                        <span>经办：</span>
                        <asp:TextBox ID="TextBox5" Width="80px" runat="server"></asp:TextBox>

                        <span style="margin-left: 320px;">TEL：</span>
                        <asp:TextBox ID="TextBox6" Width="100px" onKeyPress="if (event.keyCode!=45 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" runat="server"></asp:TextBox>
                    </li>
                </ul>

                <ul>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnDataBound="GridView1_DataBound" OnRowCreated="GridView1_RowCreated">

                        <Columns>
                            <asp:TemplateField HeaderText="供货日期">
                                <ItemTemplate>
                                    <cc1:CalendarBox ID="txt_delivery_date" Style="text-align: center;" ResourcePath="../../Calendar" FormatString="yyyy/MM/dd" runat="server"></cc1:CalendarBox>
                                    <asp:HiddenField ID="hdf_g_no" Value ='<%#Eval("g_no") %>' runat="server" />
                                    <asp:HiddenField ID="hdf_entry_id" Value ='<%#Eval("entry_id") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="g_name"  HeaderText="产品名称" />
                            <asp:BoundField DataField="g_qty"  DataFormatString="{0:N}" HeaderText="数量" HtmlEncode="False" />
                            <asp:BoundField DataField="g_unit" HeaderText="单位" />
                            <asp:TemplateField HeaderText="单价" >
                                <ItemTemplate >
                                    <asp:TextBox ID="txt_invoice_price" AutoPostBack="true" Width="80px" OnTextChanged="txt_invoice_price_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center" runat="server"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="含税金额（元）">
                                <ItemTemplate>
                                    <asp:Label ID="txt_invoice_total" runat="server"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </ul>

                <ul>
                    <li>
                        <span>一、  人权要求：供方不得招收监狱劳工以及18周岁以下人员，员工每月工资不得低于当地最低生活保障等人权要求，违者需方将取消其供应商资格。
                        </span></li>
                    <li>
                        <span>二、  质量要求：供方必须按需方的最终确认样品或需方要求的质量及技术标准</span>
                    </li>
                    <li>
                        <span style="color: red">三、运输方式：公路运输</span>
                    </li>

                    <li>
                        <span>四、  交货地点：</span>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            
                        </asp:DropDownList>
                    </li>
                    <li>
                        <span style="color: red">五、结算方式：以增值税发票日后</span>
                        <asp:TextBox ID="TextBox7" Width="50px" runat="server" Style="text-align: center" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false" ForeColor="Red" Text="30"></asp:TextBox><span style="color: red"> 天</span>
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
                        <asp:TextBox ID="TextBox15" Width="220px" runat="server"></asp:TextBox>
                        <span style="margin-left: 80px;">供方：</span>
                        <asp:TextBox ID="TextBox14" Width="240px" runat="server"></asp:TextBox>
                    </li>
                    <li>
                        <span>法定代表人： </span>
                        <asp:TextBox ID="TextBox8" Width="80px" runat="server"></asp:TextBox>

                        <span style="margin-left: 180px;">法定代表人： </span>
                        <asp:TextBox ID="TextBox9" Width="80px" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-top: 1px;">
                        <span>或代理人： </span>
                        <asp:TextBox ID="TextBox10" Width="80px" runat="server"></asp:TextBox>

                        <span style="margin-left: 193px;">或代理人： </span>
                        <asp:TextBox ID="TextBox11" Width="80px" runat="server"></asp:TextBox>
                    </li>
                    <li style="margin-bottom: 60px; margin-top: 1px;">
                        <span>
                            <cc1:CalendarBox ID="CalendarBox1" Width="120px" FormatString="yyyy 年 MM 月dd 日" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox></span>

                        <span style="margin-left: 220px;">
                            <cc1:CalendarBox ID="CalendarBox2" Width="120px" FormatString="yyyy 年 MM 月dd 日" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox></span>
                    </li>
                </ul>

                <ul>
                     <li >
                         <p>
                         <asp:Button ID="Button1" style="font-weight: bold;font-size: 23px;" runat="server" Text="保存并导出excel" OnClick="Button1_Click" />
                             <asp:Label ID="Label1" ForeColor="Red" Visible="false" runat="server"></asp:Label>

                         </p>
                    </li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
