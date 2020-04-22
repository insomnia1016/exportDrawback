<%@ Page Title="销售订单列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="SEOrderList.aspx.cs" Inherits="UI_QueryAndReports_SEOrderList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            DataKeyNames="FBillNo,FDate,FAmount,FCurrencyID,FPayDate,FNote" >
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="cbHead" onclick="javascript:selectAll(this.checked);"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FBillNo" HeaderText="销售订单号" />
                <asp:BoundField DataField="FDate" DataFormatString="{0:d}" HeaderText="单据日期" />                                                                 
                <asp:BoundField DataField="FAmount" DataFormatString="{0:N2}" HeaderText="订单金额" />
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
