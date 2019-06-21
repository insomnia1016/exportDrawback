<%@ Page Title="待开票合同" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="contract.aspx.cs" Inherits="UI_QueryAndReports_contract" %>

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
    <%--<ul class="queryarea">
        <li><span class="title">供应商</span>
            <span class="control">
                <asp:TextBox ID="txtSupplier" runat="server"></asp:TextBox>
            </span>
        </li>

        <li><span class="title">开票单价</span>
            <span class="control">
                <asp:TextBox ID="txtInvoice_price" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"  runat="server"></asp:TextBox>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </span>
        </li>
        <li><span class="title">交货日期</span>
            <span class="control">
                <cc1:CalendarBox ID="txtDelivery_date" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
            </span>
        </li>

        <li><span class="title">合同签订日期</span>
            <span class="control">
                <cc1:CalendarBox ID="txtContract_date" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar" runat="server"></cc1:CalendarBox>
            </span>
        </li>
        <li><span class="title">退税额</span>
            <span class="control">
                <asp:TextBox ID="txtTax_return_price" ReadOnly="true" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">退税总金额</span>
            <span class="control">
                <asp:TextBox ID="txtTax_return_total" ReadOnly="true" runat="server"></asp:TextBox>
            </span>
        </li>
    </ul>--%>

    <ul class="queryarea">
        <li style="width: 100%">
            <asp:Button ID="updateUser" runat="server" Text="下推采购合同" OnClick="updateUser_Click"  Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  AllowSorting="True" PageSize="20" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" OnDataBinding="GridView1_DataBinding" OnRowDataBound="GridView1_RowDataBound" DataKeyNames="entry_id,g_no,g_name,g_qty,g_unit" >
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <HeaderTemplate>
                    <asp:CheckBox runat="server" ID="cbHead" onclick="javascript:selectAll(this.checked);"></asp:CheckBox>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                    <asp:HiddenField ID="hdfId"  runat="server" Value='<%#Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="entry_id" HeaderText="报关单号" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="owner_name" HeaderText="客户" />
            <asp:BoundField DataField="d_date" HeaderText="申报时间" />
            <asp:BoundField DataField="agent_name" HeaderText="申报公司" />
            <asp:BoundField DataField="g_name" HeaderText="商品名称" />
            <asp:BoundField DataField="g_qty" DataFormatString="{0:N}" HeaderText="法定数量" />
            <asp:BoundField DataField="g_unit" HeaderText="法定单位" />
            <asp:BoundField DataField="trade_curr" HeaderText="成交币制" />
            <asp:BoundField DataField="decl_price" DataFormatString="{0:N}" HeaderText="申报单价" />
            <asp:BoundField DataField="decl_total" DataFormatString="{0:N}" HeaderText="申报总价" />
            <asp:BoundField DataField="code_ts" HeaderText="商品编码" />
            <asp:BoundField DataField="drawback_rate" DataFormatString="{0:N3}" HeaderText="退税率" />
            <asp:BoundField DataField="operator" HeaderText="操作员" />
            <asp:BoundField DataField="operate_time" HeaderText="操作时间" />
        </Columns>
    </asp:GridView>
</asp:Content>

