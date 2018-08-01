using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.Data.Entity;
using Northwind.Data.Entities;
#endregion
namespace NorthwindSystem.DAL
{
    // to add a little security to the database access we will
    //      set the access privilege of this class to internal
    // this access restricts calls to this class from within this
    //      project only
    // this class will be created by any BLL controller class method

    // this class will interact with the EntityFramework software to
    //     access the database
    // to setup this interaction, this class will inherit from
    //     EntityFramework its DbContext class
    internal class NorthwindContext:DbContext
    {
        // we need to pass the database connection to the EntityFramework
        //     DbContext class via the :base("xxxx") parameter
        // this is done via the NortwindContext constructor
        public NorthwindContext():base("NWDB")
        {

        }

        // indicate the property in this context class that will
        //    connection the sql table to the data definition class
        // this is done by using the EntityFramework datatype DbSet<T>
        //     where <T> is the data defintion class
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
