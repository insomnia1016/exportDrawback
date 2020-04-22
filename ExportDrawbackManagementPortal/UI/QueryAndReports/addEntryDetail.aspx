<%@ Page Title="填制报关单明细" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="addEntryDetail.aspx.cs" Inherits="UI_QueryAndReports_addEntryDetail" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>录入报关单表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">客户</span>
            <span class="control">
                <asp:TextBox ID="owner_name" Width="180px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="owner_name" ErrorMessage="客户不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">报关日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="d_date" Width="180px" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ControlToValidate="d_date" ErrorMessage="报关日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">申报单位</span>
            <span class="control">
                <asp:TextBox ID="agent_name" Width="180px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="agent_name" ErrorMessage="申报单位不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li style="clear:both;"><span class="title" >报关单号</span>
            <span class="control">
                <asp:TextBox ID="entry_id" Width="180px" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="entry_id" ErrorMessage="报关单号不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
         <li><span class="title">销售发票号</span>
            <span class="control">
                <asp:TextBox ID="txt_sale_bill_no" Width="180px" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
            </span>
        </li>
    </ul>
    
    <h2 style="float:left;text-align:left;width:100%;">录入报关单表体数据</h2>
        <div id="div1" style="text-align:center">
            <asp:HiddenField ID="hfRptColumns" runat="server" Value="g_name,code_ts,g_no,g_qty,g_unit,trade_curr,decl_price,decl_total,drawback_rate" />
            <table border="1"  style="width: 100%;">
                <thead>
                    <tr>
                        <th>报关品名
                        </th>
                        <th>海关编码
                        </th>
                        <th>品名项号
                        </th>
                        <th>数量
                        </th>
                        <th>计量单位
                        </th>
                        <th>成交币制
                        </th>
                        <th>申报单价
                        </th>
                        <th>报关金额
                        </th>
                        <th>出口退税率
                        </th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTest" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txt_g_name" runat="server" Text='<%#Eval("g_name") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_code_ts" runat="server" Text='<%#Eval("code_ts") %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_g_no" runat="server" Text='<%# Container.ItemIndex+1 %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_g_qty" AutoPostBack="true" OnTextChanged="txt_g_qty_Click" runat="server"  Text='<%#Eval("g_qty") %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_g_unit" runat="server" Text='<%#Eval("g_unit") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_trade_curr" runat="server" Text='<%#Eval("trade_curr") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_decl_price" AutoPostBack="true" OnTextChanged="txt_g_qty_Click" runat="server" Text='<%#Eval("decl_price") %>' onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" ></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_decl_total" runat="server" Text='<%#Eval("decl_total") %>' onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_drawback_rate" runat="server" Text='<%#Eval("drawback_rate") %>' onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>

            <div>
                <asp:Button ID="btnAddNewRow" runat="server" OnClick="btnAddNewRow_Click" Text="添加一行" />
            </div>
        </div>
    
    <ul class="queryarea">
        <li style="width: 100%">
            <asp:Button ID="add" runat="server" Text="录  入" OnClick="add_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>


   
</asp:Content>

