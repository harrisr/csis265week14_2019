<%@ Page Title="" Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Genres.aspx.cs" 
    Inherits="csis265week11.Genres" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Genres"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Genre Name:"></asp:Label>
        <asp:TextBox ID="txtGenre" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:HiddenField ID="hdnGenreId" runat="server" />
    </p>
    <p>
        <asp:DropDownList ID="drpGenres" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
    </p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="False" OnClick="btnSubmit_Click" Text="Add" />
    </p>




</asp:Content>
