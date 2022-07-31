using FrontEnd.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class CoverPhotoController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44995/api");

        HttpClient client;

        public CoverPhotoController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: Book
        public ActionResult Index()
        {
            List<CoverPhotoViewModel> coverlist = new List<CoverPhotoViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/coverphoto").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                coverlist = JsonConvert.DeserializeObject<List<CoverPhotoViewModel>>(data);
            }
            return View(coverlist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CoverPhotoViewModel coverPhotoViewModel)
        {
            string data = JsonConvert.SerializeObject(coverPhotoViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/coverphoto", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }



        [HttpPost]
        public ActionResult Edit(int id)
        {
            CoverPhotoViewModel cover = new CoverPhotoViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/coverphoto/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cover = JsonConvert.DeserializeObject<CoverPhotoViewModel>(data);
            }
            return View("Create", cover);

        }

        [HttpPost]
        public ActionResult Edit(CoverPhotoViewModel cover)
        {
            string data = JsonConvert.SerializeObject(cover);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/coverphoto/" + cover.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", content);

        }
    }
}