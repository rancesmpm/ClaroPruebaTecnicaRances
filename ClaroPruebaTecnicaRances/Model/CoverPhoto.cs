using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPruebaTecnicaRances.Model
{
    public class CoverPhoto
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string Url { get; set; }

        [ForeignKey("IdBook")]
        public Book Book { get; set; }

    }
}
