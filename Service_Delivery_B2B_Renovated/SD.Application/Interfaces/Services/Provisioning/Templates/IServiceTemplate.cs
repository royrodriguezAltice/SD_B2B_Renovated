using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Provisioning.Templates
{
    public interface IServiceTemplate
    {
        #region AddLines
        Task<string> AddLineHeader();
        Task<string> AddLineDescriptionB2B();
        Task<string> AddLineBandWith();
        Task<string> AddLineInterface();
        Task<string> AddLineRouteStatic();
        Task<string> AddLineStatitics();
        Task<string> AddLineTraficPolicy();
        #endregion

        Task<string> CreateTemplate();
    }
}
