using myOnlineShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myOnlineShop.Enities.Repositories
{
    public interface ICategoryRepository :IGenericRepositories<Category>
    {
        void Update(Category category);
    }
}
