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
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _bookContext;

        public BookController(BookDbContext bookDbContext)
        {
            _bookContext = bookDbContext;
        }

        [HttpGet]
        public List<Book> GetBooks()
        {
            return _bookContext.Books.ToList();
        }

        [HttpGet("{id}")]
        public Book GetBookById(int id)
        {
            return _bookContext.Books.SingleOrDefault(b => b.Id == id);
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            _bookContext.Books.Add(book);
            _bookContext.SaveChanges();
            return Created("api/Book/" + book.Id, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook( int id, Book book)
        {
            var bo = _bookContext.Books.SingleOrDefault(b => b.Id == id);
            if (bo == null)
            {
                return NotFound($"Book with the id {id} does not exist ");
            }

            if (book.Title != null && bo.Description != null && bo.PageCpunt != 0 && bo.Excerpt != null && bo.PublishDate != null )
            {
                bo.Title = bo.Title;
                bo.Description = bo.Description;
                bo.PageCpunt = bo.PageCpunt;
                bo.Excerpt = bo.Excerpt;
                bo.PublishDate = bo.PublishDate;
            }
            _bookContext.Update(bo);
            _bookContext.SaveChanges();
            return Ok($"Book with the Id {id} update successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bo = _bookContext.Books.SingleOrDefault(b => b.Id == id);
            if (bo == null)
            {
                return NotFound($"Book with the id {id} does not exist ");
            }
            _bookContext.Books.Remove(bo);
            _bookContext.SaveChanges();
            return Ok($"Book with the id {id} delete Succesfully");
        }
    }
}
