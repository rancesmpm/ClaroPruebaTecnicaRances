using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPruebaTecnicaRances.Model
{
    public class Author
    {
        public int Id { get; set; }
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [ForeignKey("IdBook")]
        public Book Book { get; set; }
    }
}
