using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Repositories.Generic
{
    public interface IGenericRepository<TModel> where TModel : class
	{
		Task NoTrackingBehaivour();
        Task<TModel> GetByIdAsync(int id);
		Task<List<TModel>> GetEverythingAsync();
		Task<TModel> CreateAsync(TModel model);
		Task<TModel> UpdateAsync(TModel model, int id);
		Task<bool> DeleteAsync(TModel model);
		Task<IQueryable<TModel>> VerifyDataExistenceAsync(Expression<Func<TModel, bool>> filter = null);
	}
}
