using POS.Domain.Entities;
using POS.Infrastucture.Commons.Bases.Request;
using POS.Infrastucture.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastucture.Persistences.Interfaces
{
    public interface ICategoryRepository
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filter);
        Task<IEnumerable<Category>> ListSelectCategories();
        Task<Category> CategoryById(int categoriyId);
        Task<bool> RegisterCategory(Category category);
        Task<bool> EditCategory(Category category);
        Task<bool> RemoveCategory(int categoriyId);
        
    }
}
