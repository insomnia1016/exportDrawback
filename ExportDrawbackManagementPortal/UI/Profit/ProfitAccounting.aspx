<%@ Page Title="订单利润核算" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ProfitAccounting.aspx.cs" Inherits="UI_Profit_ProfitAccounting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <ul class="queryarea">
        <li style="width: 100%"><span class="title">销售发单号</span>
            <span class="control">
                <asp:TextBox ID="txt_sale_bill_no" Width="180px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txt_sale_bill_no" ErrorMessage="销售订单号不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li style="width: 100%"><span class="title" style="width: 20%">销售发票下原销售订单额外费用总和：</span>
            <span class="control">
                <asp:TextBox ID="txt_extra_charges_lists_all" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li style="width: 20%"><span class="title">额外费用</span>
            <span class="control">
                <asp:TextBox ID="txt_extra_charges" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
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
        <li>
            <asp:Button ID="query" runat="server" OnClick="query_Click" Text="查   询" />
        </li>
        <li>
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
            <Columns>
                <asp:BoundField DataField="buy_bill_no" HeaderText="采购订单号" />
                <asp:BoundField DataField="FName" HeaderText="产品名称" />
                <asp:BoundField DataField="FNumber" HeaderText="货号" />
                <asp:BoundField DataField="sale_price" DataFormatString="{0:N2}" HeaderText="销售价" />
                <asp:BoundField DataField="currency" HeaderText="币制" />
                <asp:TemplateField HeaderText="汇率">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_exchange_rate" Width="50px" Text='<%#Eval("exchange_rate","{0:f2}") %>' AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="sale_rate" HeaderText="含税" />
                <asp:BoundField DataField="buy_price" DataFormatString="{0:N2}" HeaderText="采购价" />
                <asp:BoundField DataField="buy_rate" HeaderText="含税" />
                <asp:BoundField DataField="sale_qty" DataFormatString="{0:N2}" HeaderText="销售数量" />
                <asp:BoundField DataField="buy_qty" DataFormatString="{0:N2}" HeaderText="采购数量" />
                <asp:TemplateField HeaderText="配件名称">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory" Text='<%#Eval("accessory") %>' Width="80px" runat="server" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="配件价格">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_accessory_price" Text='<%#Eval("accessory_price","{0:f2}") %>' runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="箱规(cm)">
                    <ItemTemplate>
                        长：<asp:TextBox ID="txt_length" Text='<%#Eval("length","{0:f2}") %>' runat="server" AutoPostBack="true" Width="50px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                        宽：<asp:TextBox ID="txt_width" Text='<%#Eval("width","{0:f2}") %>' runat="server" AutoPostBack="true" Width="50px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                        高：<asp:TextBox ID="txt_height" Text='<%#Eval("height","{0:f2}") %>' runat="server" AutoPostBack="true" Width="50px" OnTextChanged="txt_volume_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="体积(m³)">
                    <ItemTemplate>
                        <asp:Label ID="lbl_volume" Text='<%#Eval("volume","{0:f2}") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="预估运费">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_estimate_freight_charge" Text='<%#Eval("estimate_freight_charge","{0:f2}") %>' runat="server" Width="50px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="入数">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_capacity" Text='<%#Eval("capacity","{0:f2}") %>' runat="server" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="税点">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_tax_rate" Text='<%#Eval("tax_rate","{0:f2}") %>' runat="server" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="退税率">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_return_rate" Text='<%#Eval("return_rate","{0:f2}") %>' runat="server" Width="30px" AutoPostBack="true" OnTextChanged="txt_Value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="利润率">
                    <ItemTemplate>
                        <asp:Label ID="lbl_profit" runat="server" Text=""></asp:Label>
                        <asp:HiddenField ID="hdf_finter_id" Value='<%#Eval("FInterID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_sale_fentry_id" Value='<%#Eval("Sale_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_buy_fentry_id" Value='<%#Eval("Buy_FEntryID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_fitem_id" Value='<%#Eval("FItemID") %>' runat="server" />
                        <asp:HiddenField ID="hdf_dept_id" Value='<%#Eval("dept_id") %>' runat="server" />
                        <asp:HiddenField ID="hdf_emp_id" Value='<%#Eval("emp_id") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提成">
                    <ItemTemplate>
                        <asp:Label ID="lbl_commission" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FOrderBillNo" HeaderText="销售订单" />
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
</asp:Content>

