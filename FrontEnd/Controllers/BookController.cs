using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FrontEnd.Controllers
{
    public class BookController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:44995/api");

        HttpClient client;

        public BookController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        // GET: Book
        public ActionResult Index()
        {
            List<BookViewModel> booklist = new List<BookViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/book").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
               booklist = JsonConvert.DeserializeObject<List<BookViewModel>>(data);
            }
            return View(booklist);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookViewModel bookViewModel)
        {
            string data = JsonConvert.SerializeObject(bookViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/book", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();

        }

       

        [HttpPost]
        public ActionResult Edit(int id)
        {
            BookViewModel book = new BookViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/book/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookViewModel>(data);
            }
            return View("Create",book);

        }

        [HttpPost]
        public ActionResult Edit(BookViewModel  book)
        {
            string data = JsonConvert.SerializeObject(book);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/book/" + book.Id, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", book );

        }
    }
}