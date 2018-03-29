using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class JCold_UVU_MVC_InventoryDb : DbContext
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

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.Books> Books { get; set; }

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.Students> Students { get; set; }

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.Supplies> Supplies { get; set; }

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.CheckOutBook> CheckOutBooks { get; set; }

        public System.Data.Entity.DbSet<JCold_UVU_MVC_Inventory.Models.CheckOutSupplies> CheckOutSupplies { get; set; }
    }
}
