using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class CheckOutSupplies
    {
        public int CheckOutSuppliesID { get; set; }
        public int StudentsID { get; set; }
        public int SuppliesID { get; set; }
        public int DepartmentID { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DueDate { get; set; }
        [Display(Name ="Returned Supply")]
        public bool ReturnedSupply { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Returned Date")]
        public DateTime? ReturnedDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Check out Date")]
        public DateTime CheckedOutDate { get; set; }
        public virtual Students Students { get; set; }
        public virtual Supplies Supplies { get; set; }
        public virtual Department Department { get; set; }
    }
}