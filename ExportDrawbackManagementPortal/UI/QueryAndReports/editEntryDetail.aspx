<%@ Page Title="报关单明细修改" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="editEntryDetail.aspx.cs" Inherits="UI_QueryAndReports_editEntryDetail" %>

<%@ Register Assembly="ExportDrawbackManagement.WebControls" Namespace="WebControls" TagPrefix="ExportDrawbackManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <h2>编辑报关单表头数据</h2>
    <ul class="queryarea">
        <li><span class="title">客户</span>
            <span class="control">
                <asp:TextBox ID="owner_name" Width="180px" runat="server"></asp:TextBox>
                
            </span>
        </li>
        <li><span class="title">报关日期</span>
            <span class="control">
                <ExportDrawbackManagement:CalendarBox ID="d_date" runat="server" FormatString="yyyy-MM-dd HH:mm:ss" ResourcePath="../../Calendar"></ExportDrawbackManagement:CalendarBox>

            </span>
        </li>
        <li><span class="title">申报单位</span>
            <span class="control">
                <asp:TextBox ID="agent_name" Width="180px" runat="server"></asp:TextBox>

            </span>
        </li>
        <li><span class="title">报关单号</span>
            <span class="control">
                <asp:TextBox ID="entry_id" Width="180px" ReadOnly="true"  runat="server" ></asp:TextBox>
            </span>
        </li>

    </ul>

    <h2 style="float: left; text-align: left; width: 100%;">编辑报关单表体数据</h2>
    <div id="div1" style="text-align: center">
        <table border="1" style="width: 100%;">
            <thead>
                <tr>
                    <th>报关品名
                    </th>
                    <th>海关编码
                    </th>
                    <th>品名项号
                    </th>
                    <th>数量
                    </th>
                    <th>计量单位
                    </th>
                    <th>成交币制
                    </th>
                    <th>申报单价
                    </th>
                    <th>报关金额
                    </th>
                    <th>出口退税率
                    </th>

                </tr>
            </thead>
            <tbody>

                <tr>
                    <td>
                        <asp:TextBox ID="txt_g_name" runat="server" ></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_code_ts" runat="server"  onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_g_no" ReadOnly="true" runat="server" ></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_g_qty" runat="server"  onKeyPress="if (event.keyCode<48 || event.keyCode>57) event.returnValue=false"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_g_unit" runat="server" ></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_trade_curr" runat="server" ></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_decl_price" runat="server"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_decl_total" runat="server"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txt_drawback_rate" runat="server"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false"></asp:TextBox></td>
                </tr>


            </tbody>
        </table>
    </div>

    <ul class="queryarea">
        <li style="width: 50%">
            <asp:Button ID="btnSave" runat="server" Text="保  存" OnClick="btnSave_Click" Style="margin-left: 40%;" Width="100px" />
        </li>
        <li style="width: 50%">
            <asp:TextBox ID="txtEntryId" Width="180px" runat="server"></asp:TextBox>
        
            <asp:Button ID="Button2" runat="server" Text="查  询" Style="margin-left: 5%;" OnClick="btnQuery_Click"  Width="100px" />
        </li>
        <li style="width: 100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" PageSize="15" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="编辑" CommandName="Select" />
                    <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("entry_id")+","+Eval("g_no")  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="entry_id" HeaderText="报关单号" />
            <asp:BoundField DataField="g_no" HeaderText="项号" />
            <asp:BoundField DataField="owner_name" HeaderText="客户" />
            <asp:BoundField DataField="d_date" HeaderText="申报时间" />
            <asp:BoundField DataField="agent_name" HeaderText="申报公司" />
            <asp:BoundField DataField="g_name" HeaderText="商品名称" />
            <asp:BoundField DataField="g_qty" DataFormatString="{0:N}" HeaderText="法定数量" />
            <asp:BoundField DataField="g_unit" HeaderText="法定单位" />
            <asp:BoundField DataField="trade_curr" HeaderText="成交币制" />
            <asp:BoundField DataField="decl_price" DataFormatString="{0:N}" HeaderText="申报单价" />
            <asp:BoundField DataField="decl_total" DataFormatString="{0:N}" HeaderText="申报总价" />
            <asp:BoundField DataField="code_ts" HeaderText="商品编码" />
            <asp:BoundField DataField="drawback_rate" DataFormatString="{0:N3}" HeaderText="退税率" />
            <asp:BoundField DataField="operator" HeaderText="操作员" />
            <asp:BoundField DataField="operate_time" HeaderText="操作时间" />
        </Columns>
    </asp:GridView>
</asp:Content>

