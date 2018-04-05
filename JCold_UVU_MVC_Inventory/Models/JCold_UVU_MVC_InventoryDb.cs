using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class JCold_UVU_MVC_InventoryDb : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public JCold_UVU_MVC_InventoryDb() : base("name=JCold_UVU_MVC_InventoryDb")
        {

        }
        public static JCold_UVU_MVC_InventoryDb Create()
        {
            return new JCold_UVU_MVC_InventoryDb();
        }


        public DbSet<Department> Departments { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<Students> Students { get; set; }

        public DbSet<Supplies> Supplies { get; set; }

        public DbSet<CheckOutBook> CheckOutBooks { get; set; }

        public DbSet<CheckOutSupplies> CheckOutSupplies { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }
    }
}
