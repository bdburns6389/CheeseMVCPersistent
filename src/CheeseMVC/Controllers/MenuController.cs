using CheeseMVC.Data;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }


        public IActionResult Index()
        {//Might be wrong context.  Might need to be context.Menus.
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        public IActionResult Add()
        {//Won't work yet due to no menuviewmodel.
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }
        
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
             if (ModelState.IsValid)
             {
                // Add the new cheese to my existing cheeses
                Menu newMenu = new Menu 
                {
                    Name = addMenuViewModel.Name,
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
             }

            return View(addMenuViewModel);
        }
    }
}
