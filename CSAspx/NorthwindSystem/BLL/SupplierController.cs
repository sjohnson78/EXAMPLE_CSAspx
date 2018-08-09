using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Northwind.Data.Entities;
using Northwind.Data.Views;
using NorthwindSystem.DAL;
using System.Data.SqlClient;
#endregion

namespace NorthwindSystem.BLL
{
    public class SupplierController
    {
        public List<Supplier> Suppliers_List()
        {
            using (var context = new NorthwindContext())
            {
                return context.Suppliers.ToList();
            }
        }

        public Supplier Suppliers_GetSupplier(int supplierid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Suppliers.Find(supplierid);
            }
        }

        public List<SupplierCategories> Suppliers_GetCategories(int suppilerid)
        {
            using (var context = new NorthwindContext())
            {
                IEnumerable<SupplierCategories> results =
                    context.Database.SqlQuery<SupplierCategories>("Suppliers_GetCategories @SupplierID",
                                    new SqlParameter("SupplierID", suppilerid));
                return results.ToList();
            }
        }
    }
}
