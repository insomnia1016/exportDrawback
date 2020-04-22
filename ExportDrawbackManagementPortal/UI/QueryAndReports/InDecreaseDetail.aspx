<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InDecreaseDetail.aspx.cs" Inherits="UI_QueryAndReports_InDecreaseDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>增减单详情</title>
    <style type="text/css">
        body {
        }

        .box {
            margin: 0 auto;
            width: 800px;
            /*border: 1px solid #00F;*/
        }

        p {
            text-align: center;
        }

        ul li {
            list-style-type: none;
            margin-top: 20px;
        }
    </style>
    <style media="print" type="text/css">
        .noprint {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function printme() {
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="box" style="margin-top:200px;">
            <div style="width: 800px; font-size: 13px;">

                <ul>
                    <li style="text-align: center;">
                        <asp:Label ID="lbl_title" runat="server" Style="text-align: center; width: 680px; font-size: 20px; font-weight: bold; height: 22px;" Text="Label">增减费用单</asp:Label>
                    </li>
                </ul>


                <ul>
                    <li>
                        <span style="margin-left: 20px;">客户：</span>
                        <asp:Label ID="lbl_customer" Width="200px" runat="server" Text="Label"></asp:Label>
                        <span>币制：</span>
                        <asp:Label ID="lbl_currency" Width="200px" runat="server" Text="Label"></asp:Label>
                        <span style="margin-left: 50px;">单据编号：</span>
                        <asp:Label ID="lbl_indecrease_id" Width="80px" runat="server" Text="Label"></asp:Label>
                    </li>
                </ul>

                <ul>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="name" HeaderText="费用名称" />
                            <asp:BoundField DataField="amount" DataFormatString="{0:N}" HeaderText="金额" />
                            <asp:BoundField DataField="type" HeaderText="费用类型" />
                            <asp:BoundField DataField="apply_date" DataFormatString="{0:d}" HeaderText="申请日期" />
                            <asp:BoundField DataField="note" HeaderText="备注" />
                        </Columns>
                        <FooterStyle BackColor="LightCyan" HorizontalAlign="Center" ForeColor="MediumBlue" />
                    </asp:GridView>
                </ul>

                <ul>

                    <br />
                    <br />
                    <li style="margin-top: 1px;">
                        <span style="margin-left: 493px;">申请人： </span>
                        <asp:Label ID="lbl_agenter" runat="server" Width="80px"></asp:Label>
                    </li>
                    <li style="margin-bottom: 60px; margin-top: 1px;">
                        <span style="margin-left: 493px;">日期： </span>
                        <asp:Label ID="lbl_agente_date" runat="server" Width="120px" Text="Label"></asp:Label>
                    </li>
                </ul>
                <ul>
                    <li>

                        <div class="noprint" >
                            <p>
                            <asp:Button ID="print" style="font-weight: bold; font-size: 23px;" runat="server" Text="打印" OnClientClick="printme();" />
                                </p>
                        </div>

                    </li>
                </ul>
            </div>
        </div>
    </form>
</body>
</html>
