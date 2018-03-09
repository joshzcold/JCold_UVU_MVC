using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Department
    {
        public virtual int DepartmentID { get; set; }
        public virtual string DepName { get; set; }
        public virtual string Location { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Email { get; set; }
        public virtual string WebAddress { get; set; }
        public virtual string DepChair { get; set; }
        public virtual string DepChairEmail { get; set; }
    }
}