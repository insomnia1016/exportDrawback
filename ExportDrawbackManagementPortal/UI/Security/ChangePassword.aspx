<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage/ContentPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="UI_Security_ChangePassword" Title="修改密码" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function validate() {
            var oldPswd = document.all('<%=txtOldPin.ClientID %>').value;
            var password1 = document.all('<%=txtNewPin.ClientID %>').value;
            var password2 = document.all('<%=txtValidPin.ClientID %>').value;
            var val6 = document.all('<%=msgLbl.ClientID %>');
            if (oldPswd == "" || password1 == "" || password2 == "") {
                val6.innerHTML = '密码不能为空';
                return false;
            }
            if (password1.length < 6) {
                val6.innerHTML = '对不起，您的新密码不能小于六位';
                return false;
            }
            if (password1 != password2) {
                val6.innerHTML = '新密码和确认新密码不一致';
                return false;
            }
            val6.innerHTML = '';
            return true;

        }
    </script>
    <div style="text-align: center">
        <div style="text-align: right; width: 300px; margin: 100 auto 100 auto;">
            <div>
                旧密码
        <asp:TextBox ID="txtOldPin" runat="server" MaxLength="9" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                新密码
        <asp:TextBox ID="txtNewPin" runat="server" MaxLength="9" onblur="javascript:validate();" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                确认新密码
        <asp:TextBox ID="txtValidPin" runat="server" MaxLength="9" onblur="javascript:validate();" TextMode="Password"></asp:TextBox>
            </div>
            <div style="text-align: center">
                <asp:Button ID="btnSetPin" runat="server" Text="重设密码" OnClientClick="return validate();" OnClick="btnSetPin_Click" />
                <br />
                <asp:Label ID="msgLbl" runat="server" ForeColor="Red" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

