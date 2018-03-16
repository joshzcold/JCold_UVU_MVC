using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class UVUInventoryDbInitializer : DropCreateDatabaseAlways<JCold_UVU_MVC_InventoryDb>
    {
        protected override void Seed(JCold_UVU_MVC_InventoryDb context)
        {
            context.Books.Add(new Books { Title = "Harry Potter", Author = "Jk Rollowing", ISBN = "9788700631625", Number =7, Publisher = "Penguin" });
            context.Books.Add(new Books { Title = "Coding For Dummies", Author = "Ethan Bradberry", ISBN = "9788478888566", Number = 8, Publisher = "O'Reiliy" });
            context.Departments.Add(new Department
            { DepName = "Computer Science", DepChair = "Keith Mulbery", DepChairEmail = "KM@uvu.edu",
                Email = "CS@uvu.edu", Location = "CS401", Telephone = "801-919-5180", WebAddress = "CS.uvu.edu" });
            context.Departments.Add(new Department
            {
                DepName = "Information Technology",
                DepChair = "Jeff Cold",
                DepChairEmail = "JC@uvu.edu",
                Email = "IT@uvu.edu",
                Location = "CS401",
                Telephone = "801-555-5555",
                WebAddress = "IT.uvu.edu"
            });
            context.Students.Add(new Students { StudentName = "Joshua Cold", StudentEmail = "10627512@my.uvu.edu", StudentPhone = "8015555555", UVUID = 10627512 });
            context.Students.Add(new Students { StudentName = "Cameron Pattberg", StudentEmail = "4568524@my.uvu.edu", StudentPhone = "555-555-5555", UVUID = 45685246 });

            context.Supplies.Add(new Supplies { Name = "Laptop", Number = 2, Value = "$500" });
            context.Supplies.Add(new Supplies { Name = "Protrator", Number = 3, Value = "$1000" });

        }
    }
}