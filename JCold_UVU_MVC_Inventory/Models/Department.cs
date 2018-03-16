using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Department
    {
        public virtual int DepartmentID { get; set; }
        [Display(Name ="Department Name")]
        public virtual string DepName { get; set; }
        public virtual string Location { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Email { get; set; }
        [Display(Name ="Website")]
        public virtual string WebAddress { get; set; }
        [Display(Name ="Department Chair")]
        public virtual string DepChair { get; set; }
        [Display(Name ="Department Chair Email")]
        public virtual string DepChairEmail { get; set; }
    }
}