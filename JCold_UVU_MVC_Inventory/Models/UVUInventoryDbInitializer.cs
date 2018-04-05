using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class UVUInventoryDbInitializer : DropCreateDatabaseAlways<JCold_UVU_MVC_InventoryDb>
    {
        protected override void Seed(JCold_UVU_MVC_InventoryDb context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            context.Books.Add(new Books { Title = "Harry Potter", Author = "Jk Rollowing", ISBN = "9788700631625", Number =7, Publisher = "Penguin", ClassRoom = "INFO-4410" });
            context.Books.Add(new Books { Title = "test1", Author = "Ethan Bradberry", ISBN = "9788478456789", Number = 8, Publisher = "O'Reiliy", ClassRoom = "INFO-3340" });
            context.Books.Add(new Books { Title = "test2", Author = "Bruce Bradberry", ISBN = "9784567888566", Number = 7, Publisher = "O'Woah", ClassRoom = "INFO-2210" });
            context.Books.Add(new Books { Title = "test3", Author = "Peter Frog", ISBN = "9711178888566", Number = 6, Publisher = "Willy Wonka", ClassRoom = "INFO-1120" });
            context.Books.Add(new Books { Title = "test4", Author = "Ethan Bradberry", ISBN = "9788478888566", Number = 5, Publisher = "O'Reiliy", ClassRoom = "INFO-3340" });
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
            context.Students.Add(new Students { StudentName = "Joshua Cold", StudentEmail = "10627512@my.uvu.edu", StudentPhone = "8015555555", UVUID = "10627512" });
            context.Students.Add(new Students { StudentName = "Cameron Pattberg", StudentEmail = "4568524@my.uvu.edu", StudentPhone = "555-555-5555", UVUID = "45685246" });
            context.Students.Add(new Students { StudentName = "Johnathan Woodall", StudentEmail = "12345678@my.uvu.edu", StudentPhone = "555-555-5555", UVUID = "12345678" });
            context.Students.Add(new Students { StudentName = "Kaleb Alexander", StudentEmail = "87654321@my.uvu.edu", StudentPhone = "555-555-5555", UVUID = "87654321" });
            context.Students.Add(new Students { StudentName = "Renato Pesantes", StudentEmail = "65412387@my.uvu.edu", StudentPhone = "555-555-5555", UVUID = "65412387" });

            context.Supplies.Add(new Supplies { Name = "Laptop Dell", Number = 2, Value = "$500", ClassRoom="Med859" });
            context.Supplies.Add(new Supplies { Name = "Spectilizer", Number = 2, Value = "$3,000", ClassRoom = "Sci-3400" });
            context.Supplies.Add(new Supplies { Name = "Oliliscope", Number = 1, Value = "$50,000", ClassRoom = "Med859" });
            context.Supplies.Add(new Supplies { Name = "Protractor", Number = 3, Value = "$1,000", ClassRoom = "Art452" });
            context.Supplies.Add(new Supplies { Name = "Protractor2", Number = 4, Value = "$1,000", ClassRoom = "Art452" });
            context.Supplies.Add(new Supplies { Name = "Protractor3", Number = 5, Value = "$1,000", ClassRoom = "Art452" });
            context.Supplies.Add(new Supplies { Name = "Protractor4", Number = 6, Value = "$1,000", ClassRoom = "Art452" });

            base.Seed(context);

            var user0 = new ApplicationUser { UserName = "mphelps@yahoo.com", Email = "mphelps@yahoo.com", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 1, FirstName = "Michael", LastName = "Phelps" };
            var user1 = new ApplicationUser { UserName = "kledeky@yahoo.com", Email = "kledecy@yahoo.com", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 1, FirstName = "Katy", LastName = "Ledecky" };
            var user2 = new ApplicationUser { UserName = "joshzcold@yahoo.com", Email = "joshzcold@yahoo.com", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 1, FirstName = "Joshua", LastName = "Cold" };
            var user3 = new ApplicationUser { UserName = "CieraSilva@yahoo.com", Email = "CieraSilva@yahoo.com", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 1, FirstName = "Ciera", LastName = "Silva" };
            var user4 = new ApplicationUser { UserName = "WoodAllJohnny@yahoo.com", Email = "WoodAllJohnny@yahoo.com", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 1, FirstName = "Johnny", LastName = "WoodAll" };
            // I assumed we would want this user to be the one to edit values because its email says dmmpost. 
            var user5 = new ApplicationUser { UserName = "admin@uvu.edu", Email = "admin@uvu.edu", BirthDate = new DateTime(1985, 2, 4), UserRoleId = 2, FirstName = "UVU", LastName = "Admin" };

            userManager.Create(user0, "Control123!");
            userManager.Create(user1, "Control123!");
            userManager.Create(user2, "Control123!");
            userManager.Create(user3, "Control123!");
            userManager.Create(user4, "Control123!");
            userManager.Create(user5, "Control123!");

            roleManager.Create(new IdentityRole("canEdit"));
            // Changed which user can edit values
            userManager.AddToRole(user5.Id, "canEdit");

        }
    }
}