using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Students
    {
        public virtual int StudentsID { get; set; }
        public virtual int UVUID { get; set; }
        public virtual string StudentName { get; set; }
        public virtual string StudentEmail { get; set; }
        public virtual string StudentPhone { get; set; }
    }
}