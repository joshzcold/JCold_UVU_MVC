namespace JCold_UVU_MVC_Inventory.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JCold_UVU_MVC_Inventory.Models.JCold_UVU_MVC_InventoryDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "JCold_UVU_MVC_Inventory.Models.JCold_UVU_MVC_InventoryDb";
        }

        protected override void Seed(JCold_UVU_MVC_Inventory.Models.JCold_UVU_MVC_InventoryDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
