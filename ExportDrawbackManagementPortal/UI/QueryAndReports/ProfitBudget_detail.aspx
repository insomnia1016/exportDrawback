<%@ Page Title="订单利润预算" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ProfitBudget_detail.aspx.cs" Inherits="UI_QueryAndReports_ProfitBudget_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ul class="queryarea">
        <li><span class="title">销售订单号:</span>
            <span class="control" style="margin-left:20px;">
                <asp:Label ID="lblt_sale_bill_no" runat="server" ></asp:Label>
            </span>
        </li>
        <li><span class="title">额外费用:</span>
            <span class="control" style="margin-left:20px;">
                <asp:Label ID="lbl_extra_charges" runat="server" ></asp:Label>
            </span>
        </li>
        <li><span class="title">部门:</span>
            <span class="control" style="margin-left:20px;">
                <asp:Label ID="lbl_dept_id" runat="server" ></asp:Label>
            </span>
        </li>
        <li><span class="title">业务员:</span>
            <span class="control" style="margin-left:20px;">
                <asp:Label ID="lbl_emp" runat="server" ></asp:Label>
            </span>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" >
        <Columns>
            <asp:BoundField DataField="buy_bill_no" HeaderText="采购订单号" />
            <asp:BoundField DataField="FName" HeaderText="产品名称" />
            <asp:BoundField DataField="FNumber" HeaderText="货号" />
            <asp:BoundField DataField="sale_price" DataFormatString="{0:N2}" HeaderText="销售价" />
            <asp:BoundField DataField="currency" HeaderText="币制" />
            <asp:BoundField DataField="exchange_rate" DataFormatString="{0:N2}" HeaderText="汇率" />
            <asp:BoundField DataField="sale_rate" HeaderText="含税" />
            <asp:BoundField DataField="buy_price" DataFormatString="{0:N2}" HeaderText="采购价" />
            <asp:BoundField DataField="buy_rate" HeaderText="含税" />
            <asp:BoundField DataField="sale_qty" DataFormatString="{0:N2}" HeaderText="销售数量" />
            <asp:BoundField DataField="buy_qty" DataFormatString="{0:N2}" HeaderText="采购数量" />
            <asp:BoundField DataField="accessory"  HeaderText="配件名称" />
            <asp:BoundField DataField="accessory_price" DataFormatString="{0:N2}" HeaderText="配件价格" />
            <asp:TemplateField HeaderText="箱规(cm)">
                <ItemTemplate>
                    长：<asp:Label ID="lbl_length" runat="server" Text='<%#Eval("length") %>'></asp:Label>
                    宽：<asp:Label ID="lbl_width" runat="server" Text='<%#Eval("width") %>'></asp:Label>
                    高：<asp:Label ID="lbl_height" runat="server" Text='<%#Eval("height") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="volume" DataFormatString="{0:N2}" HeaderText="体积(m³)" />
            <asp:BoundField DataField="estimate_freight_charge" DataFormatString="{0:N2}" HeaderText="预估运费" />
            <asp:BoundField DataField="capacity" DataFormatString="{0:N2}" HeaderText="入数" />
            <asp:BoundField DataField="tax_rate" DataFormatString="{0:N2}" HeaderText="税点" />
            <asp:BoundField DataField="return_rate" DataFormatString="{0:N2}" HeaderText="退税率" />
            <asp:BoundField DataField="profit" DataFormatString="{0:N2}" HeaderText="利润率" />
        </Columns>
    </asp:GridView>
    
   
</asp:Content>

