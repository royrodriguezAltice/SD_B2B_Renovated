using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Provisioning.Templates
{
    public interface ILoopbackTemplate
    {
        #region Add Lines

        #region Add Lines Genericas
        string AddJump();
        string AddPad();
        string AddAdmiration();
        string AddLineInterface();
        string AddLineDescription();
        #endregion

        #region Add Lines MAN
        string AddLineVrfMan();
        string AddLineIpAddressMan();
        #endregion

        #region Add Lines ISP
        string AddLineVrfIsp();
        string AddLineIpAddressIsp();
        #endregion

        #endregion

        Task<string> CreateTemplate(string noEquipment);
    }
}
