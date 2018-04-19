using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Books
    {
        public int BooksID { get; set; }
        [Required(ErrorMessage ="Provide a book title")]
        [Display(Name ="Book Title")]
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Number { get; set; }
        public bool Available { get; set; } = true;
        [Display(Name ="Class Room")]
        public string ClassRoom { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}