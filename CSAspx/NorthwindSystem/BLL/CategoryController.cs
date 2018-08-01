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
    public class CategoryController
    {
        public List<Category> Categories_List()
        {
            using (var context = new NorthwindContext())
            {
                return context.Categories.ToList();
            }
        }

        public Category Categories_GetCategory(int categoryid)
        {
            using (var context = new NorthwindContext())
            {
                return context.Categories.Find(categoryid);
            }
        }
    }
}
