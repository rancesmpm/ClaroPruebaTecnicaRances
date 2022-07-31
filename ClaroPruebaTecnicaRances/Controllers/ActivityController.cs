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
    public class ActivityController : ControllerBase
    {
        private readonly BookDbContext _bookContext;

        public ActivityController(BookDbContext bookDbContext)
        {
            _bookContext = bookDbContext;
        }

        [HttpGet]
        public List<Activity> GetActivity()
        {
            return _bookContext.Activities.ToList();
        }

        [HttpGet("{id}")]
        public Activity GetActivityById(int id)
        {
            return _bookContext.Activities.SingleOrDefault(a => a.Id == id);
        }

        [HttpPost]
        public IActionResult AddActivity(Activity activity)
        {
            _bookContext.Activities.Add(activity);
            _bookContext.SaveChanges();
            return Created("api/Activity/" + activity.Id, activity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateActivity(int id, Activity activity)
        {
            var at = _bookContext.Activities.SingleOrDefault(a => a.Id == id);
            if (at == null)
            {
                return NotFound($"Activity with the id {id} does not exist ");
            }

            if (at.Title != null && at.DueDate != null && at.Completed != false)
            {
                at.Title = at.Title;
                at.DueDate = at.DueDate;
                at.Completed = at.Completed;
                

            }
            _bookContext.Update(at);
            _bookContext.SaveChanges();
            return Ok($"Activity with the Id {id} update successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var at = _bookContext.Activities.SingleOrDefault(a => a.Id == id);
            if (at == null)
            {
                return NotFound($"Activity with the id {id} does not exist ");
            }
            _bookContext.Activities.Remove(at);
            _bookContext.SaveChanges();
            return Ok($"Activity with the id {id} delete Succesfully");
        }
    }
}

