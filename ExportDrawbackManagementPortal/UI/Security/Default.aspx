<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="UI_Security_Default" Title="出口退税管理系统" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <script type="text/javascript">
      var leftWidth = 160;
      function HideLeft(level) {
          left.style.display = 'none';
          divHideLeft.style.display = 'none';
          divShowLeft.style.display = '';
          right.style.marginLeft = 8;

      }
      function ShowLeft() {
          left.style.display = '';
          divHideLeft.style.display = '';
          divShowLeft.style.display = 'none';
          left.style.width = leftWidth;
          right.style.marginLeft = 180;
      }
    </script>
  
  <div id="center" style="margin: 0 auto; overflow: hidden; width: 100%; background-color: #d3dde7;">
    <div id="left" style="width: 160px; float: left; height: 670px; text-align: left;
            background-image: url(../../Images/main/bg.JPG); padding: 3px; border-right: solid 1px #014e92">
      <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
        
      </table>
      <table width="100%" style="height: 5px" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td style="background: url(../../Images/menu/pnl_left.jpg); width: 6px;"></td>
          <td style="background-color: #E0DFE4;"><div id="divMenu" style="text-align: left; background-color: Transparent;" runat="server">
              <asp:Menu ID="menuFuns" CssClass="left_menu" Width="100%" Font-Names="Verdana" Font-Size="0.8em"
                                ForeColor="black" StaticSubMenuIndent="10px" runat="server" DynamicHorizontalOffset="-20"
                                DynamicVerticalOffset="10" DisappearAfter="2000" 
                  onmenuitemclick="menuFuns_MenuItemClick">
                <StaticMenuItemStyle HorizontalPadding="0px" CssClass="left_menu_a" Height="29px" Font-Bold="true" />
                <StaticSelectedStyle BackColor="#eee" />
                <StaticHoverStyle CssClass="left_menu_on" />
                <DynamicHoverStyle />
                <LevelMenuItemStyles>
                  <asp:MenuItemStyle Font-Underline="False" Font-Size="14px"/>
                  <asp:MenuItemStyle CssClass="left_menu_2" Font-Underline="False" BorderColor="#c9e2de"
                                        BorderStyle="Solid" ItemSpacing="1px" BorderWidth="1px" Font-Size="14px" HorizontalPadding="8px"
                                        Height="22px" />
                  <asp:MenuItemStyle CssClass="left_menu_2" Font-Underline="False" BorderColor="#c9e2de"
                                        BorderStyle="Solid" ItemSpacing="1px" BorderWidth="1px" Font-Size="14px" HorizontalPadding="8px"
                                        Height="22px" />
                  <asp:MenuItemStyle CssClass="left_menu_2" Font-Underline="False" BorderColor="#c9e2de"
                                        BorderStyle="Solid" ItemSpacing="1px" BorderWidth="1px" Font-Size="14px" HorizontalPadding="8px"
                                        Height="22px" />
                </LevelMenuItemStyles>
                <DynamicSelectedStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                <DynamicMenuStyle HorizontalPadding="1px" />
                <DynamicMenuItemStyle HorizontalPadding="1px" />
              </asp:Menu>
            </div></td>
          <td style="background-image: url(../../Images/menu/pnl_right.jpg); width: 10px;"></td>
        </tr>
      </table>
      
      <table width="100%" style="" border="0" cellpadding="0" cellspacing="0">
        <tr style="background-repeat: repeat-x;">
          <td style="background-image: url(../../Images/menu/down_left.gif); width: 22px; height: 19px;"></td>
          <td style="background-image: url(../../Images/menu/down_center_left.jpg); font-size: 1px;
                        height: 19px;">&nbsp;</td>
          <td style="background-image: url(../../Images/menu/down_center.jpg); width: 126px;
                        height: 19px;"></td>
          <td style="background-image: url(../../Images/menu/down_center_right.jpg); width: 1px;
                        height: 19px;"></td>
          <td style="background-image: url(../../Images/menu/down_right.gif); width: 12px;
                        height: 19px;"></td>
        </tr>
      </table>

    </div>
    <div id="divHidePnl" style="top: 250; left: 200; z-index: 10; text-align: left; cursor: hand;
            float: left">
      <div id="divHideLeft" title="隐藏菜单栏" onClick="HideLeft()" style="background-image: url(../../Images/bg/button_right.gif);
                width: 8px; height: 500px; float: left; background-repeat: no-repeat; background-position: center"> </div>
      <div id="divShowLeft" title="显示菜单栏" onClick="ShowLeft()" style="background-image: url(../../Images/bg/button_left.gif);
                width: 8px; height: 500px; display: none; float: left; background-repeat: no-repeat;
                background-position: center"> </div>
    </div>
    <div id="right" style=" text-align: center; background-image: url(../../Images/bg/content_small.JPG);
            margin: 0;margin-left:180px">
      <iframe id="contentFrame" name="contentFrame" frameborder="0" scrolling="auto" src="../Common/WelcomePage.aspx"
                runat="server" width="100%" style="overflow: hidden; width: 100%; margin: 0 2px;
                border: solid 0px; height: 700px;"></iframe>
    </div>
  </div>
</asp:Content>
