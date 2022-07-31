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
    public class CoverPhotoController : ControllerBase
    {
        private readonly BookDbContext _bookContext;

        public CoverPhotoController(BookDbContext bookDbContext)
        {
            _bookContext = bookDbContext;
        }

        [HttpGet]
        public List<CoverPhoto> GetCoverPhoto()
        {
            return _bookContext.CoverPhotos.ToList();
        }

        [HttpGet("{id}")]
        public CoverPhoto GetCoverPhotoById(int id)
        {
            return _bookContext.CoverPhotos.SingleOrDefault(c => c.Id == id);
        }

        [HttpPost]
        public IActionResult AddCoverPhoto(CoverPhoto coverPhoto)
        {
            _bookContext.CoverPhotos.Add(coverPhoto);
            _bookContext.SaveChanges();
            return Created("api/CoverPhoto/" + coverPhoto.Id, coverPhoto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCoverPhoto(int id, CoverPhoto coverPhoto)
        {
            var cp = _bookContext.CoverPhotos.SingleOrDefault(c => c.Id == id);
            if (cp == null)
            {
                return NotFound($"Cover Photo with the id {id} does not exist ");
            }

            if (cp.IdBook != 0 && cp.Url != null)
            {
                cp.IdBook = cp.IdBook;
                cp.Url = cp.Url;
                
            }
            _bookContext.Update(cp);
            _bookContext.SaveChanges();
            return Ok($"Cover Photo with the Id {id} update successfully");
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cp = _bookContext.CoverPhotos.SingleOrDefault(c => c.Id == id);
            if (cp == null)
            {
                return NotFound($"Cover Photo with the id {id} does not exist ");
            }
            _bookContext.CoverPhotos.Remove(cp);
            _bookContext.SaveChanges();
            return Ok($"Cover Photo with the id {id} delete Succesfully");
        }
    }
}
