<%@ Page Title="Filter CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDFilterSearch.aspx.cs" Inherits="WebApp.SamplePages.CRUDFilterSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> CRUD using Filter Searches</h1>
     <div class="row">
        <div class="col-sm-offset-1 col-sm-10 alert alert-info">
            <blockquote style="font-style:italic">
                This page will demonstrate CRUD using Filter Searches.<br /><br />
                The first filter search will be used on finding the product. The user will enter a partial product name and press the Products? button. This will load the dropdownlist of products. The Fetch button will retrieve the data of the selected Product. Reset clears the product filter search.<br /><br />
                The Supplier data is obtained using a filter search. The user will access this toggled search by clicking the Click to change link. This will reveal a filter search based on the Company Name (partial string). This filter search works in the same fashion as the product filter search.<br /><br />
                The Discontinued button has a confirmation script added to it. This is javascript code. 
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

  <%--  this is a filter search based on a text partial string
    the user enters the product partial string, presses the Products? button
    the Products? button loads the ProductList
    the Fetch button retrieves the selected Product--%>
    <div class="col-md-12 bs-grid">
        <fieldset class="form-inline">

            <asp:Label ID="Label12" runat="server" Text="Enter Product name"
                 AssociatedControlID="SearchProductName"></asp:Label>
            <asp:TextBox ID="SearchProductName" runat="server"
                 placeholder="partial name"></asp:TextBox>
            <asp:Button ID="FilterProducts" runat="server" Text="Products?"
                 CausesValidation="false" OnClick="FilterProducts_Click"/>

            <asp:Label ID="label1" runat="server" AssociatedControlID="ProductList"
                text="Select product"></asp:Label>
            <asp:DropDownList ID="ProductList" runat="server"></asp:DropDownList>
             <asp:Button ID="SearchProduct" runat="server" OnClick="SearchProduct_Click" text="Fetch"
                  CausesValidation="false"></asp:Button>

            <asp:Button ID="ClearFilters" runat="server" Text="Reset"
                 CausesValidation="false" OnClick="ClearFilters_Click"/>
       </fieldset>
    </div>
            <br/>
   <div class="row">
        <div class="grid-form">          
            <asp:Label ID="Label2" runat="server" AssociatedControlID="ProductID" 
                text="ID:"></asp:Label>
            <asp:Literal ID="ProductID" runat="server"></asp:Literal>

            <asp:Label ID="Label3" runat="server" AssociatedControlID="ProductName" 
                text="Name:"></asp:Label>
            <asp:TextBox ID="ProductName" runat="server"></asp:TextBox>
           
            <%-- example of a collapsable filter search
                 this example uses code localled in the Page_PreRender event--%>
             <asp:Label ID="Label4" runat="server" Text="Supplier"
                    AssociatedControlID="SupplierID"></asp:Label>
            <details id="EditSupplierID" runat="server" class="bs-grid" ontoggle="preserveToggle(this, 'EditSupplierIDState')">
                <summary>
                    (<asp:Label ID="SupplierID" runat="server"></asp:Label>)
                    <asp:Label ID="CompanyName" runat="server"></asp:Label>
                </summary>
                <input type="hidden" name="EditSupplierIDState" id="EditSupplierIDState" />
                <fieldset class="form-inline">
                    <asp:TextBox ID="SearchCompanyName" runat="server" placeholder="company name"></asp:TextBox>
                    <asp:Button ID="ShortListSuppliers" runat="server" CausesValidation="false" OnClick="ShortListSuppliers_Click"  Text="Company?"></asp:Button>
                    <asp:DropDownList ID="SupplierList" runat="server" ></asp:DropDownList  >
                    <asp:Button ID="FetchSupplier" runat="server" Text="Select" OnClick="FetchSupplier_Click" CausesValidation="false"/>
                </fieldset>
            </details>

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
    <br />
    <div class="row col-md-offset-2">
        <asp:Button ID="Add" runat="server" Text="Add"  
            height="26px" OnClick="Add_Click" width="67px" />&nbsp;&nbsp;
        <asp:Button ID="Update" runat="server" Text="Update" OnClick="Update_Click" />&nbsp;&nbsp;
        <asp:Button ID="Delete" runat="server" Text="Delete" height="26px" OnClick="Delete_Click" width="67px" CausesValidation="false" OnClientClick="return ConfirmDelete()" />&nbsp;&nbsp;
        <asp:Button ID="Clear" runat="server" Text="Clear" height="26px" OnClick="Clear_Click" width="67px"
            CausesValidation="false" />
         <script>
            function ConfirmDelete() {
                return confirm("Are you sure you want to discontinue this product?");
            }
            function preserveToggle(self, id) {
                console.log(self);
                console.log(self.open);
                console.log(document.getElementById(id));
                document.getElementById(id).value = self.open;
            }
        </script>
    </div>
</asp:Content>
