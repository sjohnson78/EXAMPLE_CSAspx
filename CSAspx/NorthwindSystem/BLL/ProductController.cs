
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Northwind.Data.Entities;
using NorthwindSystem.DAL;
#endregion

namespace NorthwindSystem.BLL
{
    //this is the public interface class that will handle
    //  web page service requests for data to the Product sql table
    //Methods in this class can interact with the internal DAL Context class
    public class ProductController
    {
        //this method will return all records from the sql table Products
        //this will first create a transaction code block which uses
        //    the DAL Context class
        //the Context class has a DbSet<Product> property for referencing
        //    the sql table
        //The property works with EntityFramework to retrieve the data
        public List<Product> Products_List()
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.ToList();
            }
        }

        //this method will return a specific record from the sql
        //    Products table based on the primary key
        public Product Products_GetProduct(int productid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Products.Find(productid);
            }
        }
    }
}
