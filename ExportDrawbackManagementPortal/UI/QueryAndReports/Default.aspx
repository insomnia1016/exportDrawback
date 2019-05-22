<%@ Page Title="" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="UI_QueryAndReports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function outputNumbers() {

        var result = document.getElementById("<%=TextBox1.ClientID %>").value;
        alert(result);
    }
    function al() {
        if (confirm("are you  sure?")) {
            alert("I'm so glad you 're sure!");
        } else {
            alert("I'm sorry to hear you're not sure.");
        }
    }
    function aa() {
        var result = prompt("what's your name?","");
        if (result !== null) {
            alert("Welcome," + result);
           
        }
    }
    function loc() {
        location.reload(true);
    }
</script>
<asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
<asp:Button runat="server" Text="Button" OnClientClick="outputNumbers();"></asp:Button>
<asp:Button ID="Button3" runat="server" Text="左上角" OnClientClick=" aa();"></asp:Button>
<asp:Button ID="Button4" runat="server" Text="向下移" OnClientClick="loc();"></asp:Button>
<asp:Button ID="Button1" runat="server" Text="移动到" OnClientClick="window.moveTo(200,300);"></asp:Button>
<asp:Button ID="Button2" runat="server" Text="向左移" OnClientClick="window.moveBy(-50,0);"></asp:Button>

</asp:Content>

