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
    public class AuthorController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44995/api");

        HttpClient client;

        public AuthorController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: Book
        public ActionResult Index()
        {
            List<AuthorViewModel> authorlist = new List<AuthorViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/author").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                authorlist = JsonConvert.DeserializeObject<List<AuthorViewModel>>(data);
            }
            return View(authorlist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AuthorViewModel authorViewModel)
        {
            string data = JsonConvert.SerializeObject(authorViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/author", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }



        [HttpPost]
        public ActionResult Edit(int id)
        {
            AuthorViewModel author = new AuthorViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/author/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<AuthorViewModel>(data);
            }
            return View("Create", author);

        }

        [HttpPost]
        public ActionResult Edit(AuthorViewModel author)
        {
            string data = JsonConvert.SerializeObject(author);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/author/" + author.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", author);

        }
    }
}