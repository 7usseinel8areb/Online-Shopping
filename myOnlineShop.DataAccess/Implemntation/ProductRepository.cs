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
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            Product oldProduct = _context.Products.FirstOrDefault(c => c.Id == product.Id);
            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Description = product.Description;
                oldProduct.CreatedDate = DateTime.Now;
                oldProduct.Price = product.Price;
                oldProduct.CategoryId = product.CategoryId;
                _context.SaveChanges();
            }
        }
    }
}
