<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="entryList.aspx.cs" Inherits="UI_QueryAndReports_entryList" %>
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
        <li><span class="title">申报公司</span>
            <span class="control">
                <asp:TextBox ID="txt_agent_name" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">录入员</span>
            <span class="control">
                <asp:TextBox ID="txt_operator" runat="server"></asp:TextBox>
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

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" >
        <Columns>
            
            <asp:BoundField DataField="entry_id" HeaderText="报关单号" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="owner_name" HeaderText="客户" />
            <asp:BoundField DataField="d_date" HeaderText="申报时间" />
            <asp:BoundField DataField="agent_name" HeaderText="申报公司" />
            <asp:BoundField DataField="g_name" HeaderText="商品名称" />
            <asp:BoundField DataField="g_qty" DataFormatString="{0:N}" HeaderText="法定数量" />
            <asp:BoundField DataField="g_unit" HeaderText="法定单位" />
            <asp:BoundField DataField="decl_price" DataFormatString="{0:N}" HeaderText="申报单价" />
            <asp:BoundField DataField="decl_total" DataFormatString="{0:N}" HeaderText="申报总价" />
            <asp:BoundField DataField="code_ts" HeaderText="商品编码" />
            <asp:BoundField DataField="drawback_rate" DataFormatString="{0:N3}" HeaderText="退税率" />
            <asp:BoundField DataField="operator" HeaderText="操作员" />
            <asp:BoundField DataField="operate_time" HeaderText="操作时间" />
        </Columns>
    </asp:GridView>
</asp:Content>

