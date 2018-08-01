
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
    public partial class Query : System.Web.UI.Page
    {
        List<string> errormsgs = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (!Page.IsPostBack)
            {
                BindProductList();
                //TODO: Code the methods for these calls
                BindSupplierList();
                BindCategoryList();
            }
        }

        public void BindProductList()
        {
            try
            {
                ProductController sysmgr = new ProductController();
                List<Product> info = sysmgr.Products_List();
                info.Sort((x, y) => x.ProductName.CompareTo(y.ProductName));
                ProductList.DataSource = info;
                ProductList.DataTextField = nameof(Product.ProductName);
                ProductList.DataValueField = nameof(Product.ProductID);
                ProductList.DataBind();
                ProductList.Items.Insert(0, "select ...");
            }
            catch(Exception ex)
            {
                errormsgs.Add("File Error: " + GetInnerException(ex).Message);
                LoadMessageDisplay(errormsgs, "alert alert-warning");
            }
        }

        public void BindSupplierList()
        {
            //TODO: code the method to load the SupplierList control
        }

        public void BindCategoryList()
        {
            //TODO: code the method to load the CategoryList control
        }

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

        protected void SearchProduct_Click(object sender, EventArgs e)
        {
            //TODO: code this method to lookup and display the selected product
        }
    }
}