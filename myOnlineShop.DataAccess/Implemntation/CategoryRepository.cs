using myOnlineShop.DataAccess.Data;
using myOnlineShop.Enities.Repositories;
using myOnlineShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.DataAccess.Implemntation
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            Category oldCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (oldCategory != null)
            {
                oldCategory.Name = category.Name;
                oldCategory.Description = category.Description;
                oldCategory.CreatedTime = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
