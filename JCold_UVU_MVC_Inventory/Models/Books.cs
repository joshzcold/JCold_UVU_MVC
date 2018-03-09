using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCold_UVU_MVC_Inventory.Models
{
    public class Books
    {
        public virtual int BooksID { get; set; }
        public virtual string Title { get; set; }
        public virtual int ISBN { get; set; }
        public virtual string Author { get; set; }
        public virtual string Publisher { get; set; }
        public virtual int Number { get; set; }
        public virtual bool Available { get; set; } = true;
    }
}