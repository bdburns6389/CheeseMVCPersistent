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
            List<CheeseMenu> categories = context.CheeseMenus.ToList();
            return View(categories);
        }
    }
}
