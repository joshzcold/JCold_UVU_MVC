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
        [Required(ErrorMessage ="A book is required")]
        public int BooksID { get; set; }
        public int DepartmentID { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime DueDate { get; set; }
        [Display(Name = "Returned")]
        public bool ReturnedBook { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Returned Date")]
        public DateTime? ReturnedDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Display(Name ="Checked out Date")]
        public DateTime CheckedOutDate { get; set; }
        public Students Students { get; set; }
        public Books Books { get; set; }
        public Department Department { get; set; }
    }
}