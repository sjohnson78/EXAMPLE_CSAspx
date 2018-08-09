<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MultiRecordODS.aspx.cs" Inherits="WebApp.SamplePages.MultiRecordODS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div class="page-header">
        <h1>Product MultiRecord Object DataSource</h1>
    </div>

    <div class="row col-md-12">
        <div class="alert alert-warning">
            <blockquote style="font-style: italic">
                This illustrates a display of multiple records from a query.
                The parameter will be submitted from either a drop down list
                or a textbox. The resulting dataset is of the enity Product.
                The output will be displayed in a customer GridView.
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
        <asp:Literal ID="Literal1" runat="server" Text="Categories:"></asp:Literal>
        &nbsp;&nbsp;
        <asp:DropDownList ID="CategoryList" runat="server" 
             AppendDataBoundItems="true"
            DataSourceID="CategoryListODS" 
          DataTextField="CategoryName" 
          DataValueField="CategoryID">
            <asp:ListItem Value="0">select...</asp:ListItem>
        </asp:DropDownList>
       
        &nbsp;&nbsp;
        <asp:LinkButton ID="FetchCategoryProducts" runat="server" OnClick="FetchCategoryProducts_Click">Fetch</asp:LinkButton>
        <br />
    </div>
    
    <div class="row">
        <asp:GridView ID="ProductList" runat="server" AutoGenerateColumns="False" DataSourceID="ProductListODS" AllowPaging="True" PageSize="5">
            <Columns>
                <asp:TemplateField HeaderText="ID" SortExpression="ProductID">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ProductID") %>' ID="Label1"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="ProductName">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("ProductName") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Supplier" SortExpression="SupplierID">
                    <ItemTemplate>
                      <%--  <asp:Label runat="server" Text='<%# Bind("SupplierID") %>' ID="Label3"></asp:Label>--%>
                        <asp:DropDownList ID="SupplierIDList" runat="server"
                            DataSourceID="SupplierIDListODS" 
                            DataTextField="CompanyName" 
                            DataValueField="SupplierID"
                            selectedvalue='<%# Eval("SupplierID") %>'
                             Enabled="false">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category" SortExpression="CategoryID">
                    <ItemTemplate>
                      <%--  <asp:Label runat="server" Text='<%# Bind("CategoryID") %>' ID="Label4"></asp:Label>--%>
                        <asp:DropDownList ID="CategoryIDList" runat="server"
                            DataSourceID="CategoryListODS" 
                            DataTextField="CategoryName" 
                            DataValueField="CategoryID"
                            selectedvalue='<%# Eval("CategoryID") %>'
                             Enabled="false">
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                    <ItemTemplate>
                        <asp:Label runat="server" 
                            Text='<%# string.Format("{0:0.00}",Eval("UnitPrice")) %>' 
                            ID="Label5"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Disc" SortExpression="Discontinued">
                    <ItemTemplate>
                        <asp:CheckBox runat="server" Checked='<%# Bind("Discontinued") %>' Enabled="false" ID="CheckBox1"></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
     
    </div>
    <asp:ObjectDataSource ID="CategoryListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Categories_List" TypeName="NorthwindSystem.BLL.CategoryController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SupplierIDListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Suppliers_List" TypeName="NorthwindSystem.BLL.SupplierController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ProductListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Products_GetByCategories" TypeName="NorthwindSystem.BLL.ProductController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CategoryList" PropertyName="SelectedValue" DefaultValue="0" Name="categoryid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
