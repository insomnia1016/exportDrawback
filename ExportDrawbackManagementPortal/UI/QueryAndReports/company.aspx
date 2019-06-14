<%@ Page Title="供应商资料管理" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="company.aspx.cs" Inherits="UI_QueryAndReports_company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="queryarea">
        <li><span class="title">公司</span>
            <span class="control">
                <asp:TextBox ID="txtCompany" Width="240px" runat="server"></asp:TextBox>
                  <asp:HiddenField ID="HiddenField1" runat="server" />
            </span>
        </li>

        <li><span class="title">地址</span>
            <span class="control">
                <asp:TextBox ID="txtAddress"  runat="server" Width="240px"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">电话</span>
            <span class="control">
                <asp:TextBox ID="txtTel"  onKeyPress="if (event.keyCode!=45 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" runat="server"></asp:TextBox>
            </span>
        </li>

        <li><span class="title">经办人</span>
            <span class="control">
                <asp:TextBox ID="txtJingban" runat="server"></asp:TextBox>
            </span>
        </li>
         <li><span class="title">法定代表人</span>
            <span class="control">
                <asp:TextBox ID="txtFadingdaibiaoren" runat="server"></asp:TextBox>
            </span>
        </li>
         <li><span class="title">代理人</span>
            <span class="control">
                <asp:TextBox ID="txtDailiren" runat="server"></asp:TextBox>
            </span>
        </li>
    </ul>
    
    <ul class="queryarea">
        <li style="width: 100%;margin-top:20px;margin-bottom:10px;">
            <asp:Button ID="updateUser" runat="server" Text="保   存" OnClick="updateUser_Click" Style="margin-left: 40%;" Width="100px" />
            <asp:Button ID="clean" runat="server" Text="新   增" OnClick="clean_Click"  Width="100px" />
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
            <asp:BoundField DataField="company_name" HeaderText="公司" />
            <asp:BoundField DataField="address" HeaderText="地址" />
            <asp:BoundField DataField="tel" HeaderText="电话" />
            <asp:BoundField DataField="jingban" HeaderText="经办" />
            <asp:BoundField DataField="fadingdaibiaoren" HeaderText="法定代表人" />
            <asp:BoundField DataField="dailiren" HeaderText="代理人" />
        </Columns>
    </asp:GridView>

</asp:Content>

