using Microsoft.EntityFrameworkCore;
using SD.Application.Provisioning.Control_OC.OC.Exceptions;
using SD.Application.Provisioning.Control_OC.OC.ManualMappers;
using SD.Domain.Entities.Provisioning.Control_OC.Apn;
using SD.Domain.Interfaces.Repositories.Provisioning.Control_OC;
using SD.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Persistence.Repositories.Provisioning.Control_OC
{
    public class OcRepository<TModel> : IOcRepository<TModel> where TModel : class
    {
        private readonly SdB2bDbContext _sdB2Bcontext;
        private readonly DbSet<TModel> _dbSet;

        public OcRepository(SdB2bDbContext sdB2Bcontext)
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

        public async Task<TbApn> GetOcApnDataAsync(string oc)
        {
            try
            {
                return _sdB2Bcontext.TbApn.FirstOrDefault(o => o.Id == oc);

            }
            catch (Exception ex)
            {
                throw new GetOcsFailedException();
            }
        }

        public async Task NoTrackingBehaivour()
        {
            try 
            {
                _sdB2Bcontext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            catch(Exception ex)
            {
                throw new ChangeTrackingBehaviourFailedException();
            }
        }
    }
}
