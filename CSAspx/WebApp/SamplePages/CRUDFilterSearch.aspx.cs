
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


#region Additional Namespaces
using NorthwindSystem.BLL;
using Northwind.Data.Entities;
// use Manage NuGet Packages to add EntityFramework
// add the reference assembly System.Data.Entity
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
#endregion

namespace WebApp.SamplePages
{
    public partial class CRUDFilterSearch : System.Web.UI.Page
    {
        List<string> errormsgs = new List<string>();

        #region Page Loading
        protected void Page_Load(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
               BindCategoryList();
            }
        }

        public void BindCategoryList()
        {
            //TODO: code the method to load the CategoryList control
            try
            {
                CategoryController sysmgr = new CategoryController();
                List<Category> info = sysmgr.Categories_List();
                info.Sort((x, y) => x.CategoryName.CompareTo(y.CategoryName));
                CategoryList.DataSource = info;
                CategoryList.DataTextField = nameof(Category.CategoryName);
                CategoryList.DataValueField = nameof(Category.CategoryID);
                CategoryList.DataBind();
                CategoryList.Items.Insert(0, "select ...");
            }
            catch (Exception ex)
            {
                errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
        }
        #endregion

        #region Error Handling and Rendering
        //use this method to discover the inner most error message.
        //this routine  has been created by the user
        protected Exception GetInnerException(Exception ex)
        {
            //drill down to the inner most exception
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        //use this method to load a DataList with a variable
        //number of message lines.
        //each line is a string
        //the strings (lines) are passed to this routine in
        //   a List<string>
        //second parameter is the bootstrap cssclass
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //
            if (Request["EditSupplierIDState"] == "true")
                EditSupplierID.Attributes.Add("open", string.Empty);
            else
                EditSupplierID.Attributes.Remove("open");


        }
        #endregion

        #region Filter Search Lookup
        protected void FilterProducts_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchProductName.Text))
            {
                errormsgs.Add("Enter the product name.");
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
            else
            {
                try
                {
                    ProductController sysmgr = new ProductController();
                    List<Product> info = sysmgr.Products_GetByPartialProductName(SearchProductName.Text);
                    info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                    ProductList.DataSource = info;
                    ProductList.DataTextField = nameof(Product.ProductName);
                    ProductList.DataValueField = nameof(Product.ProductID);
                    ProductList.DataBind();
                    ProductList.Items.Insert(0, "select ...");
                    if (info.Count == 0)
                    {
                        errormsgs.Add("No products found with supplied product name");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
            }
        }
        protected void SearchProduct_Click(object sender, EventArgs e)
        {
            //TODO: code this method to lookup and display the selected product

            //do you have something to search for
            //the dropdownlist has a prompt line in index 0
            if (ProductList.SelectedIndex == 0)
            {
                errormsgs.Add("Select a product to fetch.");
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
            else
            {
                //if you are leaving the web project to access
                //   code within another project, you should use
                //   user-freindly error handling
                try
                {
                    //to use an object, create an instance of the object
                    ProductController sysmgr = new ProductController();

                    //call the appropriate method within your instance
                    Product info = sysmgr.Products_GetProduct(int.Parse(ProductList.SelectedValue));
                   
                    //check that a product was found
                    //single record data is checked against null
                    //multi-record data is checked against .Count
                    if (info == null)
                    {
                        //the product was not found
                        //message to the user
                        //refresh the incorrect dropdownlist to reflect the acturate current product list
                        errormsgs.Add("Product was not found. Select and try again.");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                        FilterProducts_Click(sender, e);
                        //optionally you could empty the product data fields.
                    }
                    else
                    {
                        //product was found, display
                        ProductID.Text = info.ProductID.ToString();
                        ProductName.Text = info.ProductName;
                        SupplierID.Text = info.SupplierID == null ? "" :
                                                        info.SupplierID.ToString();
                        CompanyName.Text = GetSupplierCompanyName(SupplierID.Text);
                        CategoryList.SelectedValue = info.CategoryID == null ? "select ..." :
                                                        info.CategoryID.ToString();
                        QuantityPerUnit.Text = info.QuantityPerUnit == null ? "" : info.QuantityPerUnit;
                        UnitPrice.Text = info.UnitPrice == null ? "" : string.Format("{0:0.00}", info.UnitPrice);
                        UnitsInStock.Text = info.UnitsInStock == null ? "" : info.UnitsInStock.ToString();
                        UnitsOnOrder.Text = info.UnitsOnOrder == null ? "" : info.UnitsOnOrder.ToString();
                        ReorderLevel.Text = info.ReorderLevel == null ? "" : info.ReorderLevel.ToString();
                        Discontinued.Checked = info.Discontinued;
                    }
                }
                catch(Exception ex)
                {
                    errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
            }

        }
        protected void ClearFilters_Click(object sender, EventArgs e)
        {
            SearchProductName.Text = "";
            ProductList.Items.Clear();
            Clear_Click(sender, e); //optional
        }

        protected string GetSupplierCompanyName(string supplierid)
        {
            string companyname = "";
            if (!string.IsNullOrEmpty(supplierid))
            {
                try
                {
                    SupplierController sysmgr = new SupplierController();
                    Supplier info = sysmgr.Suppliers_GetSupplier(int.Parse(supplierid));
                    if (info != null)
                    {
                        companyname = info.CompanyName;
                    }

                }
                catch (Exception ex)
                {
                    errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
            }
            return companyname;
        }
        #endregion

        #region CRUD Events
        protected void Clear_Click(object sender, EventArgs e)
        {
            ProductID.Text = "";
            ProductName.Text = "";
            SupplierID.Text = "";
            CompanyName.Text = "";
            CategoryList.SelectedIndex = -1; //reset to prompt line
            QuantityPerUnit.Text = "";
            UnitPrice.Text = "";
            UnitsInStock.Text = "";
            UnitsOnOrder.Text = "";
            ReorderLevel.Text = "";
            Discontinued.Checked = false;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //re-execute form validation
            if (Page.IsValid)
            {
                //other validation such as ensuring that selection
                //   has been made on dropdownlists
                //for this example, we will assume that the 
                //   SupplierID and CategoryID are required
                if (string.IsNullOrEmpty(SupplierID.Text))
                {
                    errormsgs.Add("Supplier was not set.");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                else if (CategoryList.SelectedIndex == 0)
                {
                    errormsgs.Add("Category was not selected.");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                else
                {
                    try
                    {
                        //create a new instance of the entity to be add
                        Product newProduct = new Product();
                        //extract the web data and load your new instance
                        newProduct.ProductName = ProductName.Text;
                        newProduct.SupplierID = int.Parse(SupplierID.Text);
                        newProduct.CategoryID = int.Parse(CategoryList.SelectedValue);
                        newProduct.QuantityPerUnit = QuantityPerUnit.Text == null ? null :
                                                        QuantityPerUnit.Text;
                        if(string.IsNullOrEmpty(UnitPrice.Text))
                        {
                            newProduct.UnitPrice = null;
                        }
                        else
                        {
                            newProduct.UnitPrice = decimal.Parse(UnitPrice.Text);
                        }
                        if (string.IsNullOrEmpty(UnitsInStock.Text))
                        {
                            newProduct.UnitsInStock = null;
                        }
                        else
                        {
                            newProduct.UnitsInStock = Int16.Parse(UnitsInStock.Text);
                        }
                        if (string.IsNullOrEmpty(UnitsOnOrder.Text))
                        {
                            newProduct.UnitsOnOrder = null;
                        }
                        else
                        {
                            newProduct.UnitsOnOrder = Int16.Parse(UnitsOnOrder.Text);
                        }
                        if (string.IsNullOrEmpty(ReorderLevel.Text))
                        {
                            newProduct.ReorderLevel = null;
                        }
                        else
                        {
                            newProduct.ReorderLevel = Int16.Parse(ReorderLevel.Text);
                        }
                        //what about Discontinued??
                        //logically one would assume you would not add a discontinued
                        //    item to your product list; therfore; this field would logically
                        //    be false on the Add
                        //newProduct.Discontinued = false;
                        newProduct.Discontinued = Discontinued.Checked;

                        //connect to the system (BLL)
                        //issue your call
                        //check results
                        ProductController sysmgr = new ProductController();
                        int newproductid = sysmgr.Products_Add(newProduct);

                        ProductID.Text = newproductid.ToString();
                        //refresh the ProductList to show the new product in the list
                       
                        //communicate to the user
                        errormsgs.Add("Product has been added");
                        LoadMessageDisplay(errormsgs, "alert alert-success");

                    }
                    catch (DbUpdateException ex)
                    {
                        UpdateException updateException = (UpdateException)ex.InnerException;
                        if (updateException.InnerException != null)
                        {
                            errormsgs.Add(updateException.InnerException.Message.ToString());
                        }
                        else
                        {
                            errormsgs.Add(updateException.Message);
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                errormsgs.Add(validationError.ErrorMessage);
                            }
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (Exception ex)
                    {
                        errormsgs.Add(GetInnerException(ex).ToString());
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }

                }
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //other validation such as ensuring that selection
                //   has been made on dropdownlists
                //for this example, we will assume that the 
                //   SupplierID and CategoryID are required
                if (string.IsNullOrEmpty(ProductID.Text))
                {
                    errormsgs.Add("Select a product to maintain.");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                else if (string.IsNullOrEmpty(SupplierID.Text))
                {
                    errormsgs.Add("Supplier was not set.");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                else if (CategoryList.SelectedIndex == 0)
                {
                    errormsgs.Add("Category was not selected.");
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
                else
                {
                    try
                    {
                        //create a new instance of the entity to be add
                        Product newProduct = new Product();
                        //extract the web data and load your new instance
                        //update needs the ProductID to find the record to alter on
                        //     the database
                        newProduct.ProductID = int.Parse(ProductID.Text);
                        newProduct.ProductName = ProductName.Text;
                        newProduct.SupplierID = int.Parse(SupplierID.Text);
                        newProduct.CategoryID = int.Parse(CategoryList.SelectedValue);
                        newProduct.QuantityPerUnit = QuantityPerUnit.Text == null ? null :
                                                        QuantityPerUnit.Text;
                        if (string.IsNullOrEmpty(UnitPrice.Text))
                        {
                            newProduct.UnitPrice = null;
                        }
                        else
                        {
                            newProduct.UnitPrice = decimal.Parse(UnitPrice.Text);
                        }
                        if (string.IsNullOrEmpty(UnitsInStock.Text))
                        {
                            newProduct.UnitsInStock = null;
                        }
                        else
                        {
                            newProduct.UnitsInStock = Int16.Parse(UnitsInStock.Text);
                        }
                        if (string.IsNullOrEmpty(UnitsOnOrder.Text))
                        {
                            newProduct.UnitsOnOrder = null;
                        }
                        else
                        {
                            newProduct.UnitsOnOrder = Int16.Parse(UnitsOnOrder.Text);
                        }
                        if (string.IsNullOrEmpty(ReorderLevel.Text))
                        {
                            newProduct.ReorderLevel = null;
                        }
                        else
                        {
                            newProduct.ReorderLevel = Int16.Parse(ReorderLevel.Text);
                        }
                       
                        newProduct.Discontinued = Discontinued.Checked;

                        //connect to the system (BLL)
                        //issue your call
                        //check results
                        ProductController sysmgr = new ProductController();
                        int rowsaffected = sysmgr.Products_Update(newProduct);

                        if (rowsaffected == 0)
                        {
                           
                            //communicate to the user
                            errormsgs.Add("Product has not been updated. Product not on file");
                            LoadMessageDisplay(errormsgs, "alert alert-warning");
                        }
                        else
                        {
                            //communicate to the user
                            errormsgs.Add("Product has been updated");
                            LoadMessageDisplay(errormsgs, "alert alert-success");
                        }
                    }
                    catch (DbUpdateException ex)
                    {
                        UpdateException updateException = (UpdateException)ex.InnerException;
                        if (updateException.InnerException != null)
                        {
                            errormsgs.Add(updateException.InnerException.Message.ToString());
                        }
                        else
                        {
                            errormsgs.Add(updateException.Message);
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (var entityValidationErrors in ex.EntityValidationErrors)
                        {
                            foreach (var validationError in entityValidationErrors.ValidationErrors)
                            {
                                errormsgs.Add(validationError.ErrorMessage);
                            }
                        }
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }
                    catch (Exception ex)
                    {
                        errormsgs.Add(GetInnerException(ex).ToString());
                        LoadMessageDisplay(errormsgs, "alert alert-danger");
                    }

                }
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProductID.Text))
            {
                errormsgs.Add("Select a product to maintain.");
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
            else
            {
                try
                {
                    //connect to the system (BLL)
                    //issue your call
                    //check results
                    ProductController sysmgr = new ProductController();
                    int rowsaffected = sysmgr.Products_Delete(int.Parse(ProductID.Text));
                    if (rowsaffected == 0)
                    {
                        //communicate to the user
                        errormsgs.Add("Product has not been discontinued. Product not on file");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                    else
                    {
                        //point to the new product in the list
                        Discontinued.Checked = true;
                        //communicate to the user
                        errormsgs.Add("Product has been discontinued");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                }
                catch (DbUpdateException ex)
                {
                    UpdateException updateException = (UpdateException)ex.InnerException;
                    if (updateException.InnerException != null)
                    {
                        errormsgs.Add(updateException.InnerException.Message.ToString());
                    }
                    else
                    {
                        errormsgs.Add(updateException.Message);
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            errormsgs.Add(validationError.ErrorMessage);
                        }
                    }
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }

        protected void ShortListSuppliers_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SearchCompanyName.Text))
            {
                errormsgs.Add("Enter a company name.");
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
            else
            {
                try
                {
                    SupplierController sysmgr = new SupplierController();
                    List<Supplier> info = sysmgr.Suppliers_GetByPartialCompanyName(SearchCompanyName.Text);
                    info.Sort((x, y) => x.CompanyName.CompareTo(y.CompanyName));
                    SupplierList.DataSource = info;
                    SupplierList.DataTextField = nameof(Supplier.CompanyName);
                    SupplierList.DataValueField = nameof(Supplier.SupplierID);
                    SupplierList.DataBind();
                    SupplierList.Items.Insert(0, "select ...");
                    if (info.Count == 0)
                    {
                        errormsgs.Add("No suppliers found with supplied company name");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                    LoadMessageDisplay(errormsgs, "alert alert-warning");
                }
            }
        }
        
        protected void FetchSupplier_Click(object sender, EventArgs e)
        {
            if (SupplierList.SelectedIndex == 0)
            {
                errormsgs.Add("Select a supplier");
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
            else
            {
                SupplierID.Text = SupplierList.SelectedValue;
                CompanyName.Text = SupplierList.SelectedItem.Text;
            }
        }
        #endregion
    }
}