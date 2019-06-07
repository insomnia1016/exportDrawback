<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>出口退税管理系统</title>
    <link href="style/alogin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Main">
            <ul>
                <li class="top"></li>
                <li class="top2"></li>
                <li class="topA"></li>
                <li class="topB"><span>
                    <img src="images/login/logo.png" alt="" style="" />
                </span></li>
                <li class="topC"></li>
                <li class="topD">
                    <ul class="login">
                        <li><span class="left">用户名：</span> <span style="left">

                            <asp:TextBox ID="username" runat="server" type="text"></asp:TextBox>
                        </span></li>
                        <li><span class="left">密&nbsp;&nbsp; 码：</span> <span style="left">

                            <asp:TextBox ID="password" runat="server" type="text" TextMode="Password"></asp:TextBox>
                        </span></li>

                        <li>
                            <asp:Label ID="warning" runat="server" ForeColor="Red" Text=""></asp:Label>
                        </li>
                        <li>
                            <span class="left">记住我：</span>
                            <asp:CheckBox ID="Checkbox1" runat="server" type="checkbox" />
                        </li>

                    </ul>
                </li>
                <li class="topE"></li>
                <li class="middle_A"></li>
                <li class="middle_B"></li>
                <li class="middle_C">
                    <span class="loginBtn">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/login/login.png" OnClick="img_clicked" runat="server" />
                    </span>
                    <span class="loginBtn">
                        <asp:ImageButton ID="ImageButton2" ImageUrl="images/login/register.png" OnClick="img2_clicked" runat="server" />
                    </span>
                </li>
                <li class="middle_D"></li>
                <li class="bottom_A"></li>
                <li class="bottom_B"></li>
                <li class="bottom_B"></li>
            </ul>
        </div>
    </form>
</body>
</html>
