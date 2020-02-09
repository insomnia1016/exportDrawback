<%@ Page Title="部门管理" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="department.aspx.cs" Inherits="UI_QueryAndReports_department" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ul class="queryarea">
        <li><span class="title">代码</span>
            <span class="control">
                <asp:TextBox ID="txt_code" Width="240px" runat="server"></asp:TextBox>
                  <asp:HiddenField ID="HiddenField1" runat="server" />
            </span>
        </li>

        <li><span class="title">名称</span>
            <span class="control">
                <asp:TextBox ID="txt_name"  runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        
    </ul>
    
    <ul class="queryarea">
        <li style="width: 100%;margin-top:20px;margin-bottom:10px;">
            <asp:Button ID="updateUser" runat="server" Text="保   存" OnClick="updateUser_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                    <asp:Button ID="benDelete" runat="server" Text="删除" OnClick="benDelete_Click" />
                    <asp:HiddenField ID="hdfId" runat="server" Value='<%#Eval("id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="code" HeaderText="代码" />
            <asp:BoundField DataField="name" HeaderText="部门" />
        </Columns>
    </asp:GridView>
</asp:Content>

