using Microsoft.EntityFrameworkCore;
using SD.Domain.Interfaces.Repositories.Generic;
using SD.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SD.Persistence.Repositories.Generic
{
	public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
	{
		private readonly SdB2bDbContext _sdB2Bcontext;
		private readonly DbSet<TModel> _dbSet;

		public GenericRepository(SdB2bDbContext sdB2Bcontext) 
		{
			_sdB2Bcontext = sdB2Bcontext;
			_dbSet = _sdB2Bcontext.Set<TModel>();
		}

		public async Task<List<TModel>> GetEverythingAsync()
		{
			try
			{
				return await _dbSet.ToListAsync();
			}
			catch
			{
				throw;
			};
		}

		public async Task<TModel> GetByIdAsync(int id)
		{
			try
			{
				return await _dbSet.FindAsync(id);
			}
			catch
			{
				throw;
			};
		}

		public async Task<TModel> CreateAsync(TModel model)
		{
			try
			{
				await _sdB2Bcontext.AddAsync(model);
				await _sdB2Bcontext.SaveChangesAsync();
				return model;
			}
			catch
			{
				throw;
			};
		}

		public async Task<TModel> UpdateAsync(TModel model, int id)
		{
			try
			{
				TModel modelo = _dbSet.Find(id);
				_sdB2Bcontext.Update(modelo);
				await _sdB2Bcontext.SaveChangesAsync();
				return model;
			}
			catch
			{
				throw;
			};
		}

		public async Task<bool> DeleteAsync(TModel model)
		{
			try
			{
				_dbSet.Remove(model);
				await _sdB2Bcontext.SaveChangesAsync();
				return true;
			}
			catch
			{
				throw;
			};
		}

		public async Task<IQueryable<TModel>> VerifyDataExistenceAsync(Expression<Func<TModel, bool>> filter = null)
		{
			try
			{
				IQueryable<TModel> ModelQuery = filter == null ? _sdB2Bcontext.Set<TModel>() : _sdB2Bcontext.Set<TModel>().Where(filter);
				return ModelQuery;
			}
			catch
			{
				throw;
			};
		}
	}
}
