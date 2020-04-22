<%@ Page Title="增减费用单修改" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="payment_inDecrease_edit.aspx.cs" Inherits="UI_payment_payment_inDecrease_edit" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h2 style="float: left; text-align: left; width: 100%;">编辑增减单表头数据</h2>

    <ul class="queryarea">
        <li><span class="title">供应商：</span>
            <span class="control">
                <asp:TextBox ID="customer" Enabled="false" Width="380px" runat="server"></asp:TextBox>
                <asp:HiddenField ID="hdf_amount_all" runat="server" />
                <asp:HiddenField ID="hdf_amount" runat="server" />

            </span>
        </li>
        <li><span class="title">单据编码：</span>
            <span class="control">
                <asp:TextBox ID="bill_no" Width="180px" Enabled="false" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency" Enabled="false"  Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">日期：</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="agent_date" Style="text-align: center;" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>

            </span>
        </li>
        <li><span class="title">申请人：</span>
            <span class="control">
                <asp:TextBox ID="agent_name" Width="180px" runat="server"></asp:TextBox>

            </span>
        </li>


    </ul>

    <h2 style="float: left; text-align: left; width: 100%;">编辑增减单表体数据</h2>
    <div id="div1" style="text-align: center">
        <table border="1" style="width: 100%;">
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

                <tr>
                    <td>
                        <asp:TextBox ID="txt_g_no" Width="100%" Enabled="false" runat="server" Style="text-align: center;" Text='<%#Eval("g_no") %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="txt_name" Width="100%" Style="text-align: center;" runat="server" Text='<%#Eval("name") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_amount" Width="100%" Style="text-align: center;" runat="server" Text='<%#Eval("amount") %>' onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="ddl_type" Style="width: 100%; text-align: center;" runat="server">
                        <asp:ListItem Text="扣    款" Value="R"></asp:ListItem>
                        <asp:ListItem Text="补    款" Value="B"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>
                        <ExportDrawbackManagement:CalendarBox ID="cb_apply_date" runat="server" Style="text-align: center;" Width="100%" Text='<%#Eval("apply_date") %>' FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                    <td>
                        <asp:TextBox ID="txt_note" Width="100%" Style="text-align: center;" runat="server" Text='<%#Eval("note") %>'></asp:TextBox></td>
                </tr>


            </tbody>
        </table>
    </div>

    <ul class="queryarea">
        <li style="width: 50%">
            <asp:Button ID="btnSave" runat="server" Text="保  存" OnClick="btnSave_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 50%">
            <asp:TextBox ID="query_bill_no" Width="180px" runat="server"></asp:TextBox>

            <asp:Button ID="Button2" runat="server" Text="查  询" Style="margin-left: 5%;" OnClick="Button2_Click" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" PageSize="15"
        AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        OnRowCommand="GridView1_RowCommand"
        OnRowDeleting="GridView1_RowDeleting"
        OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowDataBound="GridView1_RowDataBound"
       >
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("bill_no")+","+Eval("g_no")+","+Eval("amount")+","+Eval("type")  %>' />
                    <asp:HiddenField ID="hnf_check_status" Value='<%# Eval("check_status") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:HyperLinkField DataTextField="bill_no" HeaderText="单据编码" Target="_blank" DataNavigateUrlFields="bill_no" DataNavigateUrlFormatString="payment_InDecrease_detail.aspx?id={0}" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="name" HeaderText="费用名称" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N}" HeaderText="金额" />
            <asp:BoundField DataField="type" HeaderText="费用类型" />
            <asp:BoundField DataField="apply_date" DataFormatString="{0:d}" HeaderText="申请日期" />
            <asp:BoundField DataField="note" DataFormatString="{0:N}" HeaderText="备注" />
        </Columns>
    </asp:GridView>
</asp:Content>

