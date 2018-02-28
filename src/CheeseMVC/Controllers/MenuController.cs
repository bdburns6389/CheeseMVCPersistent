using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                .CheeseMenus.Include(item => item.Cheese).Where(cm => cm.MenuID == id).ToList();

            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            return View(new AddMenuItemViewModel(menu, cheeses));
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                var cheeseID = addMenuItemViewModel.CheeseID;
                var menuID = addMenuItemViewModel.MenuId;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        Menu = context.Menus.Single(m => m.ID == menuID)
                    };

                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();

                    return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel)); //TODO not sure where to redirect.
                }
            }
            return View(addMenuItemViewModel);
        }
    }
}
