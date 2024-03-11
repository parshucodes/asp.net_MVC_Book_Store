using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models.Domain;
using MyBookStore.Repositories.Abstract;

namespace MyBookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService service;
        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Author model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error Occured at the server side";
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var data = service.FindById(id);
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(Author model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var result = service.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error Occured at the server side";
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var data = service.Delete(id);
            return RedirectToAction(nameof(GetAll));
        }

        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return View(data);
        }

    }
}
