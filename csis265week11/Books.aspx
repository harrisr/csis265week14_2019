<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    
    CodeBehind="Books.aspx.cs" 
    Inherits="csis265week11.Books" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Books"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Book Name:"></asp:Label>
        <asp:TextBox ID="txtBookName" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Book Desc:"></asp:Label>
        <asp:TextBox ID="txtBookDesc" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:HiddenField ID="hdnBookId" runat="server" />
    </p>
    <p>
        <asp:DropDownList ID="drpBooks" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
    </p>


    <p>
        <asp:DropDownList ID="drpGenres" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        <asp:DropDownList ID="drpAuthors" runat="server">
        </asp:DropDownList>
    </p>


    <p>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="False" OnClick="btnSubmit_Click" Text="Add" />
    </p>




</asp:Content>
