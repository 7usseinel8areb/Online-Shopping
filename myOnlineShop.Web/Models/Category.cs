﻿
namespace myOnlineShop.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime CreatedTime { get; private set; }


        public Category()
        {
            CreatedTime = DateTime.Now;
        }
    }
}
