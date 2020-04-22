<%@ Page Title="实际利润核算列表" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ActualProfitAccountingList.aspx.cs" Inherits="UI_Profit_ActualProfitAccountingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ul class="queryarea">
        <li><span class="title">销售发单号</span>
            <span class="control">
                <asp:TextBox ID="txt_sale_bill_no" runat="server" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">实际结汇收入</span>
            <span class="control">
                <asp:TextBox ID="txt_actual_amount" runat="server" AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">实际退税</span>
            <span class="control">
                <asp:TextBox ID="txt_return_tax" runat="server" AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">采购产品费用</span>
            <span class="control">
                <asp:TextBox ID="txt_actual_pay" runat="server" AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">额外费用</span>
            <span class="control">
                <asp:TextBox ID="txt_extra_charges" AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>

        <li><span class="title">提成</span>
            <span class="control">
                <asp:TextBox ID="txt_commission" AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li><span class="title">实际利润:</span>
            <span class="control" style="padding-left: 10px">
                <asp:Label ID="lbl_actual_profit_amount" runat="server"></asp:Label>
            </span>
        </li>
        <li><span class="title">实际利润率:</span>
            <span class="control" style="padding-left: 10px">
                <asp:Label ID="lbl_actual_profit"  runat="server"></asp:Label>
            </span>
        </li>
        <li style="width: 100%; position: relative; left: 45%;">
            <asp:Button ID="save" runat="server" OnClick="save_Click" Text="保   存" Enabled="false" />
        </li>
        <li style="width:100%">
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <div>
        <ul class="queryarea">
            <li><span class="title">销售订单号: </span>
                <span class="control">
                    <asp:TextBox ID="txt_bill_no" runat="server" Style="text-align: center"></asp:TextBox>
                </span>
                <asp:Button ID="btn_query" runat="server" OnClick="btn_query_Click" Text="查询" />
            </li>
        </ul>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
             OnRowCommand="GridView1_RowCommand" 
            OnRowDataBound="GridView1_RowDataBound" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" 
            OnRowDeleting="GridView1_RowDeleting"
            AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="修改">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="选择" CommandName="Select" />
                        <asp:Button ID="btnDelete" runat="server" OnClientClick="if(!confirm('确定删除吗?')) return false;" Text="删除" CommandName="Delete" CommandArgument='<%# Eval("sale_bill_no")  %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="sale_bill_no" HeaderText="销售发单号" Target="_blank" DataNavigateUrlFields="sale_bill_no" DataNavigateUrlFormatString="ProfitDetail.aspx?id={0}" />
                <asp:BoundField DataField="actual_amount" DataFormatString="{0:N2}" HeaderText="实际结汇收入" />
                <asp:BoundField DataField="return_tax" DataFormatString="{0:N2}" HeaderText="实际退税" />
                <asp:BoundField DataField="actual_pay" DataFormatString="{0:N2}" HeaderText="采购产品费用" />
                <asp:BoundField DataField="extra_charges" DataFormatString="{0:N2}" HeaderText="额外费用" />
                <asp:BoundField DataField="commission" DataFormatString="{0:N3}" HeaderText="提成" />
                <asp:BoundField DataField="actual_profit_amount" DataFormatString="{0:N2}" HeaderText="实际利润" />
                <asp:BoundField DataField="actual_profit" DataFormatString="{0:N3}" HeaderText="实际利润率" />
                <asp:BoundField DataField="audit_status" DataFormatString="{0:N3}" HeaderText="实际核算状态" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

