<%@ Page Title="收款单修改" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ReceiptEdit.aspx.cs" Inherits="UI_QueryAndReports_ReceiptEdit" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <ul class="queryarea">
        <li style="width: 50%">
            <span class="title">单据号</span>
            <span class="control">
                <asp:TextBox ID="query_receipt_id" Width="180px" runat="server"></asp:TextBox>
            </span>
            <asp:Button ID="Button2" runat="server" Text="查  询" Style="margin-left: 5%;" OnClick="Button2_Click" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AllowSorting="True" PageSize="20"
        AutoGenerateColumns="False"
        OnRowCommand="GridView3_RowCommand"
        OnRowDeleting="GridView3_RowDeleting"
        OnPageIndexChanging="GridView3_PageIndexChanging"
        OnRowDataBound="GridView3_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("receipt_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataTextField="receipt_id" HeaderText="单据号" Target="_blank" DataNavigateUrlFields="receipt_id" DataNavigateUrlFormatString="receipt_detail.aspx?id={0}" />
            <asp:BoundField DataField="receipt_date" DataFormatString="{0:d}" HeaderText="单据日期" />
            <asp:BoundField DataField="receipt_type" HeaderText="费用名称" />
            <asp:BoundField DataField="customer_name" HeaderText="客户" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N}" HeaderText="表头金额" />
            <asp:BoundField DataField="currency" HeaderText="币制" />
            <asp:BoundField DataField="receipt_charge" DataFormatString="{0:N}" HeaderText="手续费" />
            <asp:BoundField DataField="FDeptID" HeaderText="部门" />
            <asp:BoundField DataField="FEmpID" HeaderText="业务员" />
            <asp:BoundField DataField="FChecker" HeaderText="审核人" />
            <asp:BoundField DataField="FCheckDate" DataFormatString="{0:d}" HeaderText="审核日期" />
             <asp:TemplateField HeaderText="审核操作">
                <ItemTemplate>
                    <asp:Button ID="btn_audit" runat="server" OnClick="btn_audit_Click" Text="通过" CommandArgument="Y" />
                    <asp:HiddenField ID="hdf_account_id" Value='<%#Eval("account_id") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="audit_status" HeaderText="审核状态" />
            <asp:BoundField DataField="FCheckStatus" HeaderText="单据状态" />
        </Columns>
    </asp:GridView>
</asp:Content>

