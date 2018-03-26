using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class CheckOutBook
    {
        public int CheckOutBookID { get; set; }
        public int StudentsID { get; set; }
        public int BooksID { get; set; }
        public int DepartmentID { get; set; }
        [Column(TypeName ="datetime2")]
        public virtual DateTime DueDate { get; set; }
        [Display(Name = "Returned")]
        public bool ReturnedBook { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Returned Date")]
        public virtual DateTime? ReturnedDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Checked out Date")]
        public virtual DateTime CheckedOutDate { get; set; }
        public virtual Students Students { get; set; }
        public virtual Books Books { get; set; }
        public virtual Department Department { get; set; }
    }
}