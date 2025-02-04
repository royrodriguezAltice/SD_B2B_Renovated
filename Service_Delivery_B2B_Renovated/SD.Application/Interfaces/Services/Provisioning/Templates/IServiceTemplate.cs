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
        string AddLineHeader();
        string AddJump();
        string AddLineDescriptionB2B();
        string AddLineBandWith();
        string AddLineInterface();
        string AddLineRouteStatic();
        string AddLineStatitics();
        string AddLineTraficPolicy();
        #endregion

        string CreateTemplate();
    }
}
