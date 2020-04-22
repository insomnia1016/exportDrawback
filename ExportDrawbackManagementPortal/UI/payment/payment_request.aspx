<%@ Page Title="付款申请" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="payment_request.aspx.cs" Inherits="UI_payment_payment_request" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .grid {
            width: 12.5%;
        }
        .center {
            text-align: center;
            font-size: 16px;
        }
        span {
            font-size: 16px;
        }

        div {
            font-size: 18px;
        }
        td {
            font-size: 16px;
        }
        .TextBox {
            font-size: 16px;
        }
    </style>
    <style media="print" type="text/css">
        .noprint {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function printme() {
            window.print();
        }
    </script>
    <div class="noprint" style="padding-right: 30px; float: right;">
        <asp:Button ID="print" runat="server" OnClientClick="printme();" Text="打  印" />
    </div>

    <h3 align="center">付款通知书</h3>
    <ul>
        <span class="title" style="margin-left: 680px;">填报日期：</span>
        <span class="control">
            <ExportDrawbackManagement:CalendarBox ID="d_date" CssClass="center" runat="server" FormatString="yyyy年MM月dd日" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>
            <asp:HiddenField ID="hdf_payment_id" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="d_date" ErrorMessage="填报日期不能为空"></asp:RequiredFieldValidator>
        </span>
    </ul>
    <table border="1" height="500" width="1000" cellspacing="0" align="center">
        <tr align="center">
            <td>收款单位</td>
            <td colspan="7" style="text-align: left; padding-left: 20px;">
                <asp:TextBox ID="txt_payee_unit" Width="300" runat="server"></asp:TextBox>
            </td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_payee_unit" runat="server" ErrorMessage="收款单位不能为空"></asp:RequiredFieldValidator>
        </tr>
        <tr align="center">
            <td>金额</td>
            <td colspan="7" style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lbl_amount" runat="server"></asp:Label>
                <asp:TextBox ID="txt_amount" AutoPostBack="true" OnTextChanged="txt_amount_TextChanged" CssClass="center"
                    onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_amount" runat="server" ErrorMessage="金额不能为空"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr align="center">
            <td>收款开户行</td>
            <td colspan="3" style="text-align: left; padding-left: 20px;">
                <asp:TextBox ID="txt_opening_bank" Width="300" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txt_opening_bank" runat="server" ErrorMessage="收款开户行不能为空"></asp:RequiredFieldValidator>
            </td>
            <td>账号</td>
            <td colspan="3" style="text-align: left; padding-left: 20px;">
                <asp:TextBox ID="txt_account_id"
                    onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"
                    Width="300" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txt_account_id" runat="server" ErrorMessage="账号不能为空"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr align="center">
            <td rowspan="3">付款说明</td>
             <td>销售发票号</td>
            <td colspan="2">
                <asp:TextBox ID="txt_poorder_id" runat="server"></asp:TextBox>
            </td>
            <td rowspan="3" colspan="4" style="padding-left: 20px; padding-right: 20px;">
                <asp:TextBox ID="txt_payment_explain" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>销售订单号</td>
            <td colspan="2">
                <asp:TextBox ID="txt_icsale_id" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>产品货号</td>
            <td colspan="2">
                <asp:TextBox ID="txt_goods_model" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr align="center">
            <td>备注</td>
            <td colspan="7" style="text-align: left; padding-left: 20px; padding-right: 20px;">
                <asp:TextBox ID="txt_note" Width="100%" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr align="center">
            <td class="grid">领导审批</td>
            <td class="grid"></td>
            <td class="grid">财务审批</td>
            <td class="grid"></td>
            <td class="grid">费用所属部门</td>
            <td class="grid" style="padding-left: 20px; padding-right: 20px;">
                <asp:TextBox ID="txt_dept_name" CssClass="center" Width="100" runat="server"></asp:TextBox>
            </td>
            <td class="grid">申请人</td>
            <td class="grid" style="padding-left: 20px; padding-right: 20px;">
                <asp:TextBox ID="txt_emp_name" Width="100" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />

    <ul class="noprint" style="text-align: center;">
        <asp:Button ID="btn_save" runat="server" Text="保  存" Style="margin-left: 5%;" OnClick="btn_save_Click" Width="100px" />
        <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
    </ul>
    <br />
    <br />
</asp:Content>

