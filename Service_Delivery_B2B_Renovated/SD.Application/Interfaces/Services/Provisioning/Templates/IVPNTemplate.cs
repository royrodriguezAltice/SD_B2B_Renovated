using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Application.Interfaces.Services.Provisioning.Templates
{
    public interface IVPNTemplate
    {
        #region Add Lines
        string AddJump();
        string AddPad();
        string AddAdmiration();

        #region AddLines MAN
        string AddLineDescriptionMan();
        string AddLineVrfMan();
        string AddLineIpFamilyMan();
        string AddLineRouterRDMan();
        string AddLineApplyLabelMan();
        string AddLineRtExportMan();
        string AddLineRtImportMan();
        string AddLineBgpMan();
        string AddLineIpVrfMan();
        string AddLineIRDirectMan();
        string AddLineIRStaticMan();

        #endregion

        #region AddLines ISP
        string AddLineVrfIsp();
        string AddLineModeBigIsp();
        string AddLineAddressFamilyIsp();
        string AddLineRtExportIsp();
        string AddLineRtImportIsp();
        string AddLineBgpIsp();
        string AddLineRouterRDIsp();
        string AddLineApplyLabelIsp();
        string AddLineRedistributeConnectedIsp();
        string AddLineRedistributeStaticIsp();

        #endregion

        #endregion

        string CreateTemplate(string noEquipment);
    }
}
