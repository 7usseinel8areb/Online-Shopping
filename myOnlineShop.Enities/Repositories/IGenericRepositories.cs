using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.Enities.Repositories
{
    public interface IGenericRepositories<T> where T : class
    {
        // T = any model => Category, Product, ......

        // .toList()
        //Expression<Func<T, bool>> predicate, string? IncludeEntity    =>    to amtch .Include(entity) & .where(expression)
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate, string? IncludeEntity);

        //get single element
        //Expression<Func<T, bool>> predicate, string? IncludeEntity    =>    to amtch .Include(entity) & .where(expression) & for example .toSingleOrDefault
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate, string? IncludeEntity);

        // Add new Element
        void Add(T item);

        //Remove Single item
        void Delete (T item);

        //Remove Range of items
        void RemoveRang(IEnumerable<T> items);
    }
}
