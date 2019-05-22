<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage/ContentPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="UI_Security_ChangePassword" Title="修改密码" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
function SetPin()
{
        if(!Page_ClientValidate(''))
            return false;
        if(!confirm('是否确认要修改密码？'))
            return false;          
        var ca = null;
        
        var oldPin = document.all('<%=txtOldPin.ClientID %>').value;
        var newPin = document.all('<%=txtNewPin.ClientID %>').value;
        if(isTestLogin)
        {
            alert('密码已修改。');
            window.location.href = "../Common/WelcomePage.aspx";
            return true;
        }
        
        try
        {
            ca = new ActiveXObject("SecSnapIn.Authentication");
            var result = ca.ChangePin(oldPin,newPin);
            if(result != '')
            {
                alert(result);
               return false; 
            }
            else
            {
                alert('密码已修改。');
                window.location.href = "../Common/WelcomePage.aspx";
            }
        }
        catch(e)
        {
            alert(e.description);
            return false;
        }            
        return true;
}
</script>
<div style="text-align:center">
   <div style="text-align:right;width:240px;margin:100 auto 100 auto;">
       <div>旧密码
        <asp:TextBox ID="txtOldPin" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入旧密码" Display="None" SetFocusOnError="true" ControlToValidate="txtOldPin"/>
       </div>
       <div>新密码
        <asp:TextBox ID="txtNewPin" runat="server" MaxLength="8" TextMode="Password" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入新密码" Display="None" SetFocusOnError="true" ControlToValidate="txtOldPin"/>
       </div>
       <div>确认新密码
        <asp:TextBox ID="txtValidPin" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
           <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPin"
               ControlToValidate="txtValidPin" Display="None" ErrorMessage="确认新密码和新密码不一致"
               SetFocusOnError="True">*</asp:CompareValidator>
           
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入确认新密码" Display="None" SetFocusOnError="true" ControlToValidate="txtOldPin"/>
      </div>
      <div><asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
               ShowSummary="False" />
          <asp:Button ID="btnSetPin" runat="server" Text="重设密码" OnClientClick="return SetPin();" UseSubmitBehavior="False" />
      </div>
  </div>
  </div>
</asp:Content>

