<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="組別作品.cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="Panel0" runat="server">
        <asp:Label ID="Label2" runat="server" Text="商品" Width="33%"></asp:Label>
        <asp:Label ID="Label3" runat="server" Text="數量" Width="33%"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="小計" Width="33%"></asp:Label>
    </asp:Panel>

        <asp:Panel ID="Panel1" runat="server">
            
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Right">
        <asp:Label ID="Label1" runat="server" Text="合計:"></asp:Label>
        <asp:Label ID="lb_total" runat="server" Text="0"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btn_checkout" runat="server" Text="結帳" OnClick="btn_checkout_Click" />

    </asp:Panel>
    

</asp:Content>
