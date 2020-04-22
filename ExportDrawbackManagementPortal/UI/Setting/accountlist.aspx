<%@ Page Title="银行账户列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="accountlist.aspx.cs" Inherits="UI_Setting_accountlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>录入账户表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">开户行</span>
            <span class="control">
                <asp:TextBox ID="txt_opening_bank" Width="240px" runat="server"></asp:TextBox>

            </span>
        </li>
        <li><span class="title">账号</span>
            <span class="control">
                <asp:TextBox ID="txt_account_id" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false" runat="server" Width="240px"></asp:TextBox>
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </span>
        </li>
        <li><span class="title">户名</span>
            <span class="control">
                <asp:TextBox ID="txt_account_name" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
         <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency"  Width="170px" runat="server">
                    </asp:DropDownList>
            </span>
        </li>
        <li><span class="title">金额</span>
            <span class="control">
                <asp:TextBox ID="txt_amount" onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
    </ul>
    
    <ul class="queryarea">
        <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
            <asp:Button ID="save" runat="server" Text="保   存" OnClick="save_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
        OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
        OnRowDataBound="GridView1_RowDataBound"
        OnRowCommand="GridView1_RowCommand"
        OnRowDeleting="GridView1_RowDeleting"
        OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("account_id")  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="account_name" HeaderText="户名" />
            <asp:BoundField DataField="opening_bank" HeaderText="开户行" />
            <asp:BoundField DataField="account_id" HeaderText="账号" />
            <asp:BoundField DataField="currencyID" HeaderText="币制" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N}" HeaderText="额度" />
        </Columns>
    </asp:GridView>
</asp:Content>

