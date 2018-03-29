[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(JCold_UVU_MVC_Inventory.MVCGridConfig), "RegisterGrids")]

namespace JCold_UVU_MVC_Inventory
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using JCold_UVU_MVC_Inventory.Models;

    using MVCGrid.Models;
    using MVCGrid.Web;
    using System.Data.Entity;

    public static class MVCGridConfig
    {
        public static void RegisterGrids()
        {
            var db = new JCold_UVU_MVC_InventoryDb();
            MVCGridDefinitionTable.Add("CheckBookGrid", new MVCGridBuilder<CheckOutBook>()

                .AddColumns
                (cols => {
                    //Add your columns here
                    cols.Add("Book Name").WithValueExpression(p => p.Books.Title.ToString());
                    cols.Add("Department Name").WithValueExpression(p => p.Department.DepName);
                    cols.Add("Student Name").WithValueExpression(p => p.Students.StudentName);
                    cols.Add("Returned Book").WithValueExpression(p => p.ReturnedBook ? "Yes" : "No");
                    cols.Add("Due Date").WithValueExpression(p => p.DueDate.ToShortDateString());
                    cols.Add("Returned Date").WithValueExpression(p => p.ReturnedDate.ToString());
                    cols.Add("Check out date").WithValueExpression(p => p.CheckedOutDate.ToShortDateString());
                }).WithSorting(true, "Due Date")
                
                .WithRetrieveDataMethod((context) =>
                {
                    //Query your data here. Obey Ordering, paging and filtering parameters given in the context.QueryOptions.
                    // Use Entity Framework, a module from your IoC Container, or any other method.
                    // Return QueryResult object containing IEnumerable<YouModelItem>


                    var options = context.QueryOptions;
                    var result = new QueryResult<CheckOutBook>();
                    var query = db.CheckOutBooks;
                    //if (!String.IsNullOrWhiteSpace(options.SortColumnName))
                    //{
                    //    switch (options.SortColumnName.ToLower())
                    //    {
                    //        case "firstname":
                    //            query = query.OrderBy(p => p.Books.Title, options.SortDirection);
                    //            break;
                    //        case "lastname":
                    //            query = query.OrderBy(p => p.Department.DepName, options.SortDirection);
                    //            break;
                    //    }
                    //}
                    result.Items = query.ToList();

                    return result;

                })
            );

        }

    }
}