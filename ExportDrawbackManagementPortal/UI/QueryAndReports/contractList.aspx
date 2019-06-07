<%@ Page Title="合同查询" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="contractList.aspx.cs" Inherits="UI_QueryAndReports_contractList" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ul class="queryarea">
        <li><span class="title">合同号</span>
            <span class="control">
                <asp:TextBox ID="txt_contract_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">报关单号</span>
            <span class="control">
                <asp:TextBox ID="txt_entry_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">需方公司</span>
            <span class="control">
                <asp:TextBox ID="txt_xufang" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">需方经办人</span>
            <span class="control">
                <asp:TextBox ID="txt_xufang_jingbanren" runat="server"></asp:TextBox>
            </span>
        </li>
    </ul>
    <ul class="queryarea">
        <li><span class="title">供货日期</span>
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:HyperLinkField DataTextField="contract_id" HeaderText="合同号" Target="_blank" DataNavigateUrlFields="contract_id" DataNavigateUrlFormatString="contract_detail.aspx?id={0}" />
            <asp:BoundField DataField="xufang" HeaderText="需方" />
            <asp:BoundField DataField="delivery_date" DataFormatString="{0:d}" HeaderText="交货日期" />
            <asp:BoundField DataField="invoice_all" DataFormatString="{0:N2}" HeaderText="开票金额" />
            <asp:BoundField DataField="delivery_mode" HeaderText="交货方式" />
        </Columns>
    </asp:GridView>
</asp:Content>

