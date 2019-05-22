<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebConfigEncrypt.aspx.cs" Inherits="WebConfigEncrypt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>配置文件加密</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnEncrypt" runat="server" Text="加密配置文件" OnClick="btnEncrypt_Click" />
    <asp:Button ID="btnUnencrypt" runat="server" Text="解密配置文件" OnClick="btnUnencrypt_Click" />
    </div>
    </form>
</body>
</html>
