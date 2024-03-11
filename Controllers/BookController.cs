using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBookStore.Models.Domain;
using MyBookStore.Repositories.Abstract;

namespace MyBookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookservice;
        private readonly IAuthorService authorservice;
        private readonly IGenreService genreservice;
        private readonly IPublisherService publisherservice;
        public BookController(IBookService bookservice, IAuthorService authorservice, IGenreService genreservice, IPublisherService publisherservice)
        {
            this.bookservice = bookservice;
            this.authorservice = authorservice;
            this.genreservice = genreservice; 
            this.publisherservice = publisherservice;
        }
        public IActionResult Add()
        {
            var model = new Book();
            model.AuthorList = authorservice.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString() }).ToList();
            model.PublisherList = publisherservice.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString() }).ToList();
            model.GenreList = genreservice.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString() }).ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(Book model)
        {
            
            model.AuthorList = authorservice.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected=a.Id==model.AuthorID }).ToList();
            model.PublisherList = publisherservice.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreservice.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreID }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookservice.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "The error has occured on the server side";
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var model = bookservice.FindById(id);
            model.AuthorList = authorservice.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorID }).ToList();
            model.PublisherList = publisherservice.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreservice.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreID }).ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Book model)
        {
            model.AuthorList = authorservice.GetAll().Select(a => new SelectListItem { Text = a.AuthorName, Value = a.Id.ToString(), Selected = a.Id == model.AuthorID }).ToList();
            model.PublisherList = publisherservice.GetAll().Select(a => new SelectListItem { Text = a.PublisherName, Value = a.Id.ToString(), Selected = a.Id == model.PublisherId }).ToList();
            model.GenreList = genreservice.GetAll().Select(a => new SelectListItem { Text = a.Name, Value = a.Id.ToString(), Selected = a.Id == model.GenreID }).ToList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = bookservice.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "The error has occured on the server side";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            
            var result = bookservice.Delete(id);
          
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {

            var data = bookservice.GetAll();

            return View(data);
            
        }
    }
}
