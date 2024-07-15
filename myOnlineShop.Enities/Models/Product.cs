using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myOnlineShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [ValidateNever]
        public string Image {  get; set; }

        [Required,Range(0, 1000000)]
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Category")]
        [ValidateNever]
        public int CategoryId { get; set; }
        [ValidateNever]
        public virtual Category Category { get; set; }


        public Product()
        {
            CreatedDate = DateTime.Now;
        }


    }
}
