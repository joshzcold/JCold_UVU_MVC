using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class CheckedOut
    {
        public virtual int CheckedOutID { get; set; }
        public virtual int StudentsID { get; set; }
        public virtual int? BooksID { get; set; }
        public virtual int? SuppliesID { get; set; }
        public virtual int DepartmentID { get; set; }
        public virtual bool Returned { get; set; }
        [Column(TypeName = "datetime2")]
        public virtual DateTime? ReturnedDate { get; set; }
        [Column(TypeName = "datetime2")]
        public virtual DateTime CheckedOutDate { get; set; }
        public virtual Students Students { get; set; }
        public virtual Books Books { get; set; }
        public virtual Supplies Supplies { get; set; }
        public virtual Department Department { get; set; }
    }
}