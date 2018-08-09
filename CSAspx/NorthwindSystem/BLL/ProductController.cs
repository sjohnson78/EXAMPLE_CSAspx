
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Northwind.Data.Entities;
using NorthwindSystem.DAL;
using System.Data.SqlClient;
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

        //this method will add a new product to the sql product table
        //this method will do the add via EntityFramework
        //optionally, one can pass back the new IDENTITY value from
        //   the successful add
        public int Products_Add(Product newproduct)
        {
            //start the Insert transaction
            using (var context = new NorthwindContext())
            {
                //staging
                //stage the new record to the DbSet<T> for the object instance
                //at this time, the record IS NOT physically on the database
                context.Products.Add(newproduct);

                //commit the record to the database
                //any entity validation is done at this time
                //if this statement is NOT executed, the insert is NOT
                //     completed (Rollback)
                //if this statement is executed BUT FAILS for some reason
                //     the insert is NOT completed (Rollback)
                //if this statement is executed AND is successful then
                //     the insert has physically placed the record on the
                //     database. At this time you can retreive the new
                //     IDENTITY value
                context.SaveChanges();

                //after the success of the SaveChanges() you can access
                //    your instance for the new IDENTITY value
                return newproduct.ProductID;
            }
        }

        //this method updates the database
        //this method returns the number of records affected on the database
        public int Products_Update(Product item)
        {
            //start transaction
            using (var context = new NorthwindContext())
            {
                //stage
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;

                //commit
                return context.SaveChanges();
            }
            
        }

        //this method deletes a record from the database
        //   or
        //this method logically flags a record to be deemed deleted from the database
        //this method returns the number of records affected on the database
        public int Products_Delete(int productid)
        {
            //start transaction
            using (var context = new NorthwindContext())
            {
                ////physical delete
                //var existing = context.Products.Find(productid);
                ////stage
                //context.Products.Remove(existing);
                ////commit
                //return context.SaveChanges();

                //logical delete
                var existing = context.Products.Find(productid);
                //alter the data value on the record that will
                //   logically deem the deleted deleted
                //You should NOT rely on the user to do this
                //   alternation on the web form
                existing.Discontinued = true;
                //stage
                context.Entry(existing).State = System.Data.Entity.EntityState.Modified;
                //commit
                return context.SaveChanges();
            }

        }

        //to query your database using a non primary key value
        //this will require a sql procedure to call
        //the namespace System.Data.SqlClient is required
        //the returning datatype is IEnumerable<T>
        //this returning datatype will be cast using ToList() on the return
        public List<Product> Products_GetByPartialProductName(string partialname)
        {
            using (var context = new NorthwindContext())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByPartialProductName @PartialName",
                                    new SqlParameter("PartialName", partialname));
                return results.ToList();
            }
        }

        public List<Product> Products_GetByCategories(int categoryid)
        {
            using (var context = new NorthwindContext())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByCategories @CategoryID",
                                    new SqlParameter("CategoryID", categoryid));
                return results.ToList();
            }
        }

        public List<Product> Products_GetBySupplierPartialProductName(int supplierid, string partialproductname)
        {
            using (var context = new NorthwindContext())
            {
                //sometimes there may be a sql error that does not like the new SqlParameter()
                //       coded directly in the SqlQuery call
                //if this happens to you then code your parameters as shown below then
                //       use the parm1 and parm2 in the SqlQuery call instead of the new....
                //don't know why but its weird
                //var parm1 = new SqlParameter("SupplierID", supplierid);
                //var parm2 = new SqlParameter("PartialProductName", partialproductname);
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetBySupplierPartialProductName @SupplierID, @PartialProductName",
                                    new SqlParameter("SupplierID", supplierid),
                                    new SqlParameter("PartialProductName", partialproductname));
                return results.ToList();
            }
        }

        public List<Product> Products_GetForSupplierCategory(int supplierid, int categoryid)
        {
            using (var context = new NorthwindContext())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetForSupplierCategory @SupplierID, @CategoryID",
                                    new SqlParameter("SupplierID", supplierid),
                                    new SqlParameter("CategoryID", categoryid));
                return results.ToList();
            }
        }

        public List<Product> Products_GetByCategoryAndName(int category, string partialname)
        {
            using (var context = new NorthwindContext())
            {
                IEnumerable<Product> results =
                    context.Database.SqlQuery<Product>("Products_GetByCategoryAndName @CategoryID, @PartialName",
                                    new SqlParameter("CategoryID", category),
                                    new SqlParameter("PartialName", partialname));
                return results.ToList();
            }
        }
    }
}
