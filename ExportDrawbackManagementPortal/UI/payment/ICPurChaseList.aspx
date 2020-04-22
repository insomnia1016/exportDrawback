<%@ Page Title="采购发票明细查询" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ICPurChaseList.aspx.cs" Inherits="UI_payment_ICPurChaseList" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <ul class="queryarea">
        <li style="width: 30%">
            <span class="title">采购发票号</span>
            <span class="control">
                <asp:TextBox ID="txt_fbillno_id" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li style="width: 50%">
            <span class="title">供应商</span>
            <span class="control">
                <asp:TextBox ID="txt_customer" Width="380px" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" Enabled="true" TargetControlID="txt_customer" ServicePath="~/webservices/Suppliery.asmx" ServiceMethod="SelectSearchInfo" CompletionSetCount="10" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>
            </span>
        </li>
         <li style="width: 30%">
            <span class="title">单据状态</span>
            <span class="control">
                <asp:DropDownList ID="ddl_check_status" runat="server">
                    <asp:ListItem Text="请选择" Value="" ></asp:ListItem>
                    <asp:ListItem Text="未核销" Value="0" ></asp:ListItem>
                    <asp:ListItem Text="部分核销" Value="1" ></asp:ListItem>
                    <asp:ListItem Text="完全核销" Value="2" ></asp:ListItem>
                </asp:DropDownList>
            </span>
        </li>
        <li style="width: 40%">
            <span class="title">发票时间</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="cb_start_time" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
            </span>
            --
                <ExportDrawbackManagement:CalendarBox ID="cb_end_time" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>

            <asp:Button ID="Button1" runat="server" Text="查  询" Style="margin-left: 5%;" OnClick="Button1_Click" Width="100px" />
        </li>
       
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AllowPaging="True" AllowSorting="True" PageSize="20"
        AutoGenerateColumns="False"
        OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:HyperLinkField DataTextField="receipt_id" HeaderText="付款票号" Target="_blank" DataNavigateUrlFields="receipt_id" DataNavigateUrlFormatString="payment_detail.aspx?id={0}" />
            <asp:BoundField DataField="FBillNo" HeaderText="采购发票号" />
            <asp:BoundField DataField="FDate" DataFormatString="{0:d}" HeaderText="单据日期" />
            <asp:BoundField DataField="FName" HeaderText="供应商" />
            <asp:BoundField DataField="FPurchaseAmountFor" DataFormatString="{0:N}" HeaderText="发票金额" />
            <asp:BoundField DataField="FPayAmountFor" DataFormatString="{0:N}" HeaderText="已支付金额" />
            <asp:BoundField DataField="FUnPayAmountFor" DataFormatString="{0:N}" HeaderText="未支付金额" />
            <asp:BoundField DataField="FCurrencyID" HeaderText="币制" />
            <asp:BoundField DataField="FCheckStatus" HeaderText="单据状态" />
            <asp:BoundField DataField="FNote" HeaderText="备注" />
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
</asp:Content>

