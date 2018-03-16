using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Supplies
    {
        public virtual int SuppliesID { get; set; }
        [Display(Name ="Supply Name")]
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
        public virtual int Number { get; set; }
        public virtual bool Available { get; set; } = true;
    }
}