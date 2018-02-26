using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCheeseViewModel
    {
        [Required]
        [Display(Name = "Cheese Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must give your cheese a description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public List<SelectListItem> Categories { get; set;}

        public AddCheeseViewModel()
        {

        }
        
        /*
        public CheeseType Type { get; set; }

        public List<SelectListItem> CheeseTypes { get; set; }

         */
        public AddCheeseViewModel(IEnumerable<CheeseCategory> categories) {
            
            Categories = new List<SelectListItem>();

            // <option value="0">Hard</option>i
            foreach (var i in categories)
            {
                Categories.Add(new SelectListItem
                {
                    Value = (i.ID).ToString(),
                    Text = i.Name
                });
            }
            /*
            Categories.Add(new SelectListItem
            {
                Value = (CategoryID).ToString(),
                Text = Name
            });

            Categories.Add(new SelectListItem
            {
                Value = (CategoryID).ToString(),
                Text = Name
            });*/

        }
    }
}
