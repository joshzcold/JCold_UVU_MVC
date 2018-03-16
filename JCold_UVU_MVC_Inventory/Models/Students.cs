using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Students
    {
        public virtual int StudentsID { get; set; }

        [Required(ErrorMessage = "Provide a Student ID Number")]
        [Display(Name ="UVU ID")]
        [Range(10000000, 99999999)]
        public virtual int UVUID { get; set; }

        [Required(ErrorMessage = "Provide a Student Name")]
        [Display(Name ="Student Name")]
        public virtual string StudentName { get; set; }

        [Display(Name ="Student Email")]
        public virtual string StudentEmail { get; set; }
        
        [Display(Name ="Student Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public virtual string StudentPhone { get; set; }
    }
}