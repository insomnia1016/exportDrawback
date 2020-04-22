<%@ Page Title="定金单修改" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="DepositEdit.aspx.cs" Inherits="UI_payment_DepositEdit" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>定金表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">供应商：</span>
            <span class="control">
                <asp:TextBox ID="customer" Enabled="false" Width="380px" runat="server"></asp:TextBox>

            </span>
        </li>
        <li><span class="title">定金单号：</span>
            <span class="control">
                <asp:TextBox ID="txt_deposit_id" Width="180px" Enabled="false" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
            </span>
        </li>
         <li><span class="title">定金金额：</span>
            <span class="control">
                <asp:TextBox ID="txt_amount_all" Width="180px" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency" Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">申请日期：</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="agent_date" Style="text-align: center;" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>

            </span>
        </li>
        <li><span class="title">申请人：</span>
            <span class="control">
                <asp:DropDownList ID="ddl_agenter" runat="server"></asp:DropDownList>
            </span>
        </li>
    </ul>

    <h2 style="float: left; text-align: left; width: 100%;">定金表体数据</h2>
    <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True">
        <Columns>

            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Eval("g_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售订单号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Style="text-align: center;" onmousedown="PopupDepositWindow();" AutoCompleteType="Disabled" Width="180px" Text='<%#Eval("FBillNo") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_fdate" Text='<%#Eval("Fdate","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amountfor" Text=' <%#Eval("amount","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" Text='<%#Eval("note") %>' Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
     <hr />
    <ul class="queryarea">

        <li style="width: 50%">
            <span class="title">定金单号</span>

            <asp:TextBox ID="query_deposit_id" Width="180px" runat="server"></asp:TextBox>

            <asp:Button ID="Button2" runat="server" Text="查  询" Style="margin-left: 5%;" OnClick="Button2_Click" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>

    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AllowSorting="True" PageSize="15"
        AutoGenerateColumns="False"
        OnSelectedIndexChanged="GridView2_SelectedIndexChanged"
        OnRowCommand="GridView2_RowCommand"
        OnRowDeleting="GridView2_RowDeleting"
        OnPageIndexChanging="GridView2_PageIndexChanging"
        OnRowDataBound="GridView2_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="查看" CommandName="Select" />
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("deposit_id") %>' />
                    <asp:HiddenField ID="hnf_check_status" Value='<%# Eval("check_status") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="deposit_id" HeaderText="定金编号" />
            <asp:BoundField DataField="customer" HeaderText="供应商" />
            <asp:BoundField DataField="currencyID" HeaderText="币制" />
            <asp:BoundField DataField="amount_all" DataFormatString="{0:N}" HeaderText="金额" />
            <asp:BoundField DataField="agenter" HeaderText="申请人" />
            <asp:BoundField DataField="agent_date" DataFormatString="{0:d}" HeaderText="申请日期" />
        </Columns>
    </asp:GridView>
</asp:Content>
