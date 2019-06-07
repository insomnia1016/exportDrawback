<%@ Page Title="用户授权" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="authorize.aspx.cs" Inherits="UI_Security_authorize" %>

<%@ Register Assembly="DevControl" Namespace="DevControl" TagPrefix="Dev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

            <ul class="queryarea">
                <li><span class="title">姓名</span>
                    <span class="control">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </span>
                </li>

                <li><span class="title">用户名</span>
                    <span class="control">
                        <asp:TextBox ID="txtUsername" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </span>
                </li>
                <li><span class="title">部门</span>
                    <span class="control">
                        <asp:TextBox ID="txtDerparment" runat="server"></asp:TextBox>

                    </span>
                </li>

                <li><span class="title">密码</span>
                    <span class="control">
                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    </span>
                </li>

            </ul>
            <ul class="queryarea">
                <li style="width: 100%"><span class="title">权限</span>
                    <span class="control">
                        <Dev:DropDownCheckBoxList ID="DropDownCheckBoxList1" runat="server" ShowSelectAllOption="true" Width="640px">
                        </Dev:DropDownCheckBoxList>
                    </span>
                </li>
            </ul>
            <ul class="queryarea">
                <li style="width: 100%">
                    <asp:Button ID="updateUser" runat="server" Text="修  改" OnClick="updateUser_Click" Style="margin-left: 40%;" Width="100px" />
                </li>
                <li style="width: 100%">
                    <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                </li>
            </ul>

    <asp:GridView ID="GridView1" runat="server" CellPadding="4"  ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                    <asp:Button ID="benDelete" runat="server" Text="删除" OnClick="benDelete_Click" />
                    <asp:HiddenField ID="hdfId" runat="server" Value='<%#Eval("person_id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="name" HeaderText="姓名" />
            <asp:BoundField DataField="username" HeaderText="用户名" />
            <asp:BoundField DataField="roles" HeaderText="权限" />
            <asp:BoundField DataField="derpartment" HeaderText="部门" />
            <asp:BoundField DataField="password" HeaderText="密码" />
            <asp:BoundField DataField="rank" HeaderText="角色" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <EmptyDataTemplate>
            <asp:CheckBox ID="CheckBox1" runat="server" />
        </EmptyDataTemplate>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>


</asp:Content>

