using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Students
    {
        public int StudentsID { get; set; }

        [Required(ErrorMessage = "Provide a Student ID Number")]
        [Display(Name ="UVU ID")]
        [Range(10000000, 99999999)]
        public string UVUID { get; set; }

        [Required(ErrorMessage = "Provide a Student Name")]
        [Display(Name ="Student Name")]
        public string StudentName { get; set; }

        [Display(Name ="Student Email")]
        public string StudentEmail { get; set; }
        
        [Display(Name ="Student Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string StudentPhone { get; set; }

        [Display(Name ="Checked Books")]
        public bool HasCheckedOutBooks { get; set; } = false;

        [Display(Name = "Checked Supplies")]
        public bool HasCheckedOutSupplies { get; set; } = false;
    }
}