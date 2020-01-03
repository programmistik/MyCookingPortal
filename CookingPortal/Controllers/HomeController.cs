using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CookingPortal.Models;
using System.IO;
using CookingPortal.ViewModels;

namespace CookingPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly CookingPortalDbContext context;

        public HomeController(CookingPortalDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int? idSorting, string search = null)
        {
            IEnumerable<Recipe> rec = context.Recipes;

            if (!String.IsNullOrEmpty(search))
            {
                rec = rec.Where(s => s.Title.ToUpper().Contains(search.ToUpper()));
            }
            if (idSorting == 1)
                rec = context.Recipes.OrderByDescending(o => o.ViewCount);
            else if (idSorting == 2)
                rec = context.Recipes.OrderByDescending(o => o.LikeCount);

            var categories = context.Categories;
            ViewBag.Selected = idSorting == null ? 0 : idSorting;
            return View(new RecCatViewModel { Recipes = rec, Categories = categories});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewRecipe()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }

        [HttpPost]
        public IActionResult AddNewRecipe(Recipe rec)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(rec.ImageUri))
                    rec.ImageUri = Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\no-image.jpg";
                rec.CreationDate = DateTime.Now;
                context.Recipes.Add(rec);
                context.SaveChanges();
                TempData["Created"] = "Created";
                return RedirectToAction("Index");
            }
            TempData["Failed"] = "Failed! \nЗаголовок, описание и категория обязательные поля.";
            return RedirectToAction("AddNewRecipe");

        }

        public IActionResult Recipe(int id)
        {
            var res = context.Recipes.FirstOrDefault(x => x.Id == id);

            res.ViewCount++;

            context.SaveChanges();
            return View(res);
        }

        [HttpPost]
        public void jsAddLike(int id)
        {
            var rec = context.Recipes.FirstOrDefault(x => x.Id == id);
            rec.LikeCount++;
           
            context.SaveChanges();
            
        }

        public IActionResult jsSort(int idSorting)
        {
            return RedirectToAction("Index", new { idSorting = idSorting });

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var rec = context.Recipes.FirstOrDefault(x => x.Id == id);
            context.Recipes.Remove(rec);
            context.SaveChanges();
            TempData["Deleted"] = "Deleted";
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
