using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookingPortal.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string email { get; set; }
        public string ImageUri { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
