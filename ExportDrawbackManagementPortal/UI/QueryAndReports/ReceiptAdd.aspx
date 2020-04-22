<%@ Page Title="收款登记申请" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ReceiptAdd.aspx.cs" Inherits="UI_QueryAndReports_ReceiptAdd" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Reference Page="~/UI/QueryAndReports/ReceiptList.aspx" %>
<%@ Reference Page="~/UI/QueryAndReports/InDecreaseList.aspx" %>
<%@ Reference Page="~/UI/QueryAndReports/DepositList.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function PopupReceiptWindow() {
            var customer = document.all('<%=txt_customer.ClientID %>').value;
            var id = document.all('<%=ddl_currency.ClientID %>').value;
            window.open("receiptList.aspx?custName=" + customer + "&id=" + id, "", "status=no,resizable=no,toolbar=no,menubar=no,location=no,scroll=no");
        }
    </script>
    <script type="text/javascript">
        function PopupIndecWindow() {
            var customer = document.all('<%=txt_customer.ClientID %>').value;
            var id = document.all('<%=ddl_currency.ClientID %>').value;

            window.open("InDecreaseList.aspx?custName=" + customer + "&id=" + id, "", "width=680,height=520,status=no,resizable=no,toolbar=no,menubar=no,location=no,scroll=no");
        }
    </script>
    <script type="text/javascript">
        function PopupDeposWindow() {
            var customer = document.all('<%=txt_customer.ClientID %>').value;
            var id = document.all('<%=ddl_currency.ClientID %>').value;
            var type = document.all('<%=ddl_receipt_type.ClientID %>').value;
            window.open("DepositList.aspx?custName=" + customer + "&id=" + id + "&type=" + type, "", "status=no,resizable=no,toolbar=no,menubar=no,location=no,scroll=no");
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <h2 style="text-align: center;">收款单据</h2>
    <ul class="queryarea">


        <li><span class="title">客户</span>
            <span class="control">
                <asp:TextBox ID="txt_customer" Width="380px" runat="server"></asp:TextBox>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" Enabled="true" TargetControlID="txt_customer" ServicePath="~/webservices/AutoCompleteService.asmx" ServiceMethod="SelectSearchInfo" CompletionSetCount="10" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>

            </span>
        </li>
        <li><span class="title">日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="d_date" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ControlToValidate="d_date" ErrorMessage="日期不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">单据号</span>
            <span class="control">
                <asp:TextBox ID="txt_recetpt_no" Enabled="false" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">收款类型</span>
            <span class="control">
                <asp:DropDownList ID="ddl_receipt_type" AutoPostBack="true" OnSelectedIndexChanged="ddl_receipt_type_SelectedIndexChanged" Width="180px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency" AutoPostBack="true" OnSelectedIndexChanged="ddl_currency_SelectedIndexChanged" Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">表头收款金额</span>
            <span class="control">
                <asp:TextBox ID="txt_amount" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li style="clear: both"><span class="title">手续费</span>
            <span class="control">
                <asp:TextBox ID="txt_receipt_charge" AutoPostBack="true" OnTextChanged="txt_receipt_charge_TextChanged" Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">收款账户</span>
            <span class="control">
                <asp:DropDownList ID="ddl_account" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">备注</span>
            <span class="control">
                <asp:TextBox ID="txt_note"  Width="180px" runat="server"></asp:TextBox>
            </span>
        </li>
    </ul>
     <div runat="server" id="grid1">
    <span class="title" style="clear: both">销售发票列表</span>
    <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                    <asp:HiddenField ID="hdf_FInterID" Value='<%#Eval("FInterID") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="源单编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Style="text-align: center;" onmousedown="PopupReceiptWindow();" AutoCompleteType="Disabled" Width="180px" Text='<%#Eval("FBillNo") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_fdate" Text='<%#Eval("FDate","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amountfor" Text=' <%#Eval("FAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已核销金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_freceiveamountfor" Text='<%#Eval("FReceiveAmountFor","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="未核销金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_funreceiveamountfor" Text='<%#Eval("FUnReceiveAmountFor","{0:F}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="本次核销（*）">
                <ItemTemplate>
                    <asp:TextBox ID="txt_fcheckamountfor"
                        onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"
                        Style="text-align: center" AutoPostBack="true" OnTextChanged="txt_fcheckamountfor_TextChanged"
                        Width="180px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票币别">
                <ItemTemplate>
                    <asp:Label ID="lbl_fcurrencyid" Text='<%#Eval("FCurrencyID") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" Text='<%#Eval("FNote") %>' Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center" ForeColor="MediumBlue" />
    </asp:GridView>

    <span class="title">增减单列表</span>
    <asp:GridView ID="GridView2" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView2_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Style="text-align: center;" onmousedown="PopupIndecWindow();" AutoCompleteType="Disabled" Width="180px" Text='<%#Eval("bill_no") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户">
                <ItemTemplate>
                    <asp:Label ID="lbl_customer" Text='<%#Eval("customer") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amount_all" Text=' <%#Eval("amount_all","{0:F}") %>' runat="server"></asp:Label>
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
    <div runat="server" id="grid2">
    <span class="title">定金单列表</span>
    <asp:GridView ID="GridView3" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView3_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据编号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_deposit_id" Style="text-align: center;" onmousedown="PopupDeposWindow();" AutoCompleteType="Disabled" Width="180px" Text='<%#Eval("deposit_id") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_agent_date" Text='<%#Eval("agent_date","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amount_all" Text=' <%#Eval("amount_all","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已核销金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_freceiveamountfor" Text='<%#Eval("receive_amount_for","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="未核销金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_funreceiveamountfor" Text='<%#Eval("unreceive_amount_for","{0:F}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="本次核销（*）">
                <ItemTemplate>
                    <asp:TextBox ID="txt_deposit_checkamountfor"
                        onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"
                        Style="text-align: center" AutoPostBack="true" OnTextChanged="txt_deposit_checkamountfor_TextChanged"
                        Width="180px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发票币别">
                <ItemTemplate>
                    <asp:Label ID="lbl_fcurrencyid" Text='<%#Eval("currencyID") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lbl_agenter" Text='<%#Eval("agenter") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note" Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
          </div>
    <ul class="queryarea">
        <li><span class="title">部门</span>
            <span class="control">
                <asp:DropDownList ID="ddl_department" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">业务员</span>
            <span class="control">
                <asp:DropDownList ID="ddl_emp" runat="server"></asp:DropDownList>
            </span>
        </li>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="check_date" ErrorMessage="审核人日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li style="width: 100%; margin-top: 10px; margin-bottom: 10px;">
            <asp:Button ID="Button1" Visible="false" runat="server" Text="保   存" Style="margin-left: 35%;" Width="100px" />
            <asp:Button ID="updateUser" runat="server" Text="提交审核" OnClick="updateUser_Click" Style="margin-left: 40%;" Width="100px" />
            <%--<asp:Button ID="btnAudit" runat="server" Text="提交审核" OnClick="btnAudit_Click" Style="margin-left: 5%;" Width="100px" />--%>

        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
</asp:Content>

