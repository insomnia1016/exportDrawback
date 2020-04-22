<%@ Page Title="付款单修改" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="payment_detail.aspx.cs" Inherits="UI_payment_payment_detail" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 style="text-align: center;">付款单据</h2>
    <ul class="queryarea">
        <li><span class="title">供应商</span>
            <span class="control">
                <asp:TextBox ID="txt_customer" Enabled="false" Width="380px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="d_date" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
            </span>
        </li>
        <li><span class="title">单据号</span>
            <span class="control">
                <asp:TextBox ID="txt_recetpt_no" Enabled="false" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li style="clear:both;"><span class="title" >付款类型</span>
            <span class="control">
                <asp:DropDownList ID="ddl_receipt_type" Enabled="false" Width="180px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency" Enabled="false" Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">表头付款金额</span>
            <span class="control">
                <asp:TextBox ID="txt_amount" Enabled="false" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li style="clear: both"><span class="title" >手续费</span>
            <span class="control">
                <asp:TextBox ID="txt_receipt_charge" AutoPostBack="true" OnTextChanged="txt_receipt_charge_TextChanged" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">付款账户</span>
            <span class="control">
                <asp:TextBox ID="txt_account" Enabled="false" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
         <li><span class="title">备注</span>
            <span class="control">
                <asp:TextBox ID="txt_note"  Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
    </ul>
     <div runat="server" id="grid1">
    <span class="title" style="clear: both">采购发票列表</span>
    <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="receipt_no" HeaderText="行号" />
            <asp:BoundField DataField="FBillNo" HeaderText="源单编号" />
            <asp:BoundField DataField="FDate" DataFormatString="{0:d}" HeaderText="单据日期" />
            <asp:BoundField DataField="FPurchaseAmountFor" DataFormatString="{0:N}" HeaderText="单据金额" />
            <asp:BoundField DataField="FPayAmountFor" DataFormatString="{0:N}" HeaderText="已支付金额" />
            <asp:BoundField DataField="FUnPayAmountFor" DataFormatString="{0:N}" HeaderText="未支付金额" />
            <asp:TemplateField HeaderText="本次支付（*）">
                <ItemTemplate>
                    <asp:TextBox ID="txt_fcheckamountfor"
                        onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"
                        Style="text-align: center" AutoPostBack="true" OnTextChanged="txt_fcheckamountfor_TextChanged"
                        Width="180px" Text=' <%#Eval("FCheckAmountFor","{0:F}") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FCurrencyID" HeaderText="发票币别" />
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" Text='<%#Eval("FNote") %>' Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
         </div>
    <div runat="server" id="grid2">
        <span class="title">增减单列表</span>
        <asp:GridView ID="GridView2" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
            OnRowDataBound="GridView2_RowDataBound">
            <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Eval("receipt_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Enabled="false" Style="text-align: center;" Width="180px" Text='<%#Eval("InDecrease_no") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户">
                <ItemTemplate>
                    <asp:Label ID="lbl_customer" Text='<%#Eval("customer") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amount_all" Text=' <%#Eval("FPurchaseAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lbl_agenter" Text='<%#Eval("agenter") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_agent_date" Text='<%#Eval("agent_date","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
            <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
                ForeColor="MediumBlue" />
        </asp:GridView>
    </div>
    <div runat="server" visible="false" id="grid3">
    <span class="title">付款通知书列表</span>
    <asp:GridView ID="GridView4" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView4_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Eval("receipt_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Enabled="false" Style="text-align: center;" Width="180px" Text='<%#Eval("RequestID") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="供应商">
                <ItemTemplate>
                    <asp:Label ID="lbl_customer" Text='<%#Eval("payee_unit") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amount_all" Text=' <%#Eval("FPurchaseAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lbl_agenter" Text='<%#Eval("emp_name") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_agent_date" Text='<%#Eval("payment_date","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
    
    </div>
    <div id="grid4" runat="server">
    <span class="title">定金单列表</span>
    <asp:GridView ID="GridView3" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView3_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Eval("receipt_no") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_deposit_id" Style="text-align: center;" Enabled="false" Width="180px" Text='<%#Eval("Deposit_id") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_agent_date" Text='<%#Eval("FDate","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amount_all" Text=' <%#Eval("FPurchaseAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已支付金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_fpayamountfor" Text='<%#Eval("FPayAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="未支付金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_funpayamountfor" Text='<%#Eval("FUnPayAmountFor","{0:F}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="本次支付（*）">
                <ItemTemplate>
                    <asp:TextBox ID="txt_deposit_checkamountfor" Text='<%#Eval("FCheckAmountFor","{0:F}") %>'
                        onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"
                        Style="text-align: center" AutoPostBack="true" OnTextChanged="txt_deposit_checkamountfor_TextChanged"
                        Width="180px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票币别">
                <ItemTemplate>
                    <asp:Label ID="lbl_fcurrencyid" Text='<%#Eval("FCurrencyID") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lbl_agenter" Text='<%#Eval("agenter") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" Text='<%#Eval("FNote") %>' Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
        </div>
    <ul class="queryarea">
        <li><span class="title">制单人</span>
            <span class="control">
                <asp:DropDownList ID="ddl_preparer" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">审核人</span>
            <span class="control">
                <asp:DropDownList ID="ddl_checker" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">审核日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="check_date" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="check_date" ErrorMessage="审核日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li style="width: 100%; margin-top: 10px; margin-bottom: 10px;">
            <asp:Button ID="updateUser" runat="server" Text="修  改" OnClick="updateUser_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
</asp:Content>

