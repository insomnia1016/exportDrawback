<%@ Page Title="订单实际利润核算" Language="C#" MasterPageFile="~/UI/MasterPage/DetailPage.master" AutoEventWireup="true" CodeFile="ActualProfitAccounting.aspx.cs" Inherits="UI_Profit_ActualProfitAccounting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ul class="queryarea">
        <li ><span class="title">销售发单号</span>
            <span class="control">
                <asp:TextBox ID="txt_sale_bill_no"  runat="server"  Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li ><span class="title">实际结汇收入</span>
            <span class="control">
                <asp:TextBox ID="txt_actual_amount" runat="server" AutoPostBack="true" OnTextChanged="txt_value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li ><span class="title">实际退税</span>
            <span class="control">
                <asp:TextBox ID="txt_return_tax" runat="server" AutoPostBack="true" OnTextChanged="txt_value_TextChanged" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
         <li ><span class="title">采购产品费用</span>
            <span class="control">
                <asp:TextBox ID="txt_actual_pay" runat="server"  AutoPostBack="true" OnTextChanged="txt_value_TextChanged"  onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
        <li ><span class="title">额外费用</span>
            <span class="control">
                <asp:TextBox ID="txt_extra_charges" AutoPostBack="true" OnTextChanged="txt_value_TextChanged" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>

         <li ><span class="title">提成</span>
            <span class="control">
                <asp:TextBox ID="txt_commission" AutoPostBack="true" OnTextChanged="txt_value_TextChanged" runat="server" onKeyPress="if (event.keyCode!=46 && (event.keyCode<48 || event.keyCode>57)) event.returnValue=false" Style="text-align: center"></asp:TextBox>
            </span>
        </li>
         <li ><span class="title">实际利润:</span>
            <span class="control"  style="padding-left: 10px">
                <asp:Label ID="lbl_actual_profit_amount"  runat="server" ></asp:Label>
            </span>
        </li>
         <li ><span class="title">实际利润率:</span>
            <span class="control"  style="padding-left: 10px">
                <asp:Label ID="lbl_actual_profit"  runat="server" ></asp:Label> 
            </span>
        </li>
        <li style="width:100%;position: relative;left: 45%;" >
            <asp:Button ID="save" runat="server" OnClick="save_Click" Text="保   存" Enabled="false"/>
        </li>
        <li>
            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
        </li>
    </ul>
    <div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20">
            <Columns>
                <asp:TemplateField HeaderText="实际核算">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="选择" CommandName="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataTextField="sale_bill_no" HeaderText="销售发单号" Target="_blank" DataNavigateUrlFields="sale_bill_no" DataNavigateUrlFormatString="ProfitDetail.aspx?id={0}" />
                <asp:BoundField DataField="extra_charges" DataFormatString="{0:N2}" HeaderText="额外费用" />
                <asp:BoundField DataField="update_time" HeaderText="更新时间" />
                <asp:BoundField DataField="audit_state" HeaderText="审核状态" />
            </Columns>
        </asp:GridView>
       
    </div>
</asp:Content>

