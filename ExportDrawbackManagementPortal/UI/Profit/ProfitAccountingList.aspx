<%@ Page Title="利润核算列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ProfitAccountingList.aspx.cs" Inherits="UI_Profits_ProfitAccountingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div>
        <ul class="queryarea">
            <li><span class="title">额外费用</span>
                <span class="control">
                    <asp:TextBox ID="txt_extra_charges" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                </span>
            </li>
        </ul>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" ShowFooter="true">
            <Columns>
                <asp:BoundField DataField="buy_bill_no" HeaderText="采购订单号" />
                <asp:BoundField DataField="dept_id" HeaderText="部门" />
                <asp:BoundField DataField="emp_id" HeaderText="业务员" />
                <asp:BoundField DataField="sale_price" DataFormatString="{0:N2}" HeaderText="销售价" />
                <asp:BoundField DataField="currency"  HeaderText="币制" />
                <asp:TemplateField HeaderText="汇率">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_exchange_rate" Width="50px" Text='<%#Eval("exchange_rate","{0:f2}") %>' AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sale_rate"  HeaderText="含税" />
                <asp:BoundField DataField="buy_price" DataFormatString="{0:N2}" HeaderText="采购价" />
                <asp:BoundField DataField="buy_rate"  HeaderText="含税" />
                <asp:BoundField DataField="sale_qty" DataFormatString="{0:N2}" HeaderText="销售数量" />
                <asp:BoundField DataField="buy_qty" DataFormatString="{0:N2}" HeaderText="采购数量" />
                <asp:TemplateField HeaderText="配件名称">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory" Value='<%#Eval("accessory") %>' Width="80px" runat="server" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="配件价格">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory_price" Value='<%#Eval("accessory_price","{0:f2}") %>' runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="箱规(cm)">
                    <ItemTemplate>
                        长：<asp:TextBox ID="txt_length" runat="server" AutoPostBack="true" Value='<%#Eval("length") %>' Width="30px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                        宽：<asp:TextBox ID="txt_width" runat="server" AutoPostBack="true" Value='<%#Eval("width") %>' Width="30px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                        高：<asp:TextBox ID="txt_height" runat="server" AutoPostBack="true" Value='<%#Eval("height") %>' Width="30px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="体积(m³)">
                    <ItemTemplate>
                        <asp:Label ID="lbl_volume" Text='<%#Eval("volume","{0:f3}") %>' runat="server" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预估运费">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_estimate_freight_charge" Value='<%#Eval("estimate_freight_charge","{0:f2}") %>' runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="入数">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_capacity" Value='<%#Eval("capacity","{0:f2}") %>' runat="server" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="税点">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_tax_rate" Value='<%#Eval("tax_rate","{0:f2}") %>' runat="server" Text="0.06" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="退税率">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_return_rate" Value='<%#Eval("return_rate","{0:f2}") %>' runat="server" Text="0.13" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="利润率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_profit" Text='<%#Eval("profit","{0:f3}") %>' runat="server" ></asp:Label>
                        <asp:HiddenField ID="hdf_finter_id" Value='<%#Eval("FInterID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_sale_fentry_id" Value='<%#Eval("Sale_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_buy_fentry_id" Value='<%#Eval("Buy_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_fitem_id" Value='<%#Eval("FItemID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提成">
                    <ItemTemplate>
                         <asp:Label ID="lbl_commission" Text='<%#Eval("commission","{0:f3}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SEOrderID" HeaderText="销售订单" />
            </Columns>
            <FooterStyle BackColor="LightCyan" HorizontalAlign="Center" ForeColor="MediumBlue" />
        </asp:GridView>
        <ul class="queryarea">
            <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
                <asp:Button ID="submit" runat="server" Enabled="false" OnClientClick="if(!confirm('确定提交审核吗?审核通过后不可修改！')) return false;" Text="提交审核" OnClick="submit_Click" Style="margin-left: 40%;" Width="100px" />
                <asp:Label ID="Label2" ForeColor="Red" Visible="false" runat="server"></asp:Label>

            </li>
        </ul>
    </div>
    <div>
         <ul class="queryarea">
            <li><span class="title">销售发单号: </span>
                <span class="control">
                    <asp:TextBox ID="txt_sale_bill_no" runat="server" Style="text-align: center"></asp:TextBox>
                </span>
            
                <asp:Button ID="btn_query" runat="server" OnClick="btn_query_Click" Text="查询" />
            </li>
        </ul>
        <asp:GridView ID="GridView2" runat="server" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowCommand="GridView2_RowCommand" OnRowDeleting="GridView2_RowDeleting" ShowFooter="true" AutoGenerateColumns="False" OnRowDataBound="GridView2_RowDataBound" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("sale_bill_no") %>' />
                        <asp:HiddenField ID="hdf_finter_id" Value='<%#Eval("FInterID") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="sale_bill_no" HeaderText="销售发单号" Target="_blank" DataNavigateUrlFields="sale_bill_no" DataNavigateUrlFormatString="ProfitDetail.aspx?id={0}" />
                <asp:BoundField DataField="extra_charges" DataFormatString="{0:N2}" HeaderText="额外费用" />
                <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                <asp:BoundField DataField="audit_state" HeaderText="审核状态" />
                <asp:BoundField DataField="is_actual_audit" HeaderText="是否做过实际审核" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

