<%@ Page Title="个人未付款销售列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="receiptList.aspx.cs" Inherits="UI_payment_receiptList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        {
            function selectAll(bool) {
                var ctl = document.getElementById('<%=GridView1.ClientID %>');
                var checkbox = ctl.getElementsByTagName('input');
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].type == 'checkbox') {
                        checkbox[i].checked = bool;
                    }
                }
            }
            }
    </script>
    <ul class="queryarea">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True"
              PageSize="20" OnRowDataBound="GridView1_RowDataBound" 
            OnDataBinding="GridView1_DataBinding" 
            OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False"
            DataKeyNames="FInterID,FBillNo,FDate,FPurchaseAmountFor,FPayAmountFor,FUnPayAmountFor,FCurrencyID,FNote" >
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="cbHead" onclick="javascript:selectAll(this.checked);"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                        <asp:HiddenField ID="hdfFInterID" runat="server" Value='<%#Eval("FInterID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FBillNo" HeaderText="采购发票号" />
                <asp:BoundField DataField="FDate" DataFormatString="{0:d}" HeaderText="单据日期" />                                                                 
                <asp:BoundField DataField="FPurchaseAmountFor" DataFormatString="{0:N2}" HeaderText="订单金额" />
                <asp:BoundField DataField="FPayAmountFor" DataFormatString="{0:N2}" HeaderText="已支付金额" />
                <asp:BoundField DataField="FUnPayAmountFor" DataFormatString="{0:N2}" HeaderText="未支付金额" />
                <asp:BoundField DataField="FCurrencyID" HeaderText="发票币别" />
                <asp:BoundField DataField="FPayDate" DataFormatString="{0:d}" HeaderText="收款期限" />
                <asp:BoundField DataField="FNote" HeaderText="备注" />
            </Columns>
        </asp:GridView>
        <li style="width: 100%">
            <asp:Button ID="btn_import" runat="server"  Text="导    入"  OnClick="btn_import_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
</asp:Content>

