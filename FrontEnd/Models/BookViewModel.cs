using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCpunt { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishDate { get; set; }
    }
}