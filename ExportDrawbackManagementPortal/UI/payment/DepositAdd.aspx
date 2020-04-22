<%@ Page Title="定金支付" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="DepositAdd.aspx.cs" Inherits="UI_payment_DepositAdd" %>
<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Reference Page="~/UI/payment/POOrderList.aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function PopupDepositWindow() {
            var customer = document.all('<%=customer.ClientID %>').value;
            var id = document.all('<%=ddl_currency.ClientID %>').value;
            window.open("POOrderList.aspx?custName=" + customer + "&id=" + id, "", "status=no,resizable=no,toolbar=no,menubar=no,location=no,scroll=no");
        }
    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <h2>录入定金表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">供应商：</span>
            <span class="control">
                <asp:TextBox ID="customer" Width="380px" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="customer" ErrorMessage="客户不能为空"></asp:RequiredFieldValidator>
                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" Enabled="true" TargetControlID="customer" ServicePath="~/webservices/Suppliery.asmx" ServiceMethod="SelectSearchInfo" CompletionSetCount="10" MinimumPrefixLength="1">
                </cc1:AutoCompleteExtender>
            </span>
        </li>
         <li><span class="title">单据编码：</span>
            <span class="control">
                <asp:TextBox ID="txt_deposit_id" Width="180px" Enabled="false" runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="txt_deposit_id" ErrorMessage="单据编码不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
         <li><span class="title">定金金额：</span>
            <span class="control">
                <asp:TextBox ID="txt_amount_all" Width="180px"  runat="server" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txt_amount_all" ErrorMessage="定金金额不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
         <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency"  Width="170px" runat="server"></asp:DropDownList>
            </span>
        </li>
        <li><span class="title">日期：</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="agent_date" style="text-align:center;"  runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ControlToValidate="agent_date" ErrorMessage="日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">申请人：</span>
            <span class="control">
                <asp:DropDownList ID="ddl_agenter" runat="server"></asp:DropDownList>
            </span>
        </li>
    </ul>
    
    <h2 style="float:left;text-align:left;width:100%;">录入定金表体数据</h2>
        <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False" ShowFooter="True"
        OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="行号" InsertVisible="False">
                <ItemTemplate>
                    <asp:Label ID="lbl_gno" runat="server"  Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售订单号">
                <ItemTemplate>
                    <asp:TextBox ID="txt_bill_no" Style="text-align: center;" onmousedown="PopupDepositWindow();" AutoCompleteType="Disabled" Width="180px" Text='<%#Eval("FBillNo") %>' runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据日期">
                <ItemTemplate>
                    <asp:Label ID="lbl_fdate" Text='<%#Eval("FDate","{0:yyyy-MM-dd}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单据金额">
                <ItemTemplate>
                    <asp:Label ID="lbl_amountfor" Text=' <%#Eval("FAmount","{0:F}") %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txt_note"  Style="text-align: center;" Width="280px" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center"
            ForeColor="MediumBlue" />
    </asp:GridView>
    
    <ul class="queryarea">
        <li style="width: 100%">
            <asp:Button ID="Button1" Visible="false" runat="server" Text="保   存" Style="margin-left: 35%;" Width="100px" />
            <asp:Button ID="add" runat="server" Text="录  入" OnClick="add_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
</asp:Content>

