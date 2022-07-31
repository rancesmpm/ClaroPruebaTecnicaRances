using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class CoverPhotoViewModel
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string Url { get; set; }

        [ForeignKey("IdBook")]
        public BookViewModel Book { get; set; }
    }
}