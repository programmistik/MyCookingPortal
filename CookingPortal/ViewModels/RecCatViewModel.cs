using CookingPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPortal.ViewModels
{
    public class RecCatViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
