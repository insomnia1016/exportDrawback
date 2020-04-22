<%@ Page Title="个人增减单列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="InDecreaseList.aspx.cs" Inherits="UI_QueryAndReports_InDecreaseList" %>

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
            DataKeyNames="bill_no,customer,amount_all,agenter,agent_date,check_status" >
            <Columns>
                <asp:TemplateField HeaderText="操作">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="cbHead" onclick="javascript:selectAll(this.checked);"></asp:CheckBox>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox runat="server" ID="cbItem"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="bill_no" HeaderText="单据编号" />
                <asp:BoundField DataField="customer" HeaderText="客户" />                                                                 
                <asp:BoundField DataField="amount_all" DataFormatString="{0:N2}" HeaderText="单据金额" />
                <asp:BoundField DataField="agenter"  HeaderText="申请人" />
                <asp:BoundField DataField="agent_date" DataFormatString="{0:d}" HeaderText="申请日期" />
                <asp:BoundField DataField="check_status"  HeaderText="状态" />
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

