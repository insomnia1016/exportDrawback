<%@ Page Title="付款申请单详情" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="payment_details.aspx.cs" Inherits="UI_payment_payment_details" %>

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
            <asp:Label ID="lbl_d_date" runat="server"></asp:Label>

        </span>
    </ul>
    <table border="1" height="500" width="1000" cellspacing="0" align="center">
        <tr align="center">
            <td>收款单位</td>
            <td colspan="7" style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lbl_payee_unit" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td>金额</td>
            <td colspan="7" style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lbl_amount" runat="server"></asp:Label>
                <asp:Label ID="lbl_amount_num" runat="server"></asp:Label>

            </td>
        </tr>
        <tr align="center">
            <td>收款开户行</td>
            <td colspan="3" style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lbl_opening_bank" runat="server"></asp:Label>
            </td>
            <td>账号</td>
            <td colspan="3" style="text-align: left; padding-left: 20px;">
                <asp:Label ID="lbl_account_id" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td rowspan="3">付款说明</td>
            <td>销售发票号</td>
            <td colspan="2">
                <asp:Label ID="lbl_poorder_id" runat="server"></asp:Label>
            </td>
            <td rowspan="3" colspan="4" style="padding-left: 20px; padding-right: 20px;">
                <asp:Label ID="lbl_payment_explain" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td>销售订单号</td>
            <td colspan="2">
                <asp:Label ID="lbl_icsale_id" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td>产品货号</td>
            <td colspan="2">
                <asp:Label ID="lbl_goods_model" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td>备注</td>
            <td colspan="7" style="text-align: left; padding-left: 20px; padding-right: 20px;">
                <asp:Label ID="lbl_note" runat="server"></asp:Label>
            </td>
        </tr>

        <tr align="center">
            <td class="grid">领导审批</td>
            <td class="grid"></td>
            <td class="grid">财务审批</td>
            <td class="grid"></td>
            <td class="grid">费用所属部门</td>
            <td class="grid" style="padding-left: 20px; padding-right: 20px;">
                <asp:Label ID="lbl_dept_name" runat="server"></asp:Label>
            </td>
            <td class="grid">申请人</td>
            <td class="grid" style="padding-left: 20px; padding-right: 20px;">
                <asp:Label ID="lbl_emp_name" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
</asp:Content>


