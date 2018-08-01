<%@ Page Title="Pkey Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Query.aspx.cs" Inherits="WebApp.SamplePages.Query" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Primary Query</h1>
     <div class="row">
        <div class="col-sm-offset-1 col-sm-10 alert alert-info">
            <blockquote style="font-style:italic">
                This page will demonstrate CRUD against the Products table using web forms as the user interface.
                The web form will make calls to an application library called NorthwindSystem. This 
                application library containers public classes that will server as the web site interface
                to the data base. Each entity (sql table) will have its own controller.
            </blockquote>
        </div>
    </div>
    <div class="row">
        <asp:DataList ID="Message" runat="server">
          <ItemTemplate>
               <%# Container.DataItem %>
          </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="row">
        <div class="grid-form">
            <asp:Label ID="label1" runat="server" AssociatedControlID="ProductList"
                text="Select a product to manage:"></asp:Label>
            <asp:DropDownList ID="ProductList" runat="server"></asp:DropDownList>
             <asp:Button ID="SearchProduct" runat="server" OnClick="SearchProduct_Click" text="Fetch"></asp:Button>
            <br/>
            <asp:Label ID="Label2" runat="server" AssociatedControlID="ProductID" 
                text="ID:"></asp:Label>
            <asp:Literal ID="ProductID" runat="server"></asp:Literal>

            <asp:Label ID="Label3" runat="server" AssociatedControlID="ProductName" 
                text="Name:"></asp:Label>
            <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
           
            <asp:Label ID="Label4" runat="server" AssociatedControlID="SupplierList" 
                text="Supplier:"></asp:Label>
            <asp:DropDownList ID="SupplierList" runat="server"></asp:DropDownList>

            <asp:Label ID="Label5" runat="server" AssociatedControlID="CategoryList" 
                text="Category:"></asp:Label>
            <asp:DropDownList ID="CategoryList" runat="server"></asp:DropDownList>

            <asp:Label ID="Label6" runat="server" AssociatedControlID="QuantityPerUnit" 
                text="Size:"></asp:Label>
            <asp:TextBox ID="QuantityPerUnit" runat="server"></asp:TextBox>

            <asp:Label ID="Label7" runat="server" AssociatedControlID="UnitPrice" 
                text="Price:"></asp:Label>
            <asp:TextBox ID="UnitPrice" runat="server"></asp:TextBox>

            <asp:Label ID="Label8" runat="server" AssociatedControlID="UnitsInStock" 
                text="QOH:"></asp:Label>
            <asp:TextBox ID="UnitsInStock" runat="server"></asp:TextBox>

            <asp:Label ID="Label9" runat="server" AssociatedControlID="UnitsOnOrder" 
                text="QOO:"></asp:Label>
            <asp:TextBox ID="UnitsOnOrder" runat="server"></asp:TextBox>

            <asp:Label ID="Label10" runat="server" AssociatedControlID="ReorderLevel" 
                text="ROL:"></asp:Label>
            <asp:TextBox ID="ReorderLevel" runat="server"></asp:TextBox>

            <asp:Label ID="Label11" runat="server" AssociatedControlID="Discontinued" 
                text="Disc:"></asp:Label>
            <asp:CheckBox ID="Discontinued" runat="server" Text="(discontinued if checked)" />
        </div>
    </div>

</asp:Content>
