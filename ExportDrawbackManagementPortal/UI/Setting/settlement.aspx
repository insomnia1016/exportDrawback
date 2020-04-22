<%@ Page Title="美金结汇" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="settlement.aspx.cs" Inherits="UI_Setting_settlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2 style="width: 100%">转出账户</h2>
    <ul class="queryarea">
        <li><span class="title">开户行</span>
            <span class="control">
                 <asp:DropDownList ID="ddl_opening_bank" AutoPostBack="true" OnSelectedIndexChanged="ddl_opening_bank_SelectedIndexChanged" Width="170px" runat="server">
                </asp:DropDownList>
            </span>
        </li>
        <li><span class="title">账号</span>
            <span class="control">
                <asp:TextBox ID="txt_account_id" Enabled="false" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">户名</span>
            <span class="control">
                <asp:TextBox ID="txt_account_name" Enabled="false" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="ddl_currency" AutoPostBack="true" OnSelectedIndexChanged="ddl_currency_SelectedIndexChanged" Width="170px" runat="server">
                </asp:DropDownList>
            </span>
        </li>
        <li><span class="title">账户金额</span>
            <span class="control">
                <asp:TextBox ID="txt_amount" Enabled="false"  runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">转出金额</span>
            <span class="control">
                <asp:TextBox ID="TextBox5"  AutoPostBack="true" OnTextChanged="TextBox5_TextChanged"
                    onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" runat="server" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox5" runat="server" ErrorMessage="转出金额不能为空"></asp:RequiredFieldValidator></span>
        </li>
    </ul>
    <h2 style="width: 100%; clear: both">转入账户</h2>
    <ul class="queryarea">
        <li><span class="title">开户行</span>
            <span class="control">
                 <asp:DropDownList ID="DropDownList2" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="170px" runat="server">
                </asp:DropDownList>

            </span>
        </li>
        <li><span class="title">账号</span>
            <span class="control">
                <asp:TextBox ID="TextBox2" Enabled="false" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">户名</span>
            <span class="control">
                <asp:TextBox ID="TextBox3" Enabled="false" runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">币制</span>
            <span class="control">
                <asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="170px" runat="server">
                </asp:DropDownList>
            </span>
        </li>
        <li><span class="title">账户金额</span>
            <span class="control">
                <asp:TextBox ID="TextBox4" Enabled="false"  runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">转入金额</span>
            <span class="control">
                <asp:TextBox ID="TextBox6"  
                    onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"  runat="server" Width="240px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TextBox6" runat="server" ErrorMessage="转入金额不能为空"></asp:RequiredFieldValidator></span>
        </li>
    </ul>
    <ul class="queryarea">
        <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
            <span class="title">汇率</span>
            <asp:TextBox ID="txt_exchange_rate" AutoPostBack="true" OnTextChanged="txt_exchange_rate_TextChanged"
                onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" runat="server" Width="240px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_exchange_rate" runat="server" ErrorMessage="汇率不能为空"></asp:RequiredFieldValidator></li>
        <li style="width: 100%; margin-top: 20px; margin-bottom: 10px;">
            <asp:Button ID="save" runat="server" Text="结   汇" OnClick="save_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        OnRowDataBound="GridView1_RowDataBound">
        <Columns>
            <asp:BoundField DataField="account_name" HeaderText="户名" />
            <asp:BoundField DataField="opening_bank" HeaderText="开户行" />
            <asp:BoundField DataField="account_id" HeaderText="账号" />
            <asp:BoundField DataField="currencyID" HeaderText="币制" />
            <asp:BoundField DataField="amount" DataFormatString="{0:N}" HeaderText="额度" />
        </Columns>
    </asp:GridView>
</asp:Content>

