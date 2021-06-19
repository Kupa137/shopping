<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productDetail.aspx.cs" Inherits="組別作品.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    商品細節
    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="lb_title" runat="server" Text="Label"></asp:Label>
        <br/>
        <asp:Label ID="lb_price" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="btn_addToCart" runat="server" Text="加入購物車" OnClick="btn_addToCart_Click" />
        <br />
        <asp:Label ID="lb_describe" runat="server" Text="Label"></asp:Label>
        
    </asp:Panel>



</asp:Content>
