using SD.Application.Interfaces.Services.Provisioning.Templates;
using SD.Application.Provisioning.Control_OC.Template.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SD.Application.Provisioning.Control_OC.OC.DTOs.GetOC;

namespace SD.Application.Provisioning.Control_OC.Template.Services
{
    public class VPNTemplate : IVPNTemplate
    {
        private static GetOcDTO _tbOc;
        private StringBuilder _sb;

        public VPNTemplate(GetOcDTO tbOc)
        {
            _tbOc = tbOc;
            _sb?.Clear();
        }

        private StringBuilder GetBuilder()
        {
            return _sb ?? new StringBuilder();
        }
        private void CleanBuilder()
        {
            _sb?.Clear();
        }

        #region Add Lines

        #region Add Lines Genericas
        public string AddJump()
        {
            return "<br>";
        }
        public string AddPad()
        {
            return "#";
        }
        public string AddAdmiration()
        {
            return "!";
        }
        public string AddDescription()
        {
            return $"description {_tbOc.Cliente}";
        }
        #endregion

        #region Add Lines MAN
        public string AddLineRoutePolicyIspLacnicMAN()
        {
            return "ip ip-prefix DENY-PRIVATE-IP index 10 permit 10.0.0.0 8 le 32 <br>" +
                    "ip ip-prefix DENY-PRIVATE-IP index 20 permit 172.16.0.0 12 le 32 <br>" +
                    "ip ip-prefix DENY-PRIVATE-IP index 20 permit 192.168.0.0 16 le 32 <br>" +
                    "# <br>" +
                    "route-policy IMPORT-DIRECT-ISP-LACNIC deny node 10 <br>" +
                    "if-match ip-prefix DENY-PRIVATE-IP <br>" +
                    "# <br>" +
                    "route-policy IMPORT-DIRECT-ISP-LACNIC permit node 99 <br>" +
                    "# <br>" +
                    "# <br>" +
                    "route-policy IMPORT-STATIC-ISP-LACNIC deny node 10 <br>" +
                    "if-match ip-prefix DENY-PRIVATE-IP <br>" +
                    "# <br>" +
                    "route-policy IMPORT-STATIC-ISP-LACNIC permit node 99 <br>";
        }
        public string AddLineApplyLabelMan()
        {
            return "  apply-label per-instance";
        }
        public string AddLineBgpMan()
        {
            return "bgp 28118 ";
        }
        public string AddLineDescriptionMan()
        {
            return $" description {_tbOc.VpnAg}-{_tbOc.IdGrado}";
        }
        public string AddLineIpFamilyMan()
        {
            return " ipv4-family ";
        }
        public string AddLineIpVrfMan()
        {
            return $" ipv4-family vpn-instance {_tbOc.VpnAg}";
        }
        public string AddLineIRDirectMan()
        {
            return "  import-route direct route-policy IMPORT-DIRECT-ISP-LACNIC";
        }
        public string AddLineIRStaticMan()
        {
            return "  import-route static route-policy IMPORT-STATIC-ISP-LACNIC";
        }
        public string AddLineRouterRDMan()
        {
            return $"  route-distinguisher 28118:{_tbOc.RouterRD}666 ";
        }
        public string AddLineVrfMan()
        {
            return $"ip vpn-instance {_tbOc.VpnAg} ";
        }
        public string AddLineRtExportMan()
        {
            return $"  vpn-target 28118:666 export-extcommunity";
        }
        public string AddLineRtImportMan()
        {
            return $"  vpn-target 28118:666 import-extcommunity";
        }
        #endregion

        #region Add Lines ISP
        public string AddLineAddressFamilyIsp()
        {
            return "address-family ipv4 unicast";
        }
        public string AddLineApplyLabelIsp()
        {
            return "  label mode per-vrf";
        }
        public string AddLineBgpIsp()
        {
            return $"router bgp 28118";
        }
        public string AddLineRedistributeConnectedIsp()
        {
            return "   redistribute connected";
        }
        public string AddLineRedistributeStaticIsp()
        {
            return "   redistribute static metric 2";
        }
        public string AddLineModeBigIsp()
        {
            return " mode big";
        }
        public string AddLineRouterRDIsp()
        {
            return $"  rd 28118:{_tbOc.RouterRD}666";
        }
        public string AddLineRtExportIsp()
        {
            return "  export route-target<br>" +
                   $"  28118:666<br>";
        }
        public string AddLineRtImportIsp()
        {
            return "  import route-target<br>" +
                   $"   28118:666 <br>";
        }
        public string AddLineVrfIsp()
        {
            return $"vrf {_tbOc.VpnAg} ";
        }
        #endregion

        #region APN
        public string AddLineRtExportApn()
        {
            return "  export route-target <br>" +
                   $"  28118:{_tbOc.ApnData.RouterTarget}";
        }
        public string AddLineRtImportApn()
        {
            return "  import route-target <br>" +
                   $"   28118:{_tbOc.ApnData.RouterTarget}";
        }
        #endregion

        #endregion

        private string Huawei()
        {
            _sb = GetBuilder();

            if (_tbOc.VpnAg == "isp-lacnic")
            {
                _sb.AppendLine(AddLineRoutePolicyIspLacnicMAN());
            }

            _sb.AppendLine(AddJump());
            _sb.AppendLine(AddLineVrfMan());
            _sb.AppendLine(AddLineDescriptionMan());
            _sb.AppendLine(AddLineIpFamilyMan());
            _sb.AppendLine(AddLineRouterRDMan());
            _sb.AppendLine(AddLineApplyLabelMan());
            _sb.AppendLine(AddLineRtExportMan());
            _sb.AppendLine(AddLineRtImportMan());
            _sb.AppendLine(AddPad());
            _sb.AppendLine(AddLineBgpMan());
            _sb.AppendLine(AddLineIpVrfMan());

            if (_tbOc.VpnAg == "isp-lacnic")
            {
                _sb.AppendLine(AddLineIRDirectMan());
                _sb.AppendLine(AddLineIRStaticMan());
            }

            _sb.AppendLine(AddPad());
            _sb.AppendLine(AddPad());

            return _sb.ToString();
        }

        private string Cisco()
        {
            _sb = GetBuilder();

            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineModeBigIsp());
            _sb.AppendLine(AddLineAddressFamilyIsp());
            _sb.AppendLine(AddLineRtImportIsp());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineRtExportIsp());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineBgpIsp());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddLineRouterRDIsp());
            _sb.AppendLine(AddLineApplyLabelIsp());
            _sb.AppendLine(AddLineAddressFamilyIsp());
            _sb.AppendLine(AddLineRedistributeConnectedIsp());
            _sb.AppendLine(AddLineRedistributeStaticIsp());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }

        private string CiscoApn(string noEquipment)
        {
            _sb = GetBuilder();

            _sb.AppendLine(noEquipment == "Segundo" ? _tbOc.ApnData.SegundoEquipo : _tbOc.EquipoAg); //First equipment
            _sb.AppendLine(AddJump());
            _sb.AppendLine(AddLineVrfIsp());
            _sb.AppendLine(AddDescription());
            _sb.AppendLine(AddLineAddressFamilyIsp());
            _sb.AppendLine(AddLineRtImportApn());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddLineRtExportApn());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());
            _sb.AppendLine(AddAdmiration());

            return _sb.ToString();
        }

        public string CreateTemplate(string noEquipment)
        {
            try
            {
                //Clean the builder before make the template
                CleanBuilder();
                if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("MAN"))
                {
                    return Huawei();
                }
                else if (!string.IsNullOrEmpty(_tbOc.EquipoAg) && _tbOc.EquipoAg.StartsWith("ISP"))
                {
                    return _tbOc.Producto == "APN" ? CiscoApn(noEquipment) : Cisco();
                }
                return "";

            }
            catch(Exception ex)
            {
                throw new CreateTemplateFailedException(ex.Message);
            }
        }
    }
}
