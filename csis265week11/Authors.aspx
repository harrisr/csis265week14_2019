<%@ Page Title="" Language="C#" 
    MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" 
    CodeBehind="Authors.aspx.cs" 
    Inherits="csis265week11.Authors" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">




    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Authors"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Author Name:"></asp:Label>
        <asp:TextBox ID="txtAuthor" runat="server"></asp:TextBox>
    </p>
<p>
        <asp:Label ID="Label3" runat="server" Text="Author Email: "></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:HiddenField ID="hdnAuthorId" runat="server" />
    </p>
    <p>
        <asp:DropDownList ID="drpAuthors" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" />
        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" />
    </p>
    <p>
        <asp:Repeater ID="rptAuthors" runat="server">

            <HeaderTemplate>
                <table border="1"    >
            </HeaderTemplate>
                
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblID" Text='<%#Eval("Id") %>'  runat="server"></asp:Label>
                    </td>

                    <td>
                        <asp:Label ID="lblName" Text='<%#Eval("Name") %>'  runat="server"></asp:Label>
                    </td>

                    <td>
                        <asp:Button ID="btnEdit" Text='Edit' OnClick="btnREdit_Click"  runat="server"></asp:Button>
                    </td>

                    <td>
                        <asp:Button ID="btnDelete" Text='Delete' OnClick="btnRDelete_Click"  runat="server"></asp:Button>
                    </td>

                </tr>
            </ItemTemplate>
            
            <FooterTemplate>
                </table>
            </FooterTemplate>

        </asp:Repeater>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:GridView ID="grdAuthors" 
            runat="server" 
            AutoGenerateColumns="false" 
             DataKeyNames="Id"
             OnRowEditing="grdAuthors_RowEditing"
             OnRowDeleting="grdAuthors_RowDeleting"
             OnRowUpdating="grdAuthors_RowUpdating"
             OnRowCancelingEdit="grdAuthors_RowCancelingEdit"
            >

            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:Label runat="server" ID="lblID" Text='<%#Eval("Id") %>'></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="Id" HeaderText="MyId" />
                <asp:BoundField DataField="Name" HeaderText="MyName" />
                <asp:BoundField DataField="Email" HeaderText="MyEmail" />
                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />

            </Columns>

        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="btnSubmit" runat="server" CausesValidation="False" OnClick="btnSubmit_Click" Text="Add" />
    </p>





</asp:Content>
