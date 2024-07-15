using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.Enities.Repositories
{
    public interface IUnitOfwork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        int Complete();
    }
}
