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
    public class UserController : ControllerBase
    {
        private readonly BookDbContext _bookContext;

        public UserController(BookDbContext bookDbContext)
        {
            _bookContext = bookDbContext;
        }

        [HttpGet]
        public List<User> GetUser()
        {
            return _bookContext.Users.ToList();
        }

        [HttpGet("{id}")]
        public User GetUserById(int id)
        {
            return _bookContext.Users.SingleOrDefault(u => u.Id == id);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _bookContext.Users.Add(user);
            _bookContext.SaveChanges();
            return Created("api/User/" + user.Id, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var usu = _bookContext.Users.SingleOrDefault(u => u.Id == id);
            if (usu == null)
            {
                return NotFound($"User with the id {id} does not exist ");
            }

            if (usu.UserName != null && usu.Password != null )
            {
                usu.UserName = usu.UserName;
                usu.Password = usu.Password;
                


            }
            _bookContext.Update(usu);
            _bookContext.SaveChanges();
            return Ok($"User with the Id {id} update successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usu = _bookContext.Users.SingleOrDefault(u => u.Id == id);
            if (usu == null)
            {
                return NotFound($"User with the id {id} does not exist ");
            }
            _bookContext.Users.Remove(usu);
            _bookContext.SaveChanges();
            return Ok($"User with the id {id} delete Succesfully");
        }
    }
}
