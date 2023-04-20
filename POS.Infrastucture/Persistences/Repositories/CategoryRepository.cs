using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastucture.Commons.Bases.Request;
using POS.Infrastucture.Commons.Bases.Response;
using POS.Infrastucture.Persistences.Contexts;
using POS.Infrastucture.Persistences.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastucture.Persistences.Repositories
{
    public class CategoryRepository: GenericRpository<Category>, ICategoryRepository
    {
        private readonly POSContext _context;

        public CategoryRepository(POSContext context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filter)
        {
            var response = new BaseEntityResponse<Category>();
            var categories = (from c in _context.Categories
                              where c.AuditDeleteUser == null && c.AuditDeleteDate == null
                              select c).AsNoTracking().AsQueryable();

            if(filter.NumFilter is not null && !string.IsNullOrEmpty(filter.TextFilter))
            {
                switch (filter.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name!.Contains(filter.TextFilter));
                        break;
                    case 2:
                        categories = categories.Where(x => x.Description!.Contains(filter.TextFilter));
                        break;
                }
            }
            if (filter.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filter.StateFilter));
            }
            if(!string.IsNullOrEmpty(filter.StartDate) && !string.IsNullOrEmpty(filter.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(filter.StartDate)
                && x.AuditCreateDate <= Convert.ToDateTime(filter.EndDate).AddDays(1));
            }
            if (filter.Sort is null) filter.Sort = "CategoryId";

            response.TotalRecords = await categories.CountAsync();
            response.Items = await Ordering(filter, categories, !(bool)filter.Dowload!).ToListAsync();
            return response;
        }

        public async Task<IEnumerable<Category>> ListSelectCategories()
        {
            var categories = await _context.Categories.Where(
                x => x.State.Equals(1) && x.AuditDeleteUser == null && x.AuditDeleteDate == null).AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<Category> CategoryById(int categoriyId)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId.Equals(categoriyId));
            return category!;
        }

        public async Task<bool> RegisterCategory(Category category)
        {
            category.AuditCreateUser = 1;
            category.AuditCreateDate = DateTime.Now;

            await _context.AddAsync(category);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> EditCategory(Category category)
        {
            category.AuditUpdateUser = 1;
            category.AuditUpdateDate = DateTime.Now;

            _context.Update(category);
            _context.Entry(category).Property(x => x.AuditUpdateUser).IsModified = false;
            _context.Entry(category).Property(x => x.AuditUpdateDate).IsModified = false;

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<bool> RemoveCategory(int categoriyId)
        {
            var category = await _context.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.CategoryId.Equals(categoriyId));
            category!.AuditDeleteUser = 1;
            category.AuditDeleteDate = DateTime.Now;

            _context.Update(category);
            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;

        }
    }
}
