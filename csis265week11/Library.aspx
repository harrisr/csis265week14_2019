<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Library.aspx.cs" Inherits="csis265week11.Library" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Library"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Genre Name:"></asp:Label>
        <asp:TextBox ID="txtGenre" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:DropDownList ID="drpGenres" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="False" OnClick="btnSubmit_Click" Text="Submit" />
    </p>
</asp:Content>
