<%@ Page Title="利润预算列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ProfitBudgetList.aspx.cs" Inherits="UI_QueryAndReports_ProfitBudgetList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="grid" runat="server">
        <ul class="queryarea">
            <li><span class="title">销售订单号: </span>
                <span class="control">
                    <asp:Label ID="lbl_sale_bill_no" runat="server" Text=""></asp:Label>
                </span>
            </li>
            <li><span class="title">额外费用</span>
                <span class="control">
                    <asp:TextBox ID="txt_extra_charges" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                </span>
            </li>
        </ul>
        <asp:GridView ID="GridView1" runat="server" ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:BoundField DataField="buy_bill_no" HeaderText="采购订单号" />
                <asp:BoundField DataField="dept_id" HeaderText="业务部门代码" />
                <asp:BoundField DataField="emp_id" HeaderText="业务员" />
                <asp:BoundField DataField="sale_price" DataFormatString="{0:N2}" HeaderText="销售价" />
                <asp:TemplateField HeaderText="汇率">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_exchange_rate" Text='<%#Eval("exchange_rate","{0:f2}") %>' AutoPostBack="true" OnTextChanged="txt_exchange_rate_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="buy_price" DataFormatString="{0:N2}" HeaderText="采购价" />
                <asp:BoundField DataField="sale_qty" DataFormatString="{0:N2}" HeaderText="销售数量" />
                <asp:BoundField DataField="buy_qty" DataFormatString="{0:N2}" HeaderText="采购数量" />
                <asp:TemplateField HeaderText="配件名称">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory" Text='<%#Eval("accessory") %>' runat="server" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="配件价格">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory_price" Text='<%#Eval("accessory_price","{0:f2}") %>'  runat="server" AutoPostBack="true" OnTextChanged="txt_accessory_price_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="利润（元）">
                    <ItemTemplate>
                        <asp:Label ID="lbl_profit" Text='<%#Eval("profit","{0:f2}") %>'  runat="server" ></asp:Label>
                        <asp:HiddenField ID="hdf_finter_id" Value='<%#Eval("FInterID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_sale_fentry_id" Value='<%#Eval("Sale_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_buy_fentry_id" Value='<%#Eval("Buy_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_fitem_id" Value='<%#Eval("FItemID") %>' runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lbl_profit_all" runat="server" Text='<%#Eval("profit","{0:f2}") %>'></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <ul class="queryarea">
            <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
                <asp:Button ID="submit" runat="server" Enabled="false" OnClientClick="if(!confirm('确定提交审核吗?审核通过后不可修改！')) return false;" Text="提交审核" OnClick="submit_Click" Style="margin-left: 40%;" Width="100px" />
                <asp:Label ID="Label2" ForeColor="Red" Visible="false" runat="server"></asp:Label>

            </li>
        </ul>
    </div>
    <div>
        <asp:GridView ID="GridView2" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDeleting" ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("sale_bill_no") %>' />
                        <asp:HiddenField ID="hdf_finter_id" Value='<%#Eval("FInterID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sale_bill_no" HeaderText="销售订单号" />
                <asp:BoundField DataField="extra_charges" DataFormatString="{0:N2}" HeaderText="额外费用" />
                <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                <asp:BoundField DataField="profit_all" DataFormatString="{0:N2}" HeaderText="总利润" />
                <asp:BoundField DataField="audit_state" HeaderText="审核状态" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

