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
        [Display(Name ="UVU ID")]
        public virtual int UVUID { get; set; }

        [Display(Name ="Student Name")]
        public virtual string StudentName { get; set; }

        [Display(Name ="Student Email")]
        public virtual string StudentEmail { get; set; }

        [Display(Name ="Student Phone")]
        public virtual string StudentPhone { get; set; }
    }
}