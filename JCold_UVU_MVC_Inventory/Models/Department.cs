using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        [Required(ErrorMessage ="Provide a Department Name")]
        [Display(Name ="Department Name")]

        public string DepName { get; set; }

        public string Location { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name ="Website")]
        public string WebAddress { get; set; }

        [Display(Name ="Department Chair")]
        public string DepChair { get; set; }

        [Display(Name ="Department Chair Email")]
        [DataType(DataType.EmailAddress)]
        public string DepChairEmail { get; set; }
    }
}