<%@ Page Language="C#" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

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
                    <ul class="login">
                        <li><span class="left">用户名：</span> <span style="left">

                            <asp:TextBox ID="username" runat="server" type="text"></asp:TextBox><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </span></li>
                        <li><span class="left">密&nbsp;&nbsp; 码：</span> <span style="left">

                            <asp:TextBox ID="password1" runat="server"   MaxLength="9"  type="text" TextMode="Password"></asp:TextBox><asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </span></li>

                        <li><span class="left">确认密码：</span> <span style="left">

                            <asp:TextBox ID="password2" runat="server" MaxLength="9" type="text" TextMode="Password"></asp:TextBox><asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </span></li>
                        <li><span class="left">姓&nbsp;&nbsp; 名：</span> <span style="left">

                            <asp:TextBox ID="name" runat="server" type="text"></asp:TextBox><asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </span></li>
                    </ul>
                </span></li>
                <li class="topC"></li>
                <li class="topD">
                    <ul class="login">

                        <li><span class="left">部&nbsp;&nbsp; 门：</span> <span style="left">

                            <asp:TextBox ID="derparment" runat="server" type="text"></asp:TextBox>
                        </span></li>
                        

                    </ul>
                </li>
                <li class="topE"></li>
                <li class="middle_A"></li>
                <li class="middle_B">

                    <ul>
                        <li>
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="username" ErrorMessage="请输入用户名"></asp:RequiredFieldValidator>
                            </span>
                        </li>
                        <li>
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="password1" ErrorMessage="请输入密码"></asp:RequiredFieldValidator>
                            </span>
                        </li>
                        <li>
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="password2" ErrorMessage="请输入确认密码"></asp:RequiredFieldValidator>
                            </span>
                        </li>
                        <li>
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="name" ErrorMessage="请输入姓名"></asp:RequiredFieldValidator>
                            </span>
                        </li>
                        <li>
                            <span>
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="password1" ControlToCompare="password2" SetFocusOnError="true" runat="server" ErrorMessage="密码与确认密码不一致"></asp:CompareValidator>
                            </span>
                        </li>
                        
                    </ul>
                </li>
                <li class="middle_C">
                    <ul>
                        <li>
                            <span class="btn">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="images/login/register.png"  OnClick="img_clicked" runat="server" />
                            </span>
                        </li>
                        <li>
                            <span>
                                <asp:Label ID="errorTxt" runat="server" ForeColor ="Red" Text=""></asp:Label>
                            </span>
                        </li>
                    </ul>
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
