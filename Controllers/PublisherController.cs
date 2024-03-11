using Microsoft.AspNetCore.Mvc;
using MyBookStore.Models.Domain;
using MyBookStore.Repositories.Abstract;

namespace MyBookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherService service;
        public PublisherController(IPublisherService service)
        {
            this.service = service;
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Add(model);
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
            var result = service.FindById(id);

            return View(result);
        }
        [HttpPost]
        public IActionResult Update(Publisher model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = service.Update(model);
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
            
            var result = service.Delete(id);
          
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {

            var data = service.GetAll();

            return View(data);
            
        }
    }
}
