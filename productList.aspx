<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productList.aspx.cs" Inherits="組別作品.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:測試用資料庫01ConnectionString %>" SelectCommand="SELECT * FROM [products]"></asp:SqlDataSource>

<%--    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />--%>
<%--    <input id="Button2" runat="server" type="submit" value="button" onServerClick="Button2_Click" />--%>
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
</asp:Content>
