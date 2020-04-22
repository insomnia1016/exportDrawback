<%@ Page Title="账户详细列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="accountDetailsList.aspx.cs" Inherits="UI_Setting_accountDetailsList" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ul class="queryarea">
        <li><span class="title">收款账户</span>
            <span class="control">
                <asp:DropDownList ID="ddl_account" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">收款日期</span>
            <span class="control">
                <cc1:CalendarBox ID="CalendarBox1" FormatString="yyyy-MM-dd 00:00:00" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
            </span>
            至
            <span class="control">
                <cc1:CalendarBox ID="CalendarBox2" FormatString="yyyy-MM-dd 23:59:59" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
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
    <hr />
    <ul>
        <li>
            <span class="title">收款账户:</span>
            <span class="control" style="padding-left:10px">
                <asp:Label ID="lbl_account_id" runat="server"></asp:Label>
            </span>
        </li>
        <li>
            <span class="title">户名:</span>
            <span class="control" style="padding-left:10px">
                <asp:Label ID="lbl_account_name" runat="server"></asp:Label>
            </span>
        </li>
        <li>
            <span class="title">开户行:</span>
            <span class="control" style="padding-left:10px">
                <asp:Label ID="lbl_opening_bank" runat="server"></asp:Label>
            </span>
        </li>
         <li>
            <span class="title">币制:</span>
            <span class="control" style="padding-left:10px">
                <asp:Label ID="lbl_currency" runat="server"></asp:Label>
            </span>
        </li>
         <li>
            <span class="title">金额:</span>
            <span class="control" style="padding-left:10px">
                <asp:Label ID="lbl_amount" runat="server"></asp:Label>
            </span>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true"
        OnRowDataBound="GridView1_RowDataBound" AllowPaging="True"
        OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
        <Columns>
            <asp:BoundField DataField="account_id" HeaderText="账号" />
            <asp:HyperLinkField DataTextField="receipt_id" HeaderText="收款单号" Target="_blank" DataNavigateUrlFields="receipt_id" DataNavigateUrlFormatString="../QueryAndReports/receipt_detail.aspx?id={0}" />
            <asp:BoundField DataField="operate_time" HeaderText="收款日期" />
            <asp:BoundField DataField="operater" DataFormatString="{0:d}" HeaderText="操作员" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N2}" HeaderText="金额" />
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
</asp:Content>
