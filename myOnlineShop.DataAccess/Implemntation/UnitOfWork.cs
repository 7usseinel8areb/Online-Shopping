using myOnlineShop.DataAccess.Data;
using myOnlineShop.Enities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.DataAccess.Implemntation
{
    public class UnitOfWork : IUnitOfwork
    {
        private readonly AppDbContext _context;
        public ICategoryRepository Category {  get; private set; }

        public IProductRepository Product { get; set; }

        public UnitOfWork(AppDbContext context )
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
