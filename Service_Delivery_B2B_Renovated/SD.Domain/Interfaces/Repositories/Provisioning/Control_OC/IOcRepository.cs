using SD.Domain.Entities.Provisioning.Control_OC.Apn;
using SD.Domain.Interfaces.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Domain.Interfaces.Repositories.Provisioning.Control_OC
{
    public interface IOcRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        Task<TbApn> GetOcApnDataAsync(string oc);
    }
}
