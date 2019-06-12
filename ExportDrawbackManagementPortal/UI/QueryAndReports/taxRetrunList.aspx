<%@ Page Title="退税单查询" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="taxRetrunList.aspx.cs" Inherits="UI_QueryAndReports_taxRetrunList" %>
<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ul class="queryarea">
        <li><span class="title">客  户</span>
            <span class="control">
                <asp:TextBox ID="txt_owner_name" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">报关单号</span>
            <span class="control">
                <asp:TextBox ID="txt_entry_id" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">品  名</span>
            <span class="control">
                <asp:TextBox ID="txt_g_name" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">海关编码</span>
            <span class="control">
                <asp:TextBox ID="txt_code_ts" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">申报状态</span>
            <span class="control">
                <asp:DropDownList ID="DropDownList1"  runat="server" ></asp:DropDownList>
            </span>
        </li>
    
        <li><span class="title">申报日期</span>
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound"  >
        <Columns>
            <asp:BoundField DataField="agent_name" HeaderText="申报公司" />
            <asp:BoundField DataField="d_date" DataFormatString="{0:yyyy-MM-dd}" HeaderText="申报日期" />
            <asp:BoundField DataField="owner_name" HeaderText="客户" />
            <asp:BoundField DataField="entry_id" HeaderText="报关单号" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="g_name" HeaderText="品名" />
            <asp:BoundField DataField="g_qty"  DataFormatString="{0:N}" HtmlEncode="false" HeaderText="数量" />
            <asp:BoundField DataField="g_unit" HeaderText="单位" />
            <asp:BoundField DataField="trade_curr" HeaderText="币制" />
            <asp:BoundField DataField="decl_price" HeaderText="报关单价" />
            <asp:BoundField DataField="decl_total" HeaderText="报关金额" />
            <asp:BoundField DataField="code_ts" HeaderText="海关编码" />
            <asp:BoundField DataField="drawback_rate"  DataFormatString="{0:N3}" HtmlEncode="false" HeaderText="出口退税率" />
            <asp:BoundField DataField="invoice_price" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="开票单价" />
            <asp:BoundField DataField="invoice_total" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="开票金额" />
            <asp:BoundField DataField="tax_return_price" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="退税额" />
            <asp:BoundField DataField="tax_return_total" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="退税总金额" />
            <asp:BoundField DataField="state_code" HeaderText="状态" />
            <asp:BoundField DataField="tax_return_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="退税日期" />
            <asp:BoundField DataField="tax_retutn_d_date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"  HeaderText="退税申报日期" />
            <asp:BoundField DataField="tax_return_no" HeaderText="申报批次号" />
        </Columns>
    </asp:GridView>
</asp:Content>

