<%@ Page Title="实际利润核算审核" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ActualProfitAccountingAudit.aspx.cs" Inherits="UI_Profit_ActualProfitAccountingAudit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div>
        <ul class="queryarea">
            <li><span class="title">销售订单号: </span>
                <span class="control">
                    <asp:TextBox ID="txt_bill_no" runat="server" Style="text-align: center"></asp:TextBox>
                </span>
                <asp:Button ID="btn_query" runat="server" OnClick="btn_query_Click" Text="查询" />
            </li>
        </ul>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"  AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="审核">
                    <ItemTemplate>
                        <asp:Button ID="btn_approve" runat="server" Text="通过"  CommandArgument="true"  OnClick="audit_Click"/>
                        <asp:Button ID="btn_Unapprove" runat="server" Text="不通过" CommandArgument="false" OnClick="audit_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="sale_bill_no" HeaderText="销售发单号" Target="_blank" DataNavigateUrlFields="sale_bill_no" DataNavigateUrlFormatString="ProfitDetail.aspx?id={0}" />
                <asp:BoundField DataField="actual_amount" DataFormatString="{0:N2}" HeaderText="实际结汇收入" />
                <asp:BoundField DataField="return_tax" DataFormatString="{0:N2}" HeaderText="实际退税" />
                <asp:BoundField DataField="actual_pay" DataFormatString="{0:N2}" HeaderText="采购产品费用" />
                <asp:BoundField DataField="extra_charges" DataFormatString="{0:N2}" HeaderText="额外费用" />
                <asp:BoundField DataField="commission" DataFormatString="{0:N3}" HeaderText="提成" />
                <asp:BoundField DataField="actual_profit_amount" DataFormatString="{0:N2}" HeaderText="实际利润" />
                <asp:BoundField DataField="actual_profit" DataFormatString="{0:N3}" HeaderText="实际利润率" />
                <asp:BoundField DataField="audit_status" DataFormatString="{0:N3}" HeaderText="审核状态" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

