<%@ Page Title="退税明细" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="taxlist.aspx.cs" Inherits="UI_QueryAndReports_taxlist" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        {
            function selectAll(bool) {
                var ctl = document.getElementById('<%=GridView1.ClientID %>');
                 var checkbox = ctl.getElementsByTagName('input');
                 for (var i = 0; i < checkbox.length; i++) {
                     if (checkbox[i].type == 'checkbox') {
                         checkbox[i].checked = bool;
                     }
                 }
             }
             }
    </script>
    <div id="Div1" runat="server">
        <ul class="queryarea">
            <li><span class="title">报关单号</span>
                <span class="control">
                    <asp:TextBox ID="txt_entryId" runat="server"></asp:TextBox>
                </span>
            </li>
            <li><span class="title">销售发票号</span>
                <span class="control">
                    <asp:TextBox ID="txt_sale_bill_no" runat="server"></asp:TextBox>
                </span>
            </li>
            <li>
                <asp:Button ID="btn_Query" runat="server" Text="查   询" OnClick="btn_Query_Click" Width="100px" />
            </li>
        </ul>
    </div>

    <ul class="queryarea">
        <li><span class="title">申报状态</span>
            <span class="control">
                <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            </span>
        </li>
    </ul>
    <div id="shenbao" runat="server">
        <ul class="queryarea">
            <li><span class="title">退税申报日期</span>
                <span class="control">

                    <cc1:CalendarBox ID="CalendarBox1" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>

                </span>
            </li>
            <li><span class="title">申报批次号</span>
                <span class="control">

                    <asp:TextBox ID="txt_tax_return_no" runat="server"></asp:TextBox>

                </span>
            </li>
        </ul>
    </div>
    <div id="tuishui" runat="server">
        <ul class="queryarea">
            <li><span class="title">退税日期</span>
                <span class="control">

                    <cc1:CalendarBox ID="CalendarBox2" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>

                </span>

        </ul>
    </div>
    <ul class="queryarea">
        <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
            <asp:Button ID="updateUser" runat="server" Text="保   存" Style="margin-left: 40%;" OnClick="updateUser_Click" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="id" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" AllowSorting="True" PageSize="20" OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBinding="GridView1_DataBinding">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <HeaderTemplate>
                    <asp:CheckBox runat="server" ID="cbHead" onclick="javascript:selectAll(this.checked);"></asp:CheckBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                    <asp:HiddenField ID="hdfId" runat="server" Value='<%#Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="agent_name" HeaderText="申报公司" />
            <asp:BoundField DataField="d_date" DataFormatString="{0:d}" HeaderText="申报日期" />
            <asp:BoundField DataField="owner_name" HeaderText="客户" />
            <asp:BoundField DataField="entry_id" HeaderText="报关单号" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="g_name" HeaderText="品名" />
            <asp:BoundField DataField="g_qty" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="数量" />
            <asp:BoundField DataField="g_unit" HeaderText="单位" />
            <asp:BoundField DataField="trade_curr" HeaderText="币制" />
            <asp:BoundField DataField="decl_price" DataFormatString="{0:N}"  HeaderText="报关单价" />
            <asp:BoundField DataField="decl_total" HeaderText="报关金额" />
            <asp:BoundField DataField="code_ts" HeaderText="海关编码" />
            <asp:BoundField DataField="drawback_rate" DataFormatString="{0:N3}" HtmlEncode="false" HeaderText="出口退税率" />
            <asp:BoundField DataField="invoice_price" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="开票单价" />
            <asp:BoundField DataField="invoice_total" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="开票金额" />
            <asp:BoundField DataField="tax_return_price" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="退税额" />
            <asp:BoundField DataField="tax_return_total" DataFormatString="{0:N}" HtmlEncode="false" HeaderText="退税总金额" />
            <asp:BoundField DataField="state_code" HeaderText="状态" />
            <asp:BoundField DataField="tax_return_date" DataFormatString="{0:d}" HeaderText="退税日期" />
            <asp:BoundField DataField="tax_retutn_d_date" DataFormatString="{0:d}" HeaderText="退税申报日期" />
            <asp:BoundField DataField="tax_return_no" HeaderText="申报批次号" />
            <asp:BoundField DataField="sale_bill_no" HeaderText="销售发票号" />

        </Columns>
    </asp:GridView>
</asp:Content>

