using Microsoft.EntityFrameworkCore;
using myOnlineShop.DataAccess.Data;
using myOnlineShop.Enities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.DataAccess.Implemntation
{
    public class GenericRepository<T> : IGenericRepositories<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public void Add(T item)
        {

            //Categories.Add()
            _dbSet.Add(item);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, string? IncludeEntity)
        {
            IQueryable<T> queryResult = _dbSet;
            if (predicate != null)
            {
                queryResult = queryResult.Where(predicate);
            }
            if (IncludeEntity != null)
            {
                //.Include("Cstegory,Product,Logo") => ["", "", ""]
                foreach(var item in IncludeEntity.Split(','))
                {
                    queryResult = queryResult.Include(item);
                }
            }
            return queryResult.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? IncludeEntity)
        {
            IQueryable<T> queryResult = _dbSet;
            if (predicate != null)
            {
                queryResult = queryResult.Where(predicate);
            }
            if (IncludeEntity != null)
            {
                //.Include("Cstegory,Product,Logo") => ["", "", ""]
                foreach (var item in IncludeEntity.Split(','))
                {
                    queryResult = queryResult.Include(item);
                }
            }
            return queryResult.SingleOrDefault();
        }

        public void RemoveRang(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

    }
}
