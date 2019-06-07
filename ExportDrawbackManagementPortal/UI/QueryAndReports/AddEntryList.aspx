<%@ Page Title="填制报关单明细" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="AddEntryList.aspx.cs" Inherits="UI_QueryAndReports_AddEntryList" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function cal() {
            var g_qty = document.all('<%=g_qty.ClientID %>').value;
            var decl_price = document.all('<%=decl_price.ClientID %>').value;
            var decl_total = document.all('<%=decl_total.ClientID %>');
            if (isNumber(g_qty) && isNumber(decl_price)) {
                decl_total.value = g_qty * decl_price;
            }
        }

        function isNumber(val) {
            var regPos = /^\d+(\.\d+)?$/; //非负浮点数
            if (regPos.test(val)) {
                return true;
            } else {
                return false;
            }
        }

</script>
    <ul class ="queryarea">
        <li><span class="title">客户</span>
            <span class="control">
                <asp:TextBox ID="owner_name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ControlToValidate="owner_name" ErrorMessage="客户不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">报关日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="d_date" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ControlToValidate="d_date" ErrorMessage="报关日期不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">申报单位</span>
            <span class="control">
                <asp:TextBox ID="agent_name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ControlToValidate="agent_name" ErrorMessage="申报单位不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">报关单号</span>
            <span class="control">
                <asp:TextBox ID="entry_id" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ControlToValidate="entry_id" ErrorMessage="报关单号不能为空"></asp:RequiredFieldValidator>

            </span>
        </li>
        <li><span class="title">报关品名</span>
            <span class="control">
                <asp:TextBox ID="g_name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server" ControlToValidate="g_name" ErrorMessage="报关品名不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">品名项号</span>
            <span class="control">
                <asp:TextBox ID="g_no" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" runat="server" ControlToValidate="g_no" ErrorMessage="品名项号不能为空"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" ControlToValidate="g_no" runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$" ErrorMessage="品名项号只能是正整数"></asp:RegularExpressionValidator>

            </span>
        </li>
        <li><span class="title">数量</span>
            <span class="control">
                <asp:TextBox ID="g_qty" runat="server" OnBlur="cal();"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" runat="server" ControlToValidate="g_qty" ErrorMessage="数量不能为空"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" ControlToValidate="g_qty" runat="server" ValidationExpression="^[0-9]*[1-9][0-9]*$" ErrorMessage="数量只能是正整数"></asp:RegularExpressionValidator>

            </span>
        </li>
        <li><span class="title">计量单位</span>
            <span class="control">
                <asp:TextBox ID="g_unit" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" runat="server" ControlToValidate="g_unit" ErrorMessage="单位不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">申报单价</span>
            <span class="control">
                <asp:TextBox ID="decl_price" runat="server"  OnBlur="javascript:cal();"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" runat="server" ControlToValidate="decl_price" ErrorMessage="申报单价不能为空"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" ControlToValidate="decl_price" runat="server" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="申报单价只能是正数"></asp:RegularExpressionValidator>
            </span>
        </li>
        <li><span class="title">报关金额</span>
            <span class="control">
                <asp:TextBox ID="decl_total" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ControlToValidate="decl_total" ErrorMessage="报关金额不能为空"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4"  ControlToValidate="decl_total" runat="server" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="报关金额只能是正数"></asp:RegularExpressionValidator>
            </span>
        </li>
        <li><span class="title">海关编码</span>
            <span class="control">
                <asp:TextBox ID="code_ts" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" runat="server" ControlToValidate="code_ts" ErrorMessage="海关编码不能为空"></asp:RequiredFieldValidator>
            </span>
        </li>
        <li><span class="title">出口退税率</span>
            <span class="control">
                <asp:TextBox ID="drawback_rate" runat="server" Text="请输入小数" ForeColor="GrayText" OnFocus="javascript:if(this.value=='请输入小数') {this.value=''}"
                                OnBlur="javascript:if(this.value==''){this.value='请输入小数'}"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" SetFocusOnError="true" ControlToValidate="drawback_rate" runat="server" ValidationExpression="^\d+(\.\d+)?$" ErrorMessage="出口退税率只能是正数"></asp:RegularExpressionValidator>

            </span>
        </li>
    </ul>
    <ul class ="queryarea">
        <li style="width:100%">
            <asp:Button ID="add" runat="server" Text="录  入" OnClick="add_Click" style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width:100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>

</asp:Content>

