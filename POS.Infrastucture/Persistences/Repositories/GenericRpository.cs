using POS.Infrastucture.Commons.Bases;
using POS.Infrastucture.Helpers;
using POS.Infrastucture.Persistences.Interfaces;
using System.Linq.Dynamic.Core;


namespace POS.Infrastucture.Persistences.Repositories
{
    public class GenericRpository<T> : IGenericRepository<T> where T : class
    {
        protected IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryble, bool pagination =false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryble.OrderBy($"{request.Sort} decending") : queryble.OrderBy($"{request.Sort} ascending");

            if (pagination) queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}
