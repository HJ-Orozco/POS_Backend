using POS.Domain.Entities;
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
    }
}
