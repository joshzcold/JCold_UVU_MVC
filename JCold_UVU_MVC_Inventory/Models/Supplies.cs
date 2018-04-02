using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Supplies
    {
        public int SuppliesID { get; set; }
        [Required(ErrorMessage ="Provide a Supply Name")]
        [Display(Name ="Supply Name")]
        public string Name { get; set; }
        public string Value { get; set; }
        public int Number { get; set; }
        public bool Available { get; set; } = true;
        [Display(Name = "Class Room")]
        public string ClassRoom { get; set; }
    }
}