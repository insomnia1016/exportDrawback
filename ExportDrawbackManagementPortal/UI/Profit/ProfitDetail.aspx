<%@ Page Title="订单利润详细" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ProfitDetail.aspx.cs" Inherits="UI_Profit_ProfitDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
        <ul class="queryarea">
            <li><span class="title">额外费用:</span>
                <span class="control" style="padding-left:10px">
                    <asp:Label ID="lbl_extra_charges"  runat="server" Text="Label"></asp:Label>
                </span>
            </li>
            <li style="width: 20%"><span class="title">部门</span>
            <span class="control">
                <asp:TextBox ID="txt_dept_id" Enabled="false" runat="server"></asp:TextBox>
            </span>
        </li>
        <li style="width: 20%"><span class="title">业务员</span>
            <span class="control">
                <asp:TextBox ID="txt_emp" Enabled="false" runat="server"></asp:TextBox>
            </span>
        </li>
        </ul>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" ShowFooter="true" PageSize="20">
            <Columns>
                <asp:BoundField DataField="buy_bill_no" HeaderText="采购订单号" />
                <asp:BoundField DataField="FName" HeaderText="产品名称" />
                <asp:BoundField DataField="FNumber" HeaderText="货号" />
                <asp:BoundField DataField="sale_price" DataFormatString="{0:N2}" HeaderText="销售价" />
                <asp:BoundField DataField="currency"  HeaderText="币制" />
                <asp:BoundField DataField="exchange_rate" DataFormatString="{0:N2}" HeaderText="汇率" />
                <asp:BoundField DataField="sale_rate"  HeaderText="含税" />
                <asp:BoundField DataField="buy_price" DataFormatString="{0:N2}" HeaderText="采购价" />
                <asp:BoundField DataField="buy_rate"  HeaderText="含税" />
                <asp:BoundField DataField="sale_qty" DataFormatString="{0:N2}" HeaderText="销售数量" />
                <asp:BoundField DataField="buy_qty" DataFormatString="{0:N2}" HeaderText="采购数量" />
                <asp:BoundField DataField="accessory"  HeaderText="配件名称" />
                <asp:BoundField DataField="accessory_price" DataFormatString="{0:N2}" HeaderText="配件价格" />
                 <asp:TemplateField HeaderText="箱规(cm)">
                    <ItemTemplate>
                        长：<asp:Label ID="lbl_length" runat="server" Text='<%#Eval("length") %>' Width="40px"  Style="text-align: center"></asp:Label>
                        宽：<asp:Label ID="lbl_width" runat="server" Text='<%#Eval("width") %> ' Width="40px"  Style="text-align: center"></asp:Label>
                        高：<asp:Label ID="lbl_height" runat="server" Text='<%#Eval("height") %>' Width="40px"  Style="text-align: center" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="volume" DataFormatString="{0:N3}" HeaderText="体积(m³)" />
                <asp:BoundField DataField="estimate_freight_charge" DataFormatString="{0:N2}" HeaderText="预估运费" />
                <asp:BoundField DataField="capacity" DataFormatString="{0:N2}" HeaderText="入数" />
                <asp:BoundField DataField="tax_rate" DataFormatString="{0:N2}" HeaderText="税点" />
                <asp:BoundField DataField="return_rate" DataFormatString="{0:N2}" HeaderText="退税率" />
                <asp:BoundField DataField="profit" DataFormatString="{0:N3}" HeaderText="利润率" />
                <asp:BoundField DataField="commission" DataFormatString="{0:N3}" HeaderText="提成" />
                <asp:TemplateField HeaderText="销售订单">
                    <ItemTemplate>
                        <asp:Label ID="SEOrderID" Value='<%#Eval("SEOrderID") %>' runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdf_dept_id" Value='<%#Eval("dept_id") %>' runat="server" />
                        <asp:HiddenField ID="hdf_emp_id" Value='<%#Eval("emp_id") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="LightCyan" HorizontalAlign="Center" ForeColor="MediumBlue" />

        </asp:GridView>
</asp:Content>

