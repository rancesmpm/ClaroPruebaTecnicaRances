using ClaroPruebaTecnicaRances.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroPruebaTecnicaRances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookDbContext _bookContext;

        public AuthorController(BookDbContext bookDbContext)
        {
            _bookContext = bookDbContext;
        }

        [HttpGet]
        public List<Author> GetAuthor()
        {
            return _bookContext.Authors.ToList();
        }

        [HttpGet("{id}")]
        public Author GetAuthorById(int id)
        {
            return _bookContext.Authors.SingleOrDefault(b => b.Id == id);
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            _bookContext.Authors.Add(author);
            _bookContext.SaveChanges();
            return Created("api/Author/" + author.Id, author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            var aut = _bookContext.Authors.SingleOrDefault(a => a.Id == id);
            if (aut == null)
            {
                return NotFound($"Book with the id {id} does not exist ");
            }

            if (aut.FirstName != null && aut.LastName!= null && aut.IdBook != 0)
            {
                aut.FirstName = aut.FirstName;
                aut.LastName = aut.LastName;
                aut.IdBook = aut.IdBook;
            }
            _bookContext.Update(aut);
            _bookContext.SaveChanges();
            return Ok($"Author with the Id {id} update successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aut = _bookContext.Authors.SingleOrDefault(a => a.Id == id);
            if (aut == null)
            {
                return NotFound($"Author with the id {id} does not exist ");
            }
            _bookContext.Authors.Remove(aut);
            _bookContext.SaveChanges();
            return Ok($"Author with the id {id} delete Succesfully");
        }
    }
}
