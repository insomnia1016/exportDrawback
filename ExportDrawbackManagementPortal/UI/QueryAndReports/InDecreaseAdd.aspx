<%@ Page Title="添加增减单" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="InDecreaseAdd.aspx.cs" Inherits="UI_QueryAndReports_InDecreaseAdd" %>
<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <h2>录入增减单表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">客户/供应商：</span>
            <span class="control">
                <asp:TextBox ID="customer" Width="380px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="customer" ErrorMessage="客户不能为空"></asp:RequiredFieldValidator>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" Enabled="true" TargetControlID="customer" ServicePath="~/webservices/AutoCompleteService.asmx" ServiceMethod="SelectSearchInfo" CompletionSetCount="10" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>
            </span>
        </li>
         <li><span class="title">单据编码：</span>
            <span class="control">
                <asp:TextBox ID="bill_no" Width="180px" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="bill_no" ErrorMessage="单据编码不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
         <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency"  Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">日期：</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="agent_date" style="text-align:center;"  runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ControlToValidate="agent_date" ErrorMessage="日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">申请人：</span>
            <span class="control">
                <asp:TextBox ID="agent_name" Width="180px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="agent_name" ErrorMessage="申请人不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
       

    </ul>
    
    <h2 style="float:left;text-align:left;width:100%;">录入增减单表体数据</h2>
        <div id="div1" style="text-align:center">
            <asp:HiddenField ID="hfRptColumns" runat="server" Value="bill_no,g_no,name,amount,type,apply_date,note" />
            <table border="1"  style="width: 100%;   ">
                <thead>
                    <tr>
                        <th>项号(*)
                        </th>
                        <th>费用名称(*)
                        </th>
                        <th>金额(*)
                        </th>
                        <th>费用类型(*)
                        </th>
                        <th>申请日期(*)
                        </th>
                        <th>备注
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTest" OnItemDataBound="rptTest_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr><td>
                                    <asp:TextBox ID="txt_g_no"  Width="100%" runat="server" style="text-align:center;" Text='<%# Container.ItemIndex+1 %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                                
                                <td>
                                    <asp:TextBox ID="txt_name" Width="100%"  style="text-align:center;" runat="server" Text='<%#Eval("name") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txt_amount"  Width="100%" style="text-align:center;"  runat="server" Text='<%#Eval("amount") %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                                <td>
                                    <asp:DropDownList ID="ddl_type" style="width:100%;text-align:center;"  runat="server">
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <ExportDrawbackManagement:CalendarBox ID="cb_apply_date" runat="server"  style="text-align:center;"  Width="100%" Text='<%#Eval("apply_date") %>' FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                                <td>
                                    <asp:TextBox ID="txt_note" Width="100%" style="text-align:center;" runat="server"   Text='<%#Eval("note") %>'></asp:TextBox></td>
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

