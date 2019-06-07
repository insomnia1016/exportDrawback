<%@ Page Language="C#" MasterPageFile="~/UI/MasterPage/ContentPage.master" AutoEventWireup="true" CodeFile="WelcomePage.aspx.cs" Inherits="UI_Common_WelcomePage" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    //parent.SetSelectSmallMenuItem("DipOrg002");
function popUpWindow(winName)
{
    var popUpWin;
    if(popUpWin!=null)
        popUpWin.close();
    popUpWin = window.open(winName,'popUpWin', 'toolbar=no,location=no,directories=no,status=no,menub ar=no,scrollbar=no,resizable=no,copyhistory=yes,width=700,height=350,left=100,top=100,screenX=100,screenY=100','_blank');
}
</script>
<div style="text-align:center;background-image:url(../../Images/bg/content_small.JPG);">
<!--
<embed src="../../Images/content/sk3.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" style="width:734px; height:520px"></embed>
--><div style="background-image:url(../../Images/content/bg_welcome.jpg);width:658;height:500px; background-position:center center;margin:0 auto;background-repeat:no-repeat">
  <div style="padding-top:200px;"><asp:Label ID="Label1" runat="server" ></asp:Label></div></div>  <!--img src="../../Images/content/bg.jpg" alt="" /--></div>
</asp:Content>

