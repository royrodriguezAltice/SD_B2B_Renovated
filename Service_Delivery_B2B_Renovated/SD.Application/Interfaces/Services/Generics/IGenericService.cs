using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Generics
{
	public interface IGenericService<Record, Model>
		where Record : class
		where Model : class
	{
		public Task<List<Record>> GetAllAsync();
		public Task<Record> GetByIdAsync(int id);
		public Task<Record> AddAsync(Record rc);
		public Task<Record> UpdateAsync(Record rc, int id);
		public Task<bool> DeleteAsync(int id);
	}
}
