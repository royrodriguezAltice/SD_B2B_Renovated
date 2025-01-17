using AutoMapper;
using SD.Application.Interfaces.Services.Generics;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Common.Services.Generics
{
	//Se crea el servicio Generico que acepta las entidades de tipo Record y TEntity
	//e implementa la interfaz IGenericService y sus metodos
	public class GenericService<Record, TEntity> : IGenericService<Record, TEntity>
		where Record : class
		where TEntity : class
	{

		//Declara instancias del repositorio generico y del mapper
		private readonly IGenericRepository<TEntity> _repository;
		private readonly IMapper _mapper;

		//Se crea el constructor
		public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Record> AddAsync(Record rc)
		{
			//Se mapea el record a la entidad en cuestion
			TEntity entity = _mapper.Map<TEntity>(rc);

			//Se añade la entidad al Tracker
			await _repository.CreateAsync(entity);

			//Se retorna el record
			return rc;
		}

		public async Task<List<Record>> GetAllAsync()
		{
			List<TEntity> entities = await _repository.GetEverythingAsync();
			List<Record> records = _mapper.Map<List<Record>>(entities);

			return records;
		}

		public async Task<Record> GetByIdAsync(int id)
		{
			TEntity entity = await _repository.GetByIdAsync(id);
			return _mapper.Map<Record>(entity);
		}

		public async Task<Record> UpdateAsync(Record rc, int id)
		{
			TEntity entity = await _repository.GetByIdAsync(id);
			entity = await _repository.UpdateAsync(entity, id);
			return _mapper.Map<Record>(entity);
		}

		async Task<bool> IGenericService<Record, TEntity>.DeleteAsync(int id)
		{
			TEntity entity = await _repository.GetByIdAsync(id);
			if (entity != null)
			{
				await _repository.DeleteAsync(entity);
				return true;
			}
			return false;
		}
	}
}
