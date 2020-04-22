<%@ Page Title="付款申请列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="payment_list.aspx.cs" Inherits="UI_payment_payment_list" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ul class="queryarea">
        <li><span class="title">付款申请单号</span>
            <span class="control">
                <asp:TextBox ID="txt_payment_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">付款单位</span>
            <span class="control">
                <asp:TextBox ID="txt_payee_unit" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">开户行</span>
            <span class="control">
                <asp:TextBox ID="txt_payee_opening_bank" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">账号</span>
            <span class="control">
                <asp:TextBox ID="txt_payee_account_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">客户订单号</span>
            <span class="control">
                <asp:TextBox ID="txt_icsale_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">工厂订单号</span>
            <span class="control">
                <asp:TextBox ID="txt_poorder_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">产品货号</span>
            <span class="control">
                <asp:TextBox ID="txt_goods_model" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">申请人</span>
            <span class="control">
                <asp:TextBox ID="txt_emp_name" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">付款申请日期</span>
            <span class="control">
                <cc1:CalendarBox ID="CalendarBox1" FormatString="yyyy-MM-dd" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
            </span>
            至
            <span class="control">
                <cc1:CalendarBox ID="CalendarBox2"  FormatString="yyyy-MM-dd"  ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
            </span>
        </li>

    </ul>

    <ul class="queryarea">
        <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
            <asp:Button ID="query" runat="server" Text="查    询" OnClick="query_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="True" 
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20"
        OnRowDeleting="GridView1_RowDeleting"
        OnRowCommand="GridView1_RowCommand"
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:HyperLinkField DataTextField="payment_id" HeaderText="付款申请单号" Target="_blank" DataNavigateUrlFields="payment_id" DataNavigateUrlFormatString="payment_details.aspx?id={0}" />
            <asp:BoundField DataField="payment_date" DataFormatString="{0:d}" HeaderText="申请日期" />
            <asp:BoundField DataField="payee_unit" HeaderText="付款单位" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N2}" HeaderText="金额" />
            <asp:BoundField DataField="payee_opening_bank" HeaderText="开户行" />
            <asp:BoundField DataField="payee_account_id" HeaderText="账号" />
            <asp:BoundField DataField="customer_bill_no" HeaderText="客户订单号" />
            <asp:BoundField DataField="factory_bill_no" HeaderText="工厂订单号" />
            <asp:BoundField DataField="goods_model" HeaderText="产品货号" />
            <asp:BoundField DataField="payment_explain" HeaderText="付款说明" />
             <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("payment_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

